using System;

namespace flameborn.Sdk.Requests.Abstract
{
    public interface IApiRequest<T> where T : IApiResponse
    {
        void SendRequest(out string errorLog, params Action<T>[] listeners);
    }
}