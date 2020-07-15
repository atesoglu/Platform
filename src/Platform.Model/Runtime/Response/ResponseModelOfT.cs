using System;
using System.Collections.Generic;
using System.Text;

namespace Platform.Model.Runtime.Response
{
    public class ResponseModel<T> : ResponseModel, IResponseModel<T>
    {
        public T Data { get; set; }
        public bool Valid => Success && Data != null;

        public ResponseModel()
        {
            UId = Guid.NewGuid().ToString("N");
        }

        public ResponseModel(IResponseModel model)
        {
            Assign(model);
        }

        public IResponseModel<T> AddError(string error)
        {
            return AddError(null, error);
        }
        public virtual IResponseModel<T> AddError(string header, string body)
        {
            Errors = Errors ?? new List<KeyValuePair<string, string>>();

            Errors.Add(new KeyValuePair<string, string>(header, body));

            Message = Message ?? "An error occured while processing your request.";

            return this;
        }
        public virtual IResponseModel<T> AddParam(string key, string value)
        {
            Params = Params ?? new List<KeyValuePair<string, string>>();

            Params.Add(new KeyValuePair<string, string>(key, value));

            return this;
        }

        public void Assign(IResponseModel model)
        {
            UId = model.UId;
            //Total = model.Total;
            Message = model.Message;
            Errors = model.Errors;
            Params = model.Params;
        }
    }
}