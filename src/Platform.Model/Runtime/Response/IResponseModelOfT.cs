using System;
using System.Collections.Generic;
using System.Text;

namespace Platform.Model.Runtime.Response
{
    public interface IResponseModel<out T> : IResponseModel
    {
        T Data { get; }
        bool Valid { get; }

        IResponseModel<T> AddError(string error);
        IResponseModel<T> AddError(string header, string body);

        IResponseModel<T> AddParam(string key, string value);

        void Assign(IResponseModel model);
    }
}