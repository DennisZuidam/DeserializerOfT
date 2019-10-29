using JSONDeserializerOfT.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace JSONDeserializerOfT.Response
{
    public class Response<T> : IResponse<T>
    {
        internal Response(HttpStatusCode code, T objectT)
            : this((int)code, objectT)
        {
        }

        internal Response(int code, T objectT)
        {
            Code = code;
            Object = objectT;
        }

        public int Code { get; }
        public T Object { get; }
    }
}
