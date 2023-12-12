using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using WebAplicationTestMVC.Services;
using WebAplicationTestMVC.Interface;
using WebAplicationTestMVC.Repository;
using WebAplicationTestMVC.Middleware;

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

                        services.AddScoped<IFlashcardRepository, FlashcardRepository>();
                        services.AddScoped<IStudySetRepository, StudySetRepository>();
                        services.AddScoped<IFlashcardService, FlashcardService>();
                        services.AddScoped<IStudySetService, StudySetService>();
                        services.AddScoped<IExcelService, ExcelService>();
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
                        app.UseMiddleware<DatabaseMiddleware>();

                        // Use the database middleware here
                        app.UseDatabaseMiddleware();

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
