using AutoMapper;
using Mango.Services.CouponAPI.DAL.Models;
using Mango.Services.CouponAPI.DAL.Models.Dto;
using Mango.Services.CouponAPI.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.DAL.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly AppDbContext _db;
        protected IMapper _mapper;

        public CouponRepository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CouponDto> CreateCoupon(CouponDto couponDto)
        {
            var coupon = _mapper.Map<Coupon>(couponDto);

            _db.Coupons.Add(coupon);

            await _db.SaveChangesAsync();
            return _mapper.Map<CouponDto>(coupon);
        }

        public async Task<IEnumerable<CouponDto>> GetCoupons()
        {
            var couponList = await _db.Coupons.ToListAsync();

            return _mapper.Map<List<CouponDto>>(couponList);
        }

        public async Task<CouponDto> GetCouponById(int couponId)
        {
            var coupon = await _db.Coupons.FirstAsync(i => i.CouponId == couponId);

            return _mapper.Map<CouponDto>(coupon);
        }

        public async Task<CouponDto> GetCouponByCode(string couponCode)
        {
            var coupon = await _db.Coupons.FirstAsync(i => i.CouponCode.ToLower() == couponCode.ToLower());

            return _mapper.Map<CouponDto>(coupon);
        }

        public async Task<CouponDto> UpdateCounpon(CouponDto couponDto)
        {
            var coupon = _mapper.Map<Coupon>(couponDto);

            _db.Coupons.Update(coupon);

            await _db.SaveChangesAsync();
            return _mapper.Map<CouponDto>(coupon);
        }

        public async Task DeleteCoupon(int couponId)
        {
            var coupon = await _db.Coupons.FirstAsync(i => i.CouponId == couponId);

            _db.Coupons.Remove(coupon);

            await _db.SaveChangesAsync();
        }
    }
}
