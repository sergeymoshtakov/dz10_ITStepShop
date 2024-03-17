using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Data;
using Models.Repository.IRepository;
using Repository;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ITStepShop.Controllers
{
    [Authorize(Roles = $"{WC.ManagerRole}, {WC.AdminRole}")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly UserManager<ShopUser> _userManager;
        private readonly ApplicationDbContext _context;

        public OrderController(IOrderRepository orderRepository, UserManager<ShopUser> userManager, ApplicationDbContext context)
        {
            _orderRepository = orderRepository;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return View(orders);
        }

        public IActionResult Create(int productId)
        {
            var product = _context.Product.Find(productId);
            var user = _userManager.GetUserAsync(User).Result;

            // Перевірка наявності товару та користувача
            if (product == null || user == null)
            {
                return NotFound();
            }

            var order = new Order
            {
                CustomerId = user.Id,
                ProductId = product.Id,
                Quantity = 1,
                OrderDate = DateTime.Now
            };

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerName,ProductName,Quantity,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                _orderRepository.Add(order);
                _orderRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderRepository.FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}