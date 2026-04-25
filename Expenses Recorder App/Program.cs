using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Expenses_Recorder_App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<Models.Application_Models.ApplicationDbContext.ApplicationDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("RemoteConnection"))); // Configure the DbContext with SQL Server and connection string

            builder.Services.AddSession();


            var app = builder.Build();
            app.UseSession();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=User}/{action=UserLogin}/{id?}");

            app.Run();
        }
    }
}
