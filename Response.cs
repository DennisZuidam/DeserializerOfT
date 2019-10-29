using System;

public interface IResponse<out T>
{
    int Code { get; }
    T Object { get; }
}

public interface IErrorMessage<out T> : IResponse<T>
{
    string Error { get; }
    string OriginalData { get; }
    Exception Exception { get; }
    bool HasException { get; }
}

public class Response<T> : IResponse<T>
{
    internal Response(HttpStatusCode code, T objectT)
        : this((int)code, obejctT)
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
