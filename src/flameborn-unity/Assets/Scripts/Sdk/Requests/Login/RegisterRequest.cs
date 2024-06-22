using System;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.Abstract;
using flameborn.Sdk.Requests.Login.Abstract;
using HF.Extensions;

namespace flameborn.Sdk.Requests.Login
{
    /// <summary>
    /// Represents a request to register a new account.
    /// </summary>
    public class RegisterRequest : Request<IRegisterResponse>, IApiRequest<IRegisterResponse>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterRequest"/> class.
        /// </summary>
        /// <param name="controller">The API controller used to send the request.</param>
        public RegisterRequest(IApiController<IRegisterResponse> controller) : base(controller)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sends the request to register a new account.
        /// </summary>
        /// <param name="errorLog">The error log to be populated in case of an error.</param>
        /// <param name="listeners">The listeners to process the response.</param>
        public override void SendRequest(out string errorLog, params Action<IRegisterResponse>[] listeners)
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
