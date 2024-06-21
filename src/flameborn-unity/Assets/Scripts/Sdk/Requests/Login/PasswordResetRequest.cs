using System;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.Abstract;
using flameborn.Sdk.Requests.Login.Abstract;
using HF.Extensions;

namespace flameborn.Sdk.Requests.Login
{
    public class PasswordResetRequest : Request<IPasswordResetResponse>, IApiRequest<IPasswordResetResponse>
    {
        public PasswordResetRequest(IApiController<IPasswordResetResponse> controller) : base(controller)
        {
        }

        public override void SendRequest(out string errorLog, params Action<IPasswordResetResponse>[] listeners)
        {
            errorLog = "";
            if (Controller.IsNull()) { errorLog = $"{this.GetType().Name} does not have controller."; return; }
            Controller.SendRequest(out errorLog, listeners);
        }
    }
}