using AutoMapper;
using Mango.Services.AuthAPI.BLL;
using Mango.Services.AuthAPI.BLL.Services;
using Mango.Services.AuthAPI.BLL.Services.IServices;
using Mango.Services.AuthAPI.DAL;
using Mango.Services.AuthAPI.DAL.Models.Config;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.AuthAPI
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

            // Add configurations

            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));

            // Add services to the container.

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddSingleton(mapper);

            builder.Services
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

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

            app.UseAuthentication();
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
