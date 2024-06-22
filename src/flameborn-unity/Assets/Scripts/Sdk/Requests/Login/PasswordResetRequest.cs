using System;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.Abstract;
using flameborn.Sdk.Requests.Login.Abstract;
using HF.Extensions;

namespace flameborn.Sdk.Requests.Login
{
    /// <summary>
    /// Represents a request to reset a password.
    /// </summary>
    public class PasswordResetRequest : Request<IPasswordResetResponse>, IApiRequest<IPasswordResetResponse>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordResetRequest"/> class.
        /// </summary>
        /// <param name="controller">The API controller used to send the request.</param>
        public PasswordResetRequest(IApiController<IPasswordResetResponse> controller) : base(controller)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sends the request to reset a password.
        /// </summary>
        /// <param name="errorLog">The error log to be populated in case of an error.</param>
        /// <param name="listeners">The listeners to process the response.</param>
        public override void SendRequest(out string errorLog, params Action<IPasswordResetResponse>[] listeners)
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
