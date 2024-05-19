using Mango.Services.ShoppingCartAPI.DAL.Models.Dtos;

namespace Mango.Services.ShoppingCartAPI.BLL.Services.IServices
{
    public interface ICouponService
    {
        Task<CouponDto> GetCouponAsync(string couponCode);
    }
}
