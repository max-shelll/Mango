using AutoMapper;
using Mango.Services.CouponAPI.DAL.Models;
using Mango.Services.CouponAPI.DAL.Models.Dtos;
using Mango.Services.CouponAPI.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.DAL.Repositories
{
	public class CouponRepository : ICouponRepository
	{
		private readonly AppDbContext _db;
		private readonly IMapper _mapper;

		public CouponRepository(AppDbContext db, IMapper mapper)
		{
			_db = db;
			_mapper = mapper;
		}

		public async Task<CouponDto> CreateCouponAsync(CouponDto couponDto)
		{
			var coupon = _mapper.Map<Coupon>(couponDto);

			_db.Coupons.Add(coupon);

			await _db.SaveChangesAsync();
			return _mapper.Map<CouponDto>(coupon);
		}

		public async Task<IEnumerable<CouponDto>> GetCouponsAsync()
		{
			var couponList = await _db.Coupons.ToListAsync();

			return _mapper.Map<List<CouponDto>>(couponList);
		}

		public async Task<CouponDto> GetCouponByIdAsync(int couponId)
		{
			var coupon = await _db.Coupons.FirstAsync(i => i.Id == couponId);

			return _mapper.Map<CouponDto>(coupon);
		}

		public async Task<CouponDto> GetCouponByCodeAsync(string couponCode)
		{
			var coupon = await _db.Coupons.FirstAsync(i => i.Code.ToLower() == couponCode.ToLower());

			return _mapper.Map<CouponDto>(coupon);
		}

		public async Task<CouponDto> UpdateCounponAsync(CouponDto couponDto)
		{
			var coupon = _mapper.Map<Coupon>(couponDto);

			_db.Coupons.Update(coupon);

			await _db.SaveChangesAsync();
			return _mapper.Map<CouponDto>(coupon);
		}

		public async Task DeleteCouponAsync(int couponId)
		{
			var coupon = await _db.Coupons.FirstAsync(i => i.Id == couponId);

			_db.Coupons.Remove(coupon);

			await _db.SaveChangesAsync();
		}
	}
}
