using Mango.Services.ProductAPI.DAL.Models.Dtos;
using Mango.Services.ProductAPI.DAL.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.BLL.Controllers
{
	[Route("api/product")]
	[ApiController]
	[Authorize]
	public class ProductAPIController : Controller
	{
		private readonly IProductRepository _productRepo;
		private ResponseDto _response;

		public ProductAPIController(IProductRepository productRepo)
		{
			_productRepo = productRepo;
			_response = new();
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			try
			{
				var products = await _productRepo.GetProductsAsync();

				_response.Result = products;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;

				return BadRequest(_response);
			}
		}

		[HttpGet]
		[Route("{id:int}")]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				var product = await _productRepo.GetProductByIdAsync(id);

				_response.Result = product;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;

				return BadRequest(_response);
			}
		}

		[HttpGet]
		[Route("GetByName/{name}")]
		public async Task<IActionResult> Get(string name)
		{
			try
			{
				var product = await _productRepo.GetProductByNameAsync(name);

				_response.Result = product;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;

				return BadRequest(_response);
			}
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Post([FromBody] ProductDto productDto)
		{
			try
			{
				var product = await _productRepo.CreateProductAsync(productDto);

				_response.Result = product;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;

				return BadRequest(_response);
			}
		}

		[HttpPut]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Put([FromBody] ProductDto productDto)
		{
			try
			{
				var product = await _productRepo.UpdateProductAsync(productDto);

				_response.Result = product;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;

				return BadRequest(_response);
			}
		}

		[HttpDelete]
		[Route("{id:int}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				await _productRepo.DeleteProductAsync(id);

				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;

				return BadRequest(_response);
			}
		}
	}
}
