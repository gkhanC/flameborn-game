using System;
using flameborn.Core.Managers;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Managers;
using flameborn.Sdk.Requests.Login.Abstract;
using flameborn.Sdk.Requests.Login.Entity;
using HF.Extensions;
using PlayFab;
using PlayFab.ClientModels;

namespace flameborn.Sdk.Controllers.Login
{
    public class RegisterController_Playfab : Controller<IRegisterResponse>, IApiController<IRegisterResponse>
    {
        string email;
        string password;
        string userName;
        private event Action<IRegisterResponse> onGetResult;

        public RegisterController_Playfab(string email, string password, string userName)
        {
            this.email = email;
            this.password = password;
            this.userName = userName;
        }

        public override void SendRequest(out string errorLog, params Action<IRegisterResponse>[] listeners)
        {
            errorLog = "";
            if (string.IsNullOrEmpty(email)) { errorLog = $"{nameof(email)} is null or empty."; }
            if (string.IsNullOrEmpty(password) || password.Length < 6) { errorLog = $"{nameof(password)} is null or empty."; }
            if (string.IsNullOrEmpty(userName)) { errorLog = $"{nameof(userName)} is null or empty."; }

            listeners.ForEach(l => onGetResult += l);
            var request = TakeRequest();
            PlayFabClientAPI.AddUsernamePassword(request, OnGetLoginResult_EventListener, OnError);
        }

        private void OnGetLoginResult_EventListener(AddUsernamePasswordResult result)
        {
            var response = new RegisterResponse();
            response.SetResponse(true, result, "You are registered.");
            onGetResult?.Invoke(response);

            var account = GameManager.Instance.GetManager<AccountManager>().Instance.Account;
            var PlayfabManager = GameManager.Instance.GetManager<PlayfabManager>().Instance;
#if UNITY_ANDROID
            PlayfabManager.UnlinkAndroidDeviceId(account.DeviceId, UnlinkDeviceId);
#endif
#if Unity_IOS
            PlayfabManager.UnlinkIOSDeviceId(account.DeviceId, UnlinkDeviceId);
#endif

        }

        private void UnlinkDeviceId(UnlinkAndroidDeviceIDResult result) { }
        private void UnlinkDeviceId(UnlinkIOSDeviceIDResult result) { }

        private void OnError(PlayFabError error)
        {
            var response = new RegisterResponse();
            response.IsRequestSuccess = false;
            response.Message = error.ErrorMessage;
            onGetResult?.Invoke(response);
        }

        private AddUsernamePasswordRequest TakeRequest()
        {
            return new AddUsernamePasswordRequest
            {
                Email = email,
                Password = password,
                Username = userName
            };
        }
    }
}