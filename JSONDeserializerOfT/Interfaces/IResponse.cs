using System;
using System.Collections.Generic;
using System.Text;

namespace JSONDeserializerOfT.Interfaces
{
    public interface IResponse<out T>
    {
        int Code { get; }
        T Object { get; }
    }
}
