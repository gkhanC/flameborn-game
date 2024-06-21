using System;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.Abstract;
using flameborn.Sdk.Requests.Login.Abstract;
using HF.Extensions;

namespace flameborn.Sdk.Requests.Login
{
    public class RegisterRequest : Request<IRegisterResponse>, IApiRequest<IRegisterResponse>
    {
        public RegisterRequest(IApiController<IRegisterResponse> controller) : base(controller)
        {
        }

        public override void SendRequest(out string errorLog, params Action<IRegisterResponse>[] listeners)
        {
            errorLog = "";
            if (Controller.IsNull()) { errorLog = $"{this.GetType().Name} does not have controller."; return; }
            Controller.SendRequest(out errorLog, listeners);
        }
    }
}