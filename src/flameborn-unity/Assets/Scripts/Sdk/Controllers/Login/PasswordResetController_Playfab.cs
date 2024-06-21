using System;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.Login.Abstract;
using flameborn.Sdk.Requests.Login.Entity;
using HF.Extensions;
using PlayFab;
using PlayFab.ClientModels;

namespace flameborn.Sdk.Controllers.Login
{
    public class PasswordResetController_Playfab : Controller<IPasswordResetResponse>, IApiController<IPasswordResetResponse>
    {
        string email;
        private event Action<IPasswordResetResponse> onGetResult;
        public PasswordResetController_Playfab(string email)
        {
            this.email = email;
        }

        public override void SendRequest(out string errorLog, params Action<IPasswordResetResponse>[] listeners)
        {
            errorLog = "";
            if (string.IsNullOrEmpty(email)) { errorLog = $"{nameof(email)} is null or empty."; }


            listeners.ForEach(l => onGetResult += l);
            var request = TakeRequest();
            PlayFabClientAPI.SendAccountRecoveryEmail(request, OnGetLoginResult_EventListener, OnError);
        }

        private void OnGetLoginResult_EventListener(SendAccountRecoveryEmailResult result)
        {
            var response = new PasswordResetResponse();
            response.SetResponse(true, result, "Password reset e-mail sended.");
            onGetResult?.Invoke(response);
        }

        private void OnError(PlayFabError error)
        {
            var response = new PasswordResetResponse();
            response.IsRequestSuccess = false;
            response.Message = error.ErrorMessage;
            onGetResult?.Invoke(response);
        }

        private SendAccountRecoveryEmailRequest TakeRequest()
        {
            return new SendAccountRecoveryEmailRequest
            {
                Email = email,
                TitleId = PlayFabSettings.TitleId
            };
        }
    }
}