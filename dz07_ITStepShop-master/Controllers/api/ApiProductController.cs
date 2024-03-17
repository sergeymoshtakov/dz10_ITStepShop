using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Repository.IRepository;
using System.Linq;
using System.Threading.Tasks;

namespace ITStepShop.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = $"{WC.AdminRole}")]
    public class ApiProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ApiProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepository.GetAllAsync(includeProperties: "Category");
            return View("AllProducts", products); // Зміна тут: повертаємо перегляд з даними
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productRepository.FirstOrDefaultAsync(p => p.Id == id, includeProperties: "Category");

            if (product == null)
            {
                return NotFound();
            }

            return View("ProductDetail", product); // Зміна тут: повертаємо перегляд з даними
        }

        // GET: api/Product/search?name=example
        [HttpGet("search")]
        public async Task<IActionResult> SearchProductByName([FromQuery] string name)
        {
            var products = await _productRepository.GetAllAsync("Category");

            if (!products.Any())
            {
                return NotFound();
            }

            return View("SearchResults", products); // Зміна тут: повертаємо перегляд з даними
        }
    }
}