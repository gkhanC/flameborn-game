using System;
using flameborn.Sdk.Requests.Abstract;

namespace flameborn.Sdk.Controllers.Abstract
{
    /// <summary>
    /// Defines an interface for API controllers that manage sending requests and handling responses.
    /// </summary>
    /// <typeparam name="T">The type of response managed by the controller.</typeparam>
    public interface IApiController<T> where T : IApiResponse
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
