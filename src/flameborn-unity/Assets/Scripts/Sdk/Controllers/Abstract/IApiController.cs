using System;
using flameborn.Sdk.Requests.Abstract;

namespace flameborn.Sdk.Controllers.Abstract
{
    public interface IApiController<T> where T : IApiResponse
    {
        void SendRequest(out string errorLog, params Action<T>[] listeners);
    }
}
 