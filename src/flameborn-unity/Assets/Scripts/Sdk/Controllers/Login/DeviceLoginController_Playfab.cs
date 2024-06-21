using System;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.Login.Abstract;
using flameborn.Sdk.Requests.Login.Entity;
using HF.Extensions;
using PlayFab;
using PlayFab.ClientModels;

namespace flameborn.Sdk.Controllers.Login
{
    public class DeviceLoginController_Playfab : Controller<ILoginResponse>, IApiController<ILoginResponse>
    {
        string deviceId;
        string titleId;
        private event Action<ILoginResponse> onGetResult;

        public DeviceLoginController_Playfab(string deviceId, string titleId)
        {
            this.deviceId = deviceId;
            this.titleId = titleId;
        }

        public override void SendRequest(out string errorLog, params Action<ILoginResponse>[] listeners)
        {
            errorLog = "";
            if (string.IsNullOrEmpty(deviceId)) { errorLog = $"{nameof(deviceId)} is null or empty."; }
            if (string.IsNullOrEmpty(titleId)) { errorLog = $"{nameof(titleId)} is null or empty."; }

            listeners.ForEach(l => onGetResult += l);

#if UNITY_ANDROID
            var androidReq = TakeRequestAndroid();
            PlayFabClientAPI.LoginWithAndroidDeviceID(androidReq, OnGetLoginResult_EventListener, OnError);
#endif
#if UNITY_IOS
            var iosReq = TakeRequestIOS();
            PlayFabClientAPI.LoginWithIOSDeviceID(iosReq, OnGetLoginResult_EventListener, OnError);
#endif
        }

        private void OnGetLoginResult_EventListener(LoginResult result)
        {
            var response = new LoginResponse();
            response.SetResponse(true, false, result.NewlyCreated, result.PlayFabId, result, "Login succeed.");
            onGetResult?.Invoke(response);
        }

        private void OnError(PlayFabError error)
        {
            var response = new LoginResponse();
            response.IsRequestSuccess = false;
            response.Message = error.ErrorMessage;
            onGetResult?.Invoke(response);
        }

        private LoginWithAndroidDeviceIDRequest TakeRequestAndroid()
        {
            return new LoginWithAndroidDeviceIDRequest
            {
                AndroidDeviceId = deviceId,
                CreateAccount = true
            };
        }
        private LoginWithIOSDeviceIDRequest TakeRequestIOS()
        {
            return new LoginWithIOSDeviceIDRequest
            {
                DeviceId = deviceId,
                CreateAccount = true
            };
        }
    }
}