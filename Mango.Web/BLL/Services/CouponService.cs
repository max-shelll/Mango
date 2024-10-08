﻿using Mango.Web.BLL.Services.IServices;
using Mango.Web.BLL.Utilities;
using Mango.Web.DAL.Enums;
using Mango.Web.DAL.Models.Dtos;
using Mango.Web.DAL.Models.Dtos.Request;
using Mango.Web.DAL.Models.Dtos.Response;

namespace Mango.Web.BLL.Services
{
	public class CouponService : ICouponService
	{
		private readonly IBaseService _baseService;

		public CouponService(IBaseService baseService)
		{
			_baseService = baseService;
		}

		public async Task<ResponseDto?> GetAllCouponsAsync()
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.GET,
				Url = SD.CouponAPIBase + "/api/coupon"
			});
		}

		public async Task<ResponseDto?> GetCouponAsync(int id)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.GET,
				Url = SD.CouponAPIBase + $"/api/coupon/{id}"
			});
		}

		public async Task<ResponseDto?> GetCouponAsync(string couponCode)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.GET,
				Url = SD.CouponAPIBase + $"/api/coupon/GetByCode/{couponCode}"
			});
		}

		public async Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.POST,
				Url = SD.CouponAPIBase + $"/api/coupon",
				Data = couponDto
			});
		}

		public async Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.PUT,
				Url = SD.CouponAPIBase + $"/api/coupon",
				Data = couponDto
			});
		}

		public async Task<ResponseDto?> DeleteCouponAsync(int id)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.DELETE,
				Url = SD.CouponAPIBase + $"/api/coupon/{id}"
			});
		}
	}
}
