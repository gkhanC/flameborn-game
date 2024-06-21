using System;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.Abstract;

namespace flameborn.Sdk.Requests
{
    public abstract class Request<T> : IApiRequest<T> where T : IApiResponse
    {
        protected IApiController<T> Controller { get; set; } = null;
        public abstract void SendRequest(out string errorLog, params Action<T>[] listeners);
        protected Request(IApiController<T> controller)
        {
            Controller = controller;
        }
    }
}