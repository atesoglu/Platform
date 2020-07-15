using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Platform.Model.Runtime.Response;
using System.Threading.Tasks;

namespace Platform.Web.Runtime.Filters
{
    public class HttpResponseExceptionFilter : IAsyncExceptionFilter
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly ILogger<HttpResponseExceptionFilter> logger;

        public HttpResponseExceptionFilter(IWebHostEnvironment hostingEnvironment, ILogger<HttpResponseExceptionFilter> logger)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.logger = logger;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            //if (!hostingEnvironment.IsDevelopment()) { return; }

            var response = new ErrorResponseModel();

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)response.StatusCode;

            await context.HttpContext.Response.WriteAsync(response.ToJsonString());

            return;


            var result = new ViewResult { ViewName = "CustomError" };

            //result.ViewData = new ViewDataDictionary();
            //result.ViewData.Add("Exception", context.Exception);
            // TODO: Pass additional detailed data via ViewData

            context.Result = result;
        }
    }
}