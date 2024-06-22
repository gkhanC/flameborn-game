using System;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.Abstract;
using flameborn.Sdk.Requests.MatchMaking.Abstract;
using HF.Extensions;

namespace flameborn.Sdk.Requests.MatchMaking
{
    /// <summary>
    /// Represents a request to find a match.
    /// </summary>
    public class FindMatchRequest : Request<IFindMatchResponse>, IApiRequest<IFindMatchResponse>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FindMatchRequest"/> class.
        /// </summary>
        /// <param name="controller">The API controller for the request.</param>
        public FindMatchRequest(IApiController<IFindMatchResponse> controller) : base(controller)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sends the request to find a match.
        /// </summary>
        /// <param name="errorLog">The error log to be populated in case of an error.</param>
        /// <param name="listeners">The listeners to process the response.</param>
        public override void SendRequest(out string errorLog, params Action<IFindMatchResponse>[] listeners)
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
