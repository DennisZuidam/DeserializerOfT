using JSONDeserializerOfT.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace JSONDeserializerOfT.Response
{
    public class ErrorMessage<T> : IErrorMessage<T>
    {
        internal ErrorMessage(HttpStatusCode code, string error)
            : this((int)code, error)
        {
        }

        internal ErrorMessage(int code, string error)
        {
            Code = code;
            Error = error;
        }

        public int Code { get; }
        public string Error { get; }

        public string OriginalData { get; internal set; }
        public Exception Exception { get; internal set; }
        public bool HasException => Exception != null;

        T IResponse<T>.Object => default;
    }
}
