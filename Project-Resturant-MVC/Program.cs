using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_Resturant_MVC.Context;
using Project_Resturant_MVC.Models;

namespace Project_Resturant_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ResturantDbContext>(options =>
                 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.Password.RequiredLength = 6;
                option.Password.RequireDigit =false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;

            })
                .AddEntityFrameworkStores<ResturantDbContext>();


            // Add services to the container.
            builder.Services.AddControllersWithViews();


            //builder.Services.Configure<FormOptions>(options =>
            //{
            //    options.MultipartBodyLengthLimit = 10485760; // 10 MB
            //    options.ValueLengthLimit = int.MaxValue;
            //    options.MultipartHeadersLengthLimit = int.MaxValue;
            //});


            var app = builder.Build();

            app.MapControllers();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");



            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
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
