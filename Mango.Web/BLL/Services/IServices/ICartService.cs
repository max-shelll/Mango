using Mango.Web.DAL.Models.Dtos;
using Mango.Web.DAL.Models.Dtos.Response;

namespace Mango.Web.BLL.Services.IServices
{
	public interface ICartService
    {
		Task<ResponseDto?> GetCartByUserIdAsync(string userId);
		Task<ResponseDto?> UpsertCartAsync(CartDto cartDto);
		Task<ResponseDto?> RemoveFromCartAsync(int cartDetailsId);
		Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto);
	}
}
