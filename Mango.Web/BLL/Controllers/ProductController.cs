using Mango.Web.BLL.Services.IServices;
using Mango.Web.DAL.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.BLL.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductService _productSerivce;

		public ProductController(IProductService productSerivce)
		{
			_productSerivce = productSerivce;
		}

		[HttpGet]
		public async Task<IActionResult> ProductIndex()
		{
			var productList = new List<ProductDto>();
			var response = await _productSerivce.GetAllProductsAsync();

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
		public async Task<IActionResult> ProductCreate()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ProductCreate(ProductDto productDto)
		{
			if (ModelState.IsValid)
			{
				var response = await _productSerivce.CreateProductAsync(productDto);

				if (response != null && response.IsSuccess)
				{
					TempData["success"] = "Product created successfully";
					return RedirectToAction(nameof(ProductIndex));
				}
				else
				{
					TempData["error"] = response?.Message;
				}
			}

			return View(productDto);
		}

		[HttpGet]
		public async Task<IActionResult> ProductUpdate(int productId)
		{
            var response = await _productSerivce.GetProductAsync(productId);

            if (response != null && response.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return NotFound();
        }

		[HttpPost]
		public async Task<IActionResult> ProductUpdate(ProductDto productDto)
		{
            var response = await _productSerivce.UpdateProductAsync(productDto);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Product updated successfully";
                return RedirectToAction(nameof(ProductIndex));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return NotFound(productDto);
        }

		[HttpGet]
		public async Task<IActionResult> ProductDelete(int productId)
		{
			var response = await _productSerivce.GetProductAsync(productId);

			if (response != null && response.IsSuccess)
			{
				var model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
				return View(model);
			}
			else
			{
				TempData["error"] = response?.Message;
			}

			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> ProductDelete(ProductDto productDto)
		{
			var response = await _productSerivce.DeleteProductAsync(productDto.Id);

			if (response != null && response.IsSuccess)
			{
				TempData["success"] = "Product deleted successfully";
				return RedirectToAction(nameof(ProductIndex));
			}
			else
			{
				TempData["error"] = response?.Message;
			}

			return NotFound(productDto);
		}
	}
}
