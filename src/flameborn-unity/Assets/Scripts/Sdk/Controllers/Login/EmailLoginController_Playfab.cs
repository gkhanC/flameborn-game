using System;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.Login.Abstract;
using flameborn.Sdk.Requests.Login.Entity;
using HF.Extensions;
using PlayFab;
using PlayFab.ClientModels;

namespace flameborn.Sdk.Controllers.Login
{
    public class EmailLoginController_Playfab : Controller<ILoginResponse>, IApiController<ILoginResponse>
    {
        string email;
        string password;
        string titleId;
        bool isCombined;
        private event Action<ILoginResponse> onGetResult;

        public EmailLoginController_Playfab(string email, string password, string titleId, bool isCombined = true)
        {
            this.email = email;
            this.password = password;
            this.titleId = titleId;
            this.isCombined = isCombined;
        }

        public override void SendRequest(out string errorLog, params Action<ILoginResponse>[] listeners)
        {
            errorLog = "";
            if (string.IsNullOrEmpty(email)) { errorLog = $"{nameof(email)} is null or empty."; }
            if (string.IsNullOrEmpty(password) || password.Length < 6) { errorLog = $"{nameof(password)} is null or empty."; }
            if (string.IsNullOrEmpty(titleId)) { errorLog = $"{nameof(titleId)} is null or empty."; }

            listeners.ForEach(l => onGetResult += l);
            var request = TakeRequest();
            PlayFabClientAPI.LoginWithEmailAddress(request, OnGetLoginResult_EventListener, OnError);
        }

        private void OnGetLoginResult_EventListener(LoginResult result)
        {
            var response = new LoginResponse();
            response.SetResponse(true, true, result.NewlyCreated, result.PlayFabId, result, "Login succeed.");
            onGetResult?.Invoke(response);
        }

        private void OnError(PlayFabError error)
        {
            var response = new LoginResponse();
            response.IsRequestSuccess = false;
            response.Message = error.ErrorMessage;
            onGetResult?.Invoke(response);
        }

        private LoginWithEmailAddressRequest TakeRequest()
        {
            return new LoginWithEmailAddressRequest
            {
                Email = email,
                Password = password,
                TitleId = titleId,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
                {
                    GetUserAccountInfo = isCombined
                }
            };
        }
    }
}