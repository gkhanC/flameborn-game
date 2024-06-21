using System;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.Abstract;
using flameborn.Sdk.Requests.MatchMaking.Abstract;
using HF.Extensions;

namespace flameborn.Sdk.Requests.MatchMaking
{
    public class GetMatchInfoRequest : Request<IGetMatchInfoResponse>, IApiRequest<IGetMatchInfoResponse>
    {
        public GetMatchInfoRequest(IApiController<IGetMatchInfoResponse> controller) : base(controller)
        {
        }

        public override void SendRequest(out string errorLog, params Action<IGetMatchInfoResponse>[] listeners)
        {
            errorLog = "";
            if (Controller.IsNull()) { errorLog = $"{this.GetType().Name} does not have controller."; return; }
            Controller.SendRequest(out errorLog, listeners);
        }
    }
}