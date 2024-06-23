using System;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.Abstract;
using flameborn.Sdk.Requests.Data.Abstract;
using HF.Extensions;

namespace flameborn.Sdk.Requests.Data
{
    /// <summary>
    /// Represents a request to update statistics.
    /// </summary>
    public class UpdateStatisticsRequest : Request<IUpdateStatisticsResponse>, IApiRequest<IUpdateStatisticsResponse>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateStatisticsRequest"/> class.
        /// </summary>
        /// <param name="controller">The API controller used to send the request.</param>
        public UpdateStatisticsRequest(IApiController<IUpdateStatisticsResponse> controller) : base(controller)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sends the request to update statistics.
        /// </summary>
        /// <param name="errorLog">The error log to be populated in case of an error.</param>
        /// <param name="listeners">The listeners to process the response.</param>
        public override void SendRequest(out string errorLog, params Action<IUpdateStatisticsResponse>[] listeners)
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
