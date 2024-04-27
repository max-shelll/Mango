using Mango.Web.DAL.Models.Dto;

namespace Mango.Web.BLL.Services.IServices
{
    public interface ICouponService
    {
        Task<ResponseDto?> GetAllCouponsAsync();
        Task<ResponseDto?> GetCouponAsync(int id);
        Task<ResponseDto?> GetCouponAsync(string couponCode);
        Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto);
        Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto);
        Task<ResponseDto?> DeleteCouponAsync(int id);
    }
}
