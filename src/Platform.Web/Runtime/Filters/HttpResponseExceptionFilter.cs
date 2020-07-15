using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Platform.Model.Runtime.Response;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Platform.Web.Runtime.Filters
{
    public class HttpResponseExceptionFilter : IAsyncExceptionFilter
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IMemoryCache memoryCache;
        private readonly ILogger<HttpResponseExceptionFilter> logger;

        public HttpResponseExceptionFilter(IWebHostEnvironment hostingEnvironment, IMemoryCache memoryCache, ILogger<HttpResponseExceptionFilter> logger)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.memoryCache = memoryCache;
            this.logger = logger;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            //if (!hostingEnvironment.IsDevelopment()) { return; }

            var response = new ErrorResponseModel
            {
                Message = "An unexpected error occurred.",
                StatusCode = HttpStatusCode.InternalServerError
            };

            memoryCache.Set($"error-uid:{response.UId}", response.ToJsonString());

            var accept = context.HttpContext.Request.Headers["Accept"];
            var contentType = context.HttpContext.Request.Headers["Content-Type"];

            if (contentType.Any(a => a == "application/json") || !accept.Any(a => a.Contains("text/html")))
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)response.StatusCode;

                await context.HttpContext.Response.WriteAsync(response.ToJsonString());

                return;
            }

            context.Result = new RedirectToActionResult("Error", "Home", new { uid = response.UId });
        }
    }
}