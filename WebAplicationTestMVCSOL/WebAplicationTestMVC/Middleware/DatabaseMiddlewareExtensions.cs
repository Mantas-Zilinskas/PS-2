namespace WebAplicationTestMVC.Middleware
{
    public static class DatabaseMiddlewareExtensions
    {
        public static IApplicationBuilder UseDatabaseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DatabaseMiddleware>();
        }
    }
}
