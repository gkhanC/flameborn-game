using System;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.Abstract;

namespace flameborn.Sdk.Controllers
{
    /// <summary>
    /// Abstract base class for API controllers.
    /// </summary>
    /// <typeparam name="T">The type of response managed by the controller.</typeparam>
    public abstract class Controller<T> : IApiController<T> where T : IApiResponse
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Controller{T}"/> class.
        /// </summary>
        protected Controller()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sends the API request.
        /// </summary>
        /// <param name="errorLog">The error log to be populated in case of an error.</param>
        /// <param name="listeners">The listeners to process the response.</param>
        public abstract void SendRequest(out string errorLog, params Action<T>[] listeners);

        #endregion
    }
}
