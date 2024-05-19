
using AutoMapper;
using Mango.Services.ShoppingCartAPI.BLL;
using Mango.Services.ShoppingCartAPI.BLL.Extensions;
using Mango.Services.ShoppingCartAPI.BLL.Services;
using Mango.Services.ShoppingCartAPI.BLL.Services.IServices;
using Mango.Services.ShoppingCartAPI.BLL.Utilities;
using Mango.Services.ShoppingCartAPI.DAL;
using Mango.Services.ShoppingCartAPI.DAL.Repositories;
using Mango.Services.ShoppingCartAPI.DAL.Repositories.IRepositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Mango.Services.ShoppingCartAPI
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
            builder.Services
                .AddScoped<IProductService, ProductService>()
                .AddScoped<ICouponService, CouponService>()
                .AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

            builder.Services.AddHttpClient("Product", u => u.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductAPI"]))
                .AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();
            builder.Services.AddHttpClient("Coupon", u => u.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CouponAPI"]))
                .AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();

            builder.Services.AddScoped<BackendApiAuthenticationHttpClientHandler>();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(name: "Bearer", securityScheme: new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        }, new string[] {}
                    }
                });
            });

            builder.AddAppAuthetication();

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
