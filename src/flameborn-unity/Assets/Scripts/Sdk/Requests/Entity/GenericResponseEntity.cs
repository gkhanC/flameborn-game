using System;
using flameborn.Sdk.Requests.Abstract;

namespace flameborn.Sdk.Requests.Entity
{
    [Serializable]
    public abstract class GenericResponseEntity<T> : IApiResponse
    {
        public bool IsRequestSuccess { get; set; } = false;
        public Type ResponseType { get; set; } = default;
        public object Response { get; set; } = null;
        public T CustomData { get; set; } = default;
        public string Message { get; set; } = string.Empty;

        public GenericResponseEntity(T customData)
        {
            CustomData = customData;
        }

        public abstract void SetResponse<V>(bool isSuccess, V response, string message = "");
    }
}