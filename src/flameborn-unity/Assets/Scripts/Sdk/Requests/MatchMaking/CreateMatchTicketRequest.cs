using HF.Extensions;
using System;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.MatchMaking.Abstract;
using flameborn.Sdk.Requests.Abstract;

namespace flameborn.Sdk.Requests.MatchMaking
{
    public class CreateMatchTicketRequest : Request<ICreateMatchTicketResponse>, IApiRequest<ICreateMatchTicketResponse>
    {
        public CreateMatchTicketRequest(IApiController<ICreateMatchTicketResponse> controller) : base(controller)
        {
        }

        public override void SendRequest(out string errorLog, params Action<ICreateMatchTicketResponse>[] listeners)
        {
            errorLog = "";
            if (Controller.IsNull()) { errorLog = $"{this.GetType().Name} does not have controller."; return; }
            Controller.SendRequest(out errorLog, listeners);
        }
    }
}