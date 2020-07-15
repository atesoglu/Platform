using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Platform.Model.HealthCheck;
using Platform.Model.Runtime.Param;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Platform.Web.Runtime.Extensions
{
    public static class HealthCheckBuilderExtensions
    {
        public static IHealthChecksBuilder AddApplicationHealthChecks(this IHealthChecksBuilder builder, IParamsCollection paramsCollection)
        {
            //builder
            //.AddSqlServer(paramsCollection["DbContext"], name: "DbContext Connectivity", failureStatus: HealthStatus.Unhealthy)
            ;

            return builder;
        }

        public static HealthCheckOptions CreateOptions()
        {
            var options = new HealthCheckOptions
            {
                AllowCachingResponses = false,
                ResultStatusCodes = new Dictionary<HealthStatus, int>
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status500InternalServerError,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
                },
                ResponseWriter = UnifyHealthCheckResponse
            };

            return options;
        }

        public static Task UnifyHealthCheckResponse(HttpContext httpContext, HealthReport result)
        {
            httpContext.Response.ContentType = "application/json";

            var response = new HealthCheckResponse
            {
                Overall = result.Status.ToString(),
                TotalDuration = result.TotalDuration
            };

            result.Entries.ToList().ForEach(entry =>
            {
                response.Dependencies.Add(new HealthCheckEntry(entry.Key, entry.Value.Status.ToString(), entry.Value.Duration));
            });

            return httpContext.Response.WriteAsync(JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}