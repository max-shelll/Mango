using Mango.Web.BLL.Services;
using Mango.Web.BLL.Services.IServices;
using Mango.Web.BLL.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Mango.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add configurations

            SD.CouponAPIBase = builder.Configuration["ServiceUrls:CouponAPI"];
            SD.AuthAPIBase = builder.Configuration["ServiceUrls:AuthAPI"];

            // Add services to the container.

            builder.Services.AddHttpClient<ICouponService, CouponService>();

            builder.Services
                .AddScoped<IBaseService, BaseService>()
                .AddScoped<ICouponService, CouponService>()
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<ITokenProvider, TokenProvider>();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromHours(10);
                    options.LoginPath = "/Auth/Login";
                    options.AccessDeniedPath = "/Auth/AccessDenied";
                });

            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
