using System;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.Abstract;
using flameborn.Sdk.Requests.Data.Abstract;
using HF.Extensions;

namespace flameborn.Sdk.Requests.Data
{
    public class UpdateStatisticsRequest : Request<IUpdateStatisticsResponse>, IApiRequest<IUpdateStatisticsResponse>
    {
        public UpdateStatisticsRequest(IApiController<IUpdateStatisticsResponse> controller) : base(controller)
        {
        }

        public override void SendRequest(out string errorLog, params Action<IUpdateStatisticsResponse>[] listeners)
        {
            errorLog = "";
            if (Controller.IsNull()) { errorLog = $"{this.GetType().Name} does not have controller."; return; }
            Controller.SendRequest(out errorLog, listeners);
        }        
    }
}