using Mango.Web.BLL.Services;
using Mango.Web.BLL.Services.IServices;
using Mango.Web.BLL.Utilities;

namespace Mango.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            SD.CouponAPIBase = builder.Configuration["ServiceUrls:CouponAPI"];

            // Add services to the container.

            builder.Services.AddHttpClient<ICouponService, CouponService>();

            builder.Services.AddScoped<IBaseService, BaseService>();
            builder.Services.AddScoped<ICouponService, CouponService>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
