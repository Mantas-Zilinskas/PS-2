using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore; // Add this line for Entity Framework Core
using WebAplicationTestMVC.Models; // Add this line for your DbContext
using WebAplicationTestMVC.Services;

namespace WebAplicationTestMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
     Host.CreateDefaultBuilder(args)
         .ConfigureWebHostDefaults(webBuilder =>
         {
             webBuilder.ConfigureServices((hostContext, services) =>
             {
                 services.AddDistributedMemoryCache(); 
                 services.AddSession(options =>
                 {
                     options.IdleTimeout = TimeSpan.FromMinutes(30);
                     options.Cookie.HttpOnly = true;
                     options.Cookie.IsEssential = true;
                 });

                
                 services.AddDbContext<ApplicationDbContext>(options =>
                     options.UseSqlite(hostContext.Configuration.GetConnectionString("DefaultConnection")));

                
                 services.AddControllersWithViews();
                 services.AddScoped<EntityFrameworkService>(); 
             })
             .Configure(app =>
             {
                 var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

                
                 if (env.IsDevelopment())
                 {
                     app.UseDeveloperExceptionPage();
                 }
                 else
                 {
                     app.UseExceptionHandler("/Home/Error");
                     app.UseHsts();
                 }

                 app.UseHttpsRedirection();
                 app.UseStaticFiles();
                 app.UseRouting();
                 app.UseAuthorization();

                 app.UseSession();

                 app.UseEndpoints(endpoints =>
                 {
                     endpoints.MapControllerRoute(
                         name: "default",
                         pattern: "{controller=Home}/{action=Index}/{id?}");
                 });
             });
         });

    }
}
