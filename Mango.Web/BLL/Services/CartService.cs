using Mango.Web.BLL.Services.IServices;
using Mango.Web.BLL.Utilities;
using Mango.Web.DAL.Enums;
using Mango.Web.DAL.Models.Dtos;
using Mango.Web.DAL.Models.Dtos.Request;
using Mango.Web.DAL.Models.Dtos.Response;

namespace Mango.Web.BLL.Services
{
    public class CartService : ICartService
	{
		private readonly IBaseService _baseService;

		public CartService(IBaseService baseService)
		{
			_baseService = baseService;
		}

        public async Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + "/api/cart/ApplyCoupon"
            });
        }

        public async Task<ResponseDto?> GetCartByUserIdAsync(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = SD.ShoppingCartAPIBase + $"/api/cart/GetCart/{userId}"
            }, withBearer: false);
        }

        public async Task<ResponseDto?> RemoveFromCartAsync(int cartDetailsId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = cartDetailsId,
                Url = SD.ShoppingCartAPIBase + "/api/cart/RemoveCart"
            });
        }

        public async Task<ResponseDto?> UpsertCartAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + "/api/cart/CartUpsert"
            });
        }
    }
}
