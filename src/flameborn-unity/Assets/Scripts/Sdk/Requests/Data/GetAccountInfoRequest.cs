using System;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.Abstract;
using flameborn.Sdk.Requests.Data.Abstract;
using HF.Extensions;

namespace flameborn.Sdk.Requests.Data
{
    public class GetAccountInfoRequest : Request<IAccountInfoResponse>, IApiRequest<IAccountInfoResponse>
    {
        public GetAccountInfoRequest(IApiController<IAccountInfoResponse> controller) : base(controller)
        {
        }

        public override void SendRequest(out string errorLog, params Action<IAccountInfoResponse>[] listeners)
        {
            errorLog = "";
            if (Controller.IsNull()) { errorLog = $"{this.GetType().Name} does not have controller."; return; }
            Controller.SendRequest(out errorLog, listeners);
        }
    }
}