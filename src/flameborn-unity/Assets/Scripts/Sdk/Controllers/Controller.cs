using System;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.Abstract;

namespace flameborn.Sdk.Controllers
{
    public abstract class Controller<T> : IApiController<T> where T : IApiResponse
    {
        protected Controller()
        {
        }

        public abstract void SendRequest(out string errorLog, params Action<T>[] listeners);
    }
}