using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Repository.ViewModel;
using Repository;

namespace ITStepShop.Controllers
{
    [Authorize(Roles = $"{WC.AdminRole}")]
    public class AdminRoleController : Controller
    {
        private readonly UserManager<ShopUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminRoleController(UserManager<ShopUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> ManageRole()
        {
            var users = _userManager.Users.ToList();
            var viewModel = new ManageRoleViewModel
            {
                Users = users,
                Roles = _roleManager.Roles.Select(r => new RoleSelectionViewModel
                {
                    RoleId = r.Id,
                    RoleName = r.Name,
                    IsSelected = false
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ManageRole(ManageRoleViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Failed to remove existing roles");
                return View(model);
            }

            result = await _userManager.AddToRolesAsync(user, model.Roles.Where(x => x.IsSelected).Select(x => x.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Failed to add selected roles");
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}