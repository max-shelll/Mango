using Mango.Services.ShoppingCartAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ShoppingCartAPI.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<CartHeader> CartHeaders { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
