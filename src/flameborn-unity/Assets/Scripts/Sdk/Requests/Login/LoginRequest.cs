using System;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.Abstract;
using flameborn.Sdk.Requests.Login.Abstract;
using HF.Extensions;

namespace flameborn.Sdk.Requests.Login
{
    /// <summary>
    /// Represents a request to log in.
    /// </summary>
    public class LoginRequest : Request<ILoginResponse>, IApiRequest<ILoginResponse>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginRequest"/> class.
        /// </summary>
        /// <param name="controller">The API controller used to send the request.</param>
        public LoginRequest(IApiController<ILoginResponse> controller) : base(controller)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sends the request to log in.
        /// </summary>
        /// <param name="errorLog">The error log to be populated in case of an error.</param>
        /// <param name="listeners">The listeners to process the response.</param>
        public override void SendRequest(out string errorLog, params Action<ILoginResponse>[] listeners)
        {
            errorLog = "";
            if (Controller.IsNull()) 
            { 
                errorLog = $"{this.GetType().Name} does not have a controller."; 
                return; 
            }
            Controller.SendRequest(out errorLog, listeners);
        }

        #endregion
    }
}
