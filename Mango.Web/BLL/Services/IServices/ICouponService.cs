using Mango.Web.DAL.Models.Dtos;
using Mango.Web.DAL.Models.Dtos.Response;

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
