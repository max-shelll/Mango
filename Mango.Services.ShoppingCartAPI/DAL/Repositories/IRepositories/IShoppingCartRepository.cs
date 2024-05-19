using Mango.Services.ShoppingCartAPI.DAL.Models;
using Mango.Services.ShoppingCartAPI.DAL.Models.Dtos;

namespace Mango.Services.ShoppingCartAPI.DAL.Repositories.IRepositories
{
    public interface IShoppingCartRepository
    {
        Task<CartHeaderDto> CreateCartHeaderAsync(CartHeaderDto cartHeaderDto);
        Task<CartDetailsDto> CreateCartDetailsAsync(CartDetailsDto cartDetailsDto);

        Task<CartHeaderDto> GetCartHeaderByUserIdAsync(string? userId);
        Task<CartHeaderDto> GetCartHeaderByIdAsync(int cartHeaderId);
        Task<CartDetailsDto> GetCartDetailsByIdAsync(int cartDetailsId);
        Task<CartDetailsDto> GetCartDetailsByProductAndHeaderIdAsync(int? productId, int? headerId);
        Task<IEnumerable<CartDetailsDto>> GetCartDetailsByHeaderIdAsync(int cartHeaderId);
        int GetTotalCountOfCartItem(int cartHeaderId);

        Task<CartHeaderDto> UpdateCartHeaderAsync(CartHeaderDto cartHeaderDto);
        Task<CartDetailsDto> UpdateCartDetailsAsync(CartDetailsDto cartDetailsDto);

        Task DeleteCartHeaderAsync(CartHeaderDto cartHeaderDto);
        Task DeleteCartDetailsAsync(CartDetailsDto cartDetailsDto);
    }
}
