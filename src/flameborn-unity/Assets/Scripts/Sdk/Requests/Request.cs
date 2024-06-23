using System;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.Abstract;

namespace flameborn.Sdk.Requests
{
    /// <summary>
    /// Represents a base class for requests.
    /// </summary>
    /// <typeparam name="T">The type of the response.</typeparam>
    public abstract class Request<T> : IApiRequest<T> where T : IApiResponse
    {
        #region Properties

        /// <summary>
        /// Gets or sets the API controller for the request.
        /// </summary>
        protected IApiController<T> Controller { get; set; } = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Request{T}"/> class.
        /// </summary>
        /// <param name="controller">The API controller for the request.</param>
        protected Request(IApiController<T> controller)
        {
            Controller = controller;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sends the request.
        /// </summary>
        /// <param name="errorLog">The error log to be populated in case of an error.</param>
        /// <param name="listeners">The listeners to process the response.</param>
        public abstract void SendRequest(out string errorLog, params Action<T>[] listeners);

        #endregion
    }
}
