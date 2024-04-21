using Mango.Services.CouponAPI.DAL.Models.Dto;

namespace Mango.Services.CouponAPI.DAL.Repositories.IRepositories
{
    public interface ICouponRepository
    {
        Task<CouponDto> CreateCoupon(CouponDto couponDto);

        Task<IEnumerable<CouponDto>> GetCoupons();
        Task<CouponDto> GetCouponById(int couponId);
        Task<CouponDto> GetCouponByCode(string couponCode);

        Task<CouponDto> UpdateCounpon(CouponDto couponDto);
        Task DeleteCoupon(int couponId);
    }
}
