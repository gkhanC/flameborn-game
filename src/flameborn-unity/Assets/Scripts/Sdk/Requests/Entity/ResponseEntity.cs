using System;
using flameborn.Sdk.Requests.Abstract;

namespace flameborn.Sdk.Requests.Entity
{
    [Serializable]
    public abstract class ResponseEntity : IApiResponse
    {
        public bool IsRequestSuccess { get; set; } = false;
        public Type ResponseType { get; set; } = default;
        public object Response { get; set; } = null;
        public string Message { get; set; } = string.Empty;

        protected ResponseEntity()
        {

        }

        public virtual void SetResponse<T>(bool isSuccess, T response, string message = "")
        {
            IsRequestSuccess = isSuccess;
            ResponseType = typeof(T);
            Response = response;
            Message = message;
        }
    }
}