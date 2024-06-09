using System;
using Flameborn.PlayFab.Abstract;
using PlayFab;
using PlayFab.ClientModels;

namespace Flameborn.PlayFab
{
    public class PlayFabLogin : ILogin
    {
        private readonly PlayFabLoginData _loginData;

        public PlayFabLogin(PlayFabLoginData loginData)
        {
            _loginData = loginData;
        }

        public bool Login(out string logMessage)
        {            
            logMessage = String.Empty;

#if UNITY_ANDROID

            var requestAndroid = new LoginWithAndroidDeviceIDRequest{ AndroidDeviceId = _loginData.DeviceId , CreateAccount = _loginData.CanCreateAccount };
            PlayFabClientAPI.LoginWithAndroidDeviceID(requestAndroid, resultCallback: _loginData.OnLoginSuccess, errorCallback: _loginData.OnLoginFailure);
            logMessage = "Logging.";
            return true;

#endif
#if UNITY_IOS

            var requestIOS = new LoginWithIOSDeviceIDRequest{ DeviceId  = _loginData.DeviceId , CreateAccount = _loginData.CanCreateAccount };
            PlayFabClientAPI.LoginWithIOSDeviceIDRequest(requestIOS, resultCallback: _loginData.OnLoginSuccess, errorCallback: _loginData.OnLoginFailure);
            logMessage = "Logging.";
            return true;
#endif

            return false;
        }
    }
}