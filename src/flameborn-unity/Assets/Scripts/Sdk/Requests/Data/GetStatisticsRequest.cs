using System;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.Abstract;
using flameborn.Sdk.Requests.Data.Abstract;
using HF.Extensions;

namespace flameborn.Sdk.Requests.Login
{
    public class GetStatisticsRequest : Request<IGetStatisticsResponse>, IApiRequest<IGetStatisticsResponse>
    {
        public GetStatisticsRequest(IApiController<IGetStatisticsResponse> controller) : base(controller)
        {
        }

        public override void SendRequest(out string errorLog, params Action<IGetStatisticsResponse>[] listeners)
        {
            errorLog = "";
            if (Controller.IsNull()) { errorLog = $"{this.GetType().Name} does not have controller."; return; }
            Controller.SendRequest(out errorLog, listeners);
        }
    }
}