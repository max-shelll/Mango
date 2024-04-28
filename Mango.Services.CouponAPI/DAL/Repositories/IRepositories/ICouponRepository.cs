using Mango.Services.CouponAPI.DAL.Models.Dto;

namespace Mango.Services.CouponAPI.DAL.Repositories.IRepositories
{
    public interface ICouponRepository
    {
        Task<CouponDto> CreateCouponAsync(CouponDto couponDto);

        Task<IEnumerable<CouponDto>> GetCouponsAsync();
        Task<CouponDto> GetCouponByIdAsync(int couponId);
        Task<CouponDto> GetCouponByCodeAsync(string couponCode);

        Task<CouponDto> UpdateCounponAsync(CouponDto couponDto);
        Task DeleteCouponAsync(int couponId);
    }
}
