using Mango.Services.CouponAPI.DAL.Models.Dto;
using Mango.Services.CouponAPI.DAL.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.BLL.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponAPIController : Controller
    {
        private readonly ICouponRepository _couponRepo;
        private ResponseDto _response;

        public CouponAPIController(ICouponRepository couponRepo)
        {
            _couponRepo = couponRepo;
            _response = new();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var coupons = await _couponRepo.GetCoupons();

                _response.Result = coupons;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

                return BadRequest(_response);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var coupon = await _couponRepo.GetCouponById(id);

                _response.Result = coupon;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

                return BadRequest(_response);
            }
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public async Task<IActionResult> Get(string code)
        {
            try
            {
                var coupon = await _couponRepo.GetCouponByCode(code);

                _response.Result = coupon;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

                return BadRequest(_response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CouponDto couponDto)
        {
            try
            {
                var coupon = await _couponRepo.CreateCoupon(couponDto);

                _response.Result = coupon;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

                return BadRequest(_response);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CouponDto couponDto)
        {
            try
            {
                var coupon = await _couponRepo.UpdateCounpon(couponDto);

                _response.Result = coupon;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

                return BadRequest(_response);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _couponRepo.DeleteCoupon(id);

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

                return BadRequest(_response);
            }
        }
    }
}
