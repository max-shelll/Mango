using Mango.Web.DAL.Models.Dtos;
using Mango.Web.DAL.Models.Dtos.Response;

namespace Mango.Web.BLL.Services.IServices
{
	public interface IProductService
	{
		Task<ResponseDto?> GetAllProductsAsync();
		Task<ResponseDto?> GetProductAsync(int id);
		Task<ResponseDto?> GetProductAsync(string name);
		Task<ResponseDto?> CreateProductAsync(ProductDto productDto);
		Task<ResponseDto?> UpdateProductAsync(ProductDto productDto);
		Task<ResponseDto?> DeleteProductAsync(int id);
	}
}
