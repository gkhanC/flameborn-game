using System;

namespace flameborn.Sdk.Requests.Abstract
{
    /// <summary>
    /// Defines an interface for API requests.
    /// </summary>
    /// <typeparam name="T">The type of response managed by the request.</typeparam>
    public interface IApiRequest<T> where T : IApiResponse
    {
        #region Methods

        /// <summary>
        /// Sends a request and processes it with the specified listeners.
        /// </summary>
        /// <param name="errorLog">The error log to be populated in case of an error.</param>
        /// <param name="listeners">The listeners to process the response.</param>
        void SendRequest(out string errorLog, params Action<T>[] listeners);

        #endregion
    }
}
