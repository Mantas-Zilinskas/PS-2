using Xunit;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using WebAplicationTestMVC.Middleware;
using WebAplicationTestMVC.Models;
using Microsoft.Extensions.DependencyInjection;
using Assert = Xunit.Assert;

namespace WebAplicationTestMVCTests.Middleware
{
    public class DatabaseMiddlewareTests
    {
        [Fact]
        public async Task InvokeAsync_ShouldSetStudySetCountInContext()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase(databaseName: "TestDatabase");
            });

            var serviceProvider = services.BuildServiceProvider();

            // Seed the in-memory database with test data
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.StudySets.Add(new StudySet("Mathematics"));
                dbContext.StudySets.Add(new StudySet("Science"));
                dbContext.SaveChanges();
            }

            var httpContext = new DefaultHttpContext();
            httpContext.RequestServices = serviceProvider;

            var loggerMock = new Mock<ILogger<DatabaseMiddleware>>();
            var nextDelegate = new RequestDelegate(context => Task.CompletedTask);

            var middleware = new DatabaseMiddleware(nextDelegate, loggerMock.Object);

            // Act
            await middleware.InvokeAsync(httpContext);

            // Assert
            Assert.True(httpContext.Items.ContainsKey("StudySetCount"));
            Assert.Equal(2, httpContext.Items["StudySetCount"]);
        }
    }
}