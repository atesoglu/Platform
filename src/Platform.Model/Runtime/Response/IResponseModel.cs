using System.Collections.Generic;

namespace Platform.Model.Runtime.Response
{
    public interface IResponseModel
    {
        string UId { get; }
        bool Success { get; }
        string Message { get; }

        int Total { get; }

        ICollection<KeyValuePair<string, string>> Errors { get; set; }
        ICollection<KeyValuePair<string, string>> Params { get; set; }
    }
}