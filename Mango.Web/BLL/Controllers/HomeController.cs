using IdentityModel;
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
        private readonly ICartService _cartService;

        public HomeController(IProductService productService, ICartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
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
        public async Task<IActionResult> ProductDetails(int productId)
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ProductDetails(ProductDto productDto)
        {
            var cartDto = new CartDto()
            {
                CartHeader = new()
                {
                    UserId = User.Claims.Where(u => u.Type == JwtClaimTypes.Subject)?.FirstOrDefault()?.Value
                }
            };

            var cartDetails = new CartDetailsDto()
            {
                Count = productDto.Count,
                ProductId = productDto.Id
            };

            var cartDetailDtos = new List<CartDetailsDto>() { cartDetails };

            cartDto.CartDetails = cartDetailDtos;

            var response = await _cartService.UpsertCartAsync(cartDto);

            if (response.IsSuccess)
            {
                TempData["success"] = "Item has been added to the Shopping cart";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(productDto);
        }
    }
}
