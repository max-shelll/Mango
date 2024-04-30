using Mango.Web.BLL.Services.IServices;
using Mango.Web.BLL.Utilities;
using Mango.Web.DAL.Enums;
using Mango.Web.DAL.Models.Dtos;
using Mango.Web.DAL.Models.Dtos.Request;
using Mango.Web.DAL.Models.Dtos.Response;

namespace Mango.Web.BLL.Services
{
	public class ProductService : IProductService
	{
		private readonly IBaseService _baseService;

		public ProductService(IBaseService baseService)
		{
			_baseService = baseService;
		}

		public async Task<ResponseDto?> GetAllProductsAsync()
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.GET,
				Url = SD.ProductAPIBase + "/api/product"
			});
		}

		public async Task<ResponseDto?> GetProductAsync(int id)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.GET,
				Url = SD.ProductAPIBase + $"/api/product/{id}"
			});
		}

		public async Task<ResponseDto?> GetProductAsync(string name)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.GET,
				Url = SD.ProductAPIBase + $"/api/product/{name}"
			});
		}

		public async Task<ResponseDto?> CreateProductAsync(ProductDto productDto)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.POST,
				Url = SD.ProductAPIBase + "/api/product",
				Data = productDto
			});
		}

		public async Task<ResponseDto?> UpdateProductAsync(ProductDto productDto)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.PUT,
				Url = SD.ProductAPIBase + "/api/product",
				Data = productDto
			});
		}

		public async Task<ResponseDto?> DeleteProductAsync(int id)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.DELETE,
				Url = SD.ProductAPIBase + $"/api/product/{id}"
			});
		}
	}
}
