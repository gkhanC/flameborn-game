using System;
using flameborn.Sdk.Requests.Abstract;

namespace flameborn.Sdk.Requests.Entity
{
    /// <summary>
    /// Represents a base response entity.
    /// </summary>
    [Serializable]
    public abstract class ResponseEntity : IApiResponse
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
        /// Gets or sets the response message.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseEntity"/> class.
        /// </summary>
        protected ResponseEntity()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the response details.
        /// </summary>
        /// <typeparam name="T">The type of the response object.</typeparam>
        /// <param name="isSuccess">Indicates if the response is successful.</param>
        /// <param name="response">The response object.</param>
        /// <param name="message">The message associated with the response.</param>
        public virtual void SetResponse<T>(bool isSuccess, T response, string message = "")
        {
            IsRequestSuccess = isSuccess;
            ResponseType = typeof(T);
            Response = response;
            Message = message;
        }

        #endregion
    }
}
