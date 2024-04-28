using Mango.Services.CouponAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Coupon> Coupons { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
            Database.EnsureCreated();
        }
    }
}
