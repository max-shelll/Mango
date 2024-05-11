using Mango.Web.BLL.Services.IServices;
using Mango.Web.DAL.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.BLL.Controllers
{
	public class HomeController : Controller
	{
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

		[HttpGet]
		public async Task<IActionResult> Index()
		{
            var productList = new List<ProductDto>();
            var response = await _productService.GetAllProductsAsync();

            if (response.IsSuccess)
            {
                productList = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(productList);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details(int productId)
        {
            var product = new ProductDto();
            var response = await _productService.GetProductAsync(productId);

            if (response.IsSuccess)
            {
                product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(product);
        }
    }
}
