using System;
using Flameborn.PlayFab.Abstract;
using MADD;
using PlayFab;
using PlayFab.ClientModels;

namespace Flameborn.PlayFab
{
    /// <summary>
    /// Handles PlayFab login operations.
    /// </summary>
    [Docs("Handles PlayFab login operations.")]
    public class PlayFabLogin : ILogin
    {
        /// <summary>
        /// Login data for PlayFab.
        /// </summary>
        [Docs("Login data for PlayFab.")]
        private readonly PlayFabLoginData _loginData;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayFabLogin"/> class with the specified login data.
        /// </summary>
        /// <param name="loginData">The login data for PlayFab.</param>
        [Docs("Initializes a new instance of the PlayFabLogin class with the specified login data.")]
        public PlayFabLogin(PlayFabLoginData loginData)
        {
            _loginData = loginData;
        }

        /// <summary>
        /// Logs the user in using PlayFab.
        /// </summary>
        /// <param name="logMessage">Outputs a log message indicating the result of the login attempt.</param>
        /// <returns>True if the login request was sent, otherwise false.</returns>
        [Docs("Logs the user in using PlayFab and outputs a log message indicating the result of the login attempt.")]
        public bool Login(out string logMessage)
        {            
            logMessage = String.Empty;

#if UNITY_ANDROID
            var requestAndroid = new LoginWithAndroidDeviceIDRequest
            {
                AndroidDeviceId = _loginData.DeviceId,
                CreateAccount = _loginData.CanCreateAccount
            };
            PlayFabClientAPI.LoginWithAndroidDeviceID(requestAndroid, 
                resultCallback: _loginData.OnLoginSuccess, 
                errorCallback: _loginData.OnLoginFailure);
            logMessage = "Logging in with Android.";
            return true;
#endif

#if UNITY_IOS
            var requestIOS = new LoginWithIOSDeviceIDRequest
            {
                DeviceId = _loginData.DeviceId,
                CreateAccount = _loginData.CanCreateAccount
            };
            PlayFabClientAPI.LoginWithIOSDeviceID(requestIOS, 
                resultCallback: _loginData.OnLoginSuccess, 
                errorCallback: _loginData.OnLoginFailure);
            logMessage = "Logging in with iOS.";
            return true;
#endif

            logMessage = "Login platform not supported.";
            return false;
        }
    }
}
