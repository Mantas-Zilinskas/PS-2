using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using WebAplicationTestMVC.Models;
using Microsoft.EntityFrameworkCore; 
using Microsoft.Extensions.Logging; 

namespace WebAplicationTestMVC.Middleware
{
    public class DatabaseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<DatabaseMiddleware> _logger; 

        public DatabaseMiddleware(RequestDelegate next, ILogger<DatabaseMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                using (var scope = context.RequestServices.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    var studySetCount = await dbContext.StudySets.CountAsync();

                    _logger.LogInformation($"Number of StudySets: {studySetCount}");

                    context.Items["StudySetCount"] = studySetCount;
                }

                await _next(context);

                _logger.LogInformation("Request processed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during request processing.");
                throw; 
            }
        }
    }
}
