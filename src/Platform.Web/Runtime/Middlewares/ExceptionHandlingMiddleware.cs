using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Platform.Model.Runtime.Exceptions;
using Platform.Model.Runtime.Response;
using System;
using System.Threading.Tasks;

namespace Platform.Web.Runtime.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try { await _next(context); }
            catch (Exception ex)
            {
                // Do some stuff...
                // Log exception

                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var response = new ErrorResponseModel();

            if (ex is HttpException httpException)
            {
                response.StatusCode = httpException.StatusCode;
                response.Message = httpException.Message;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)response.StatusCode;

            await context.Response.WriteAsync(response.ToJsonString());
        }
    }
}