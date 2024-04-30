using Mango.Services.CouponAPI.DAL.Models.Dtos;
using Mango.Services.CouponAPI.DAL.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.BLL.Controllers
{
	[Route("api/coupon")]
	[ApiController]
	[Authorize]
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
				var coupons = await _couponRepo.GetCouponsAsync();

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
				var coupon = await _couponRepo.GetCouponByIdAsync(id);

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
				var coupon = await _couponRepo.GetCouponByCodeAsync(code);

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
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Post([FromBody] CouponDto couponDto)
		{
			try
			{
				var coupon = await _couponRepo.CreateCouponAsync(couponDto);

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
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Put([FromBody] CouponDto couponDto)
		{
			try
			{
				var coupon = await _couponRepo.UpdateCounponAsync(couponDto);

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
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				await _couponRepo.DeleteCouponAsync(id);

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
