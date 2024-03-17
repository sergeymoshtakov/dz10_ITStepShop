using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Repository.ViewModel;
using Repository;
using Microsoft.EntityFrameworkCore;

namespace ITStepShop.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = $"{WC.AdminRole}")]
    public class ApiUserController : Controller
    {
        private readonly UserManager<ShopUser> _userManager;

        public ApiUserController(UserManager<ShopUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IdentityUser>>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return View("GetAllUsers", users);
        }
    }
}
