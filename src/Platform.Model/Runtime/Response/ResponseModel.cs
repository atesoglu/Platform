using System;
using System.Collections.Generic;
using System.Text;

namespace Platform.Model.Runtime.Response
{
    public class ResponseModel : IResponseModel
    {
        public string UId { get; set; }
        public bool Success => Errors == null || Errors.Count == 0;
        public string Message { get; set; }
        public int Total { get; set; }

        public ICollection<KeyValuePair<string, string>> Errors { get; set; }
        public ICollection<KeyValuePair<string, string>> Params { get; set; }

        public ResponseModel()
        {
            Errors = new List<KeyValuePair<string, string>>();
            Params = new List<KeyValuePair<string, string>>();
        }
    }
}