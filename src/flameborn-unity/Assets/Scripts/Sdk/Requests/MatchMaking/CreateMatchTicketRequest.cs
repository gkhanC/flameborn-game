using System;
using HF.Extensions;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.MatchMaking.Abstract;
using flameborn.Sdk.Requests.Abstract;

namespace flameborn.Sdk.Requests.MatchMaking
{
    /// <summary>
    /// Represents a request to create a match ticket.
    /// </summary>
    public class CreateMatchTicketRequest : Request<ICreateMatchTicketResponse>, IApiRequest<ICreateMatchTicketResponse>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMatchTicketRequest"/> class.
        /// </summary>
        /// <param name="controller">The API controller for the request.</param>
        public CreateMatchTicketRequest(IApiController<ICreateMatchTicketResponse> controller) : base(controller)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sends the request to create a match ticket.
        /// </summary>
        /// <param name="errorLog">The error log to be populated in case of an error.</param>
        /// <param name="listeners">The listeners to process the response.</param>
        public override void SendRequest(out string errorLog, params Action<ICreateMatchTicketResponse>[] listeners)
        {
            errorLog = "";
            if (Controller.IsNull())
            {
                errorLog = $"{this.GetType().Name} does not have controller.";
                return;
            }
            Controller.SendRequest(out errorLog, listeners);
        }

        #endregion
    }
}
