using Platform.Model.Runtime.Response;

namespace Platform.Model.View
{
    public class ErrorViewModel
    {
        public string UId { get; set; }
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public ErrorResponseModel ErrorResponse { get; set; }
    }
}