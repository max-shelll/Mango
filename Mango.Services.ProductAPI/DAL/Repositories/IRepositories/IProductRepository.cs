using Mango.Services.ProductAPI.DAL.Models.Dtos;

namespace Mango.Services.ProductAPI.DAL.Repositories.IRepositories
{
	public interface IProductRepository
	{
		Task<ProductDto> CreateProductAsync(ProductDto productDto);

		Task<IEnumerable<ProductDto>> GetProductsAsync();
		Task<ProductDto> GetProductByIdAsync(int productId);
		Task<ProductDto> GetProductByNameAsync(string productName);

		Task<ProductDto> UpdateProductAsync(ProductDto productDto);
		Task DeleteProductAsync(int productId);
	}
}
