using System;

namespace flameborn.Sdk.Requests.Abstract
{
    public interface IApiResponse
    {
        bool IsRequestSuccess { get; }
        Type ResponseType { get; }
        object Response { get; }
        string Message { get; }
        void SetResponse<T>(bool isSuccess, T response, string message = "");
    }
}