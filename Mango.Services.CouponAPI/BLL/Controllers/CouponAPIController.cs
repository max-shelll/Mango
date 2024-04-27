using Mango.Services.CouponAPI.DAL.Models.Dto;
using Mango.Services.CouponAPI.DAL.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.BLL.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponAPIController : Controller
    {
        private readonly ICouponRepository _couponRepository;
        private ResponseDto _response;

        public CouponAPIController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
            _response = new();
        }

        [HttpGet]
        public async Task<ResponseDto> Get()
        {
            try
            {
                var coupons = await _couponRepository.GetCoupons();
                _response.Result = coupons;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ResponseDto> Get(int id)
        {
            try
            {
                var coupon = await _couponRepository.GetCouponById(id);
                _response.Result = coupon;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public async Task<ResponseDto> Get(string code)
        {
            try
            {
                var coupon = await _couponRepository.GetCouponByCode(code);
                _response.Result = coupon;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPost]
        public async Task<ResponseDto> Post([FromBody] CouponDto couponDto)
        {
            try
            {
                var coupon = await _couponRepository.CreateCoupon(couponDto);
                _response.Result = coupon;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPut]
        public async Task<ResponseDto> Put([FromBody] CouponDto couponDto)
        {
            try
            {
                var coupon = await _couponRepository.UpdateCounpon(couponDto);
                _response.Result = coupon;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ResponseDto> Delete(int id)
        {
            try
            {
                await _couponRepository.DeleteCoupon(id);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }
    }
}
