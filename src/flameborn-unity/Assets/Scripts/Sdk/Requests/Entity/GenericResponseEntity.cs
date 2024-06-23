using System;
using flameborn.Sdk.Requests.Abstract;

namespace flameborn.Sdk.Requests.Entity
{
    /// <summary>
    /// Represents a generic response entity with custom data.
    /// </summary>
    /// <typeparam name="T">The type of custom data associated with the response.</typeparam>
    [Serializable]
    public abstract class GenericResponseEntity<T> : IApiResponse
    {
        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the request was successful.
        /// </summary>
        public bool IsRequestSuccess { get; set; } = false;

        /// <summary>
        /// Gets or sets the type of the response.
        /// </summary>
        public Type ResponseType { get; set; } = default;

        /// <summary>
        /// Gets or sets the response object.
        /// </summary>
        public object Response { get; set; } = null;

        /// <summary>
        /// Gets or sets the custom data associated with the response.
        /// </summary>
        public T CustomData { get; set; } = default;

        /// <summary>
        /// Gets or sets the response message.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericResponseEntity{T}"/> class.
        /// </summary>
        /// <param name="customData">The custom data associated with the response.</param>
        public GenericResponseEntity(T customData)
        {
            CustomData = customData;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the response details.
        /// </summary>
        /// <typeparam name="V">The type of the response object.</typeparam>
        /// <param name="isSuccess">Indicates if the response is successful.</param>
        /// <param name="response">The response object.</param>
        /// <param name="message">The message associated with the response.</param>
        public abstract void SetResponse<V>(bool isSuccess, V response, string message = "");

        #endregion
    }
}
