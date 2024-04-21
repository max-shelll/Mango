
using AutoMapper;
using Mango.Services.CouponAPI.BLL;
using Mango.Services.CouponAPI.DAL;
using Mango.Services.CouponAPI.DAL.Repositories;
using Mango.Services.CouponAPI.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var mapper = new MapperConfiguration((v) =>
            {
                v.AddProfile(new MappingProfile());
            }).CreateMapper();

            // Add services to the container.

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddSingleton(mapper);
            builder.Services.AddScoped<ICouponRepository, CouponRepository>();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            ApplyMigration(app);

            app.Run();
        }

        private static void ApplyMigration(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                if (_db.Database.GetPendingMigrations().Any())
                {
                    _db.Database.Migrate();
                }
            }
        }
    }
}
