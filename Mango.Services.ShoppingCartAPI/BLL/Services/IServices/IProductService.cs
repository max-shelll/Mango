using Mango.Services.ShoppingCartAPI.DAL.Models.Dtos;

namespace Mango.Services.ShoppingCartAPI.BLL.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
    }
}
