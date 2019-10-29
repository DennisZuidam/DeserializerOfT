using System;
using System.Collections.Generic;
using System.Text;

namespace JSONDeserializerOfT.Interfaces
{
    public interface IErrorMessage<out T> : IResponse<T>
    {
        string Error { get; }
        string OriginalData { get; }
        Exception Exception { get; }
        bool HasException { get; }
    }
}
