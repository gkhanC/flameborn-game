using System;

namespace flameborn.Sdk.Requests.Abstract
{
    /// <summary>
    /// Defines an interface for API responses.
    /// </summary>
    public interface IApiResponse
    {
        #region Properties

        /// <summary>
        /// Gets a value indicating whether the request was successful.
        /// </summary>
        bool IsRequestSuccess { get; }

        /// <summary>
        /// Gets the response message.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Gets the response object.
        /// </summary>
        object Response { get; }

        /// <summary>
        /// Gets the type of the response.
        /// </summary>
        Type ResponseType { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the response details.
        /// </summary>
        /// <typeparam name="T">The type of the response.</typeparam>
        /// <param name="isSuccess">Indicates if the response is successful.</param>
        /// <param name="response">The response object.</param>
        /// <param name="message">The message associated with the response.</param>
        void SetResponse<T>(bool isSuccess, T response, string message = "");

        #endregion
    }
}
