﻿using AutoMapper;
using Mango.Services.CouponAPI.DAL.Models;
using Mango.Services.CouponAPI.DAL.Models.Dtos;

namespace Mango.Services.CouponAPI.BLL
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<CouponDto, Coupon>().ReverseMap();
		}
	}
}
