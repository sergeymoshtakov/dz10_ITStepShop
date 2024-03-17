using ITStepShop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Repository;
using Models.Repository.IRepository;
using Repository;
using Repository.ViewModel;
using System.Diagnostics;
using System.Security.Claims;

namespace ITStepShop.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryRepository _catRepo;
        private readonly IProductRepository _productRepo;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepo, ICategoryRepository categoryRepository)
        {
            _logger = logger;
            _productRepo = productRepo;
            _catRepo = categoryRepository;
        }

        public async Task<IActionResult> IndexAsync()
        {
            HomeVM homeVM = new HomeVM()
            {
                Products = await _productRepo.GetAllAsync("Category"),
                Categories = _catRepo.GetAll()
            };
            return View(homeVM);
        }

        [Authorize(Roles = $"{WC.CustomerRole}, {WC.AdminRole}")]
        public async Task<IActionResult> DetailsAsync(int id)
        {
            List<ShopingCart> shopingCartList = new List<ShopingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCart).Count() > 0)
            {
                shopingCartList = HttpContext.Session.Get<List<ShopingCart>>(WC.SessionCart);
            }
            DetailsVM detailsVM = new DetailsVM()
            {
                Product = await _productRepo.FirstOrDefaultAsync(p => p.Id == id, includeProperties: "Category"),
                ExistsInCart = false
            };
            foreach (var item in shopingCartList)
            {
                if (item.ProductId == id)
                {
                    detailsVM.ExistsInCart = true;
                }
            }
            return View(detailsVM);
        }

        [HttpPost, ActionName("Details")]
        public IActionResult DetailsPost(int id)
        {
            List<ShopingCart> shopingCartList = new List<ShopingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCart).Count() > 0)
            {
                shopingCartList = HttpContext.Session.Get<List<ShopingCart>>(WC.SessionCart);
            }
            shopingCartList.Add(new ShopingCart { ProductId = id });
            HttpContext.Session.Set(WC.SessionCart, shopingCartList);
            return RedirectToAction(nameof(IndexAsync));
        }

        public IActionResult RemoveFromCart(int id)
        {
            List<ShopingCart> shopingCartList = new List<ShopingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCart).Count() > 0)
            {
                shopingCartList = HttpContext.Session.Get<List<ShopingCart>>(WC.SessionCart);
            }
            var itemRemove = shopingCartList.SingleOrDefault(x => x.ProductId == id);
            if (itemRemove != null)
            {
                shopingCartList.Remove(itemRemove);
            }
            HttpContext.Session.Set(WC.SessionCart, shopingCartList);
            return RedirectToAction(nameof(IndexAsync));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
