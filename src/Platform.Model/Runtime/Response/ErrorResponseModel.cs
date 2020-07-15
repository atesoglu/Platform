using System;
using System.Net;
using System.Text.Json;

namespace Platform.Model.Runtime.Response
{
    public class ErrorResponseModel
    {
        public string UId { get; set; }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;
        public string Message { get; set; } = "An unexpected error occurred.";

        public ErrorResponseModel()
        {
            UId = Guid.NewGuid().ToString("N");
        }

        public string ToJsonString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}