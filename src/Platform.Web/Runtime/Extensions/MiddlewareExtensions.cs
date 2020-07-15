using Microsoft.AspNetCore.Builder;
using Platform.Web.Runtime.Middlewares;

namespace Platform.Web.Runtime.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder ConfigureApplicationMiddlewares(this IApplicationBuilder app)
        {
            return app
                //.UseMiddleware<ExceptionHandlingMiddleware>()
                //.UseMiddleware<HealthCheckMiddleware>()
                ;
        }
    }
}