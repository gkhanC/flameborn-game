using System;
using Flameborn.Configurations;
using Flameborn.Managers;
using Flameborn.PlayFab.Abstract;
using HF.Extensions;
using PlayFab;
using PlayFab.ClientModels;

namespace Flameborn.PlayFab
{
    /// <summary>
    /// Handles PlayFab login operations.
    /// </summary>
    public class PlayFabLogin : ILogin
    {
        /// <summary>
        /// Login data for PlayFab.
        /// </summary>
        private readonly PlayFabLoginData _loginData;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayFabLogin"/> class with the specified login data.
        /// </summary>
        /// <param name="loginData">The login data for PlayFab.</param>
        public PlayFabLogin(PlayFabLoginData loginData)
        {
            _loginData = loginData;
        }

        /// <summary>
        /// Logs the user in using PlayFab.
        /// </summary>
        /// <param name="logMessage">Outputs a log message indicating the result of the login attempt.</param>
        /// <returns>True if the login request was sent, otherwise false.</returns>
        public bool Login(out string logMessage, PlayFabConfiguration config)
        {
            logMessage = String.Empty;

            if (UserManager.Instance.IsNotNull() && UserManager.Instance.gameObject.IsNotNull())
            {
                if (UserManager.Instance.currentUserData.IsRegistered)
                {
                    if (UserManager.Instance.currentUserData.IsPasswordCorrect)
                    {
                        var requestEmail = new LoginWithEmailAddressRequest
                        {
                            Email = UserManager.Instance.currentUserData.EMail,
                            Password = UserManager.Instance.currentUserData.Password,
                            TitleId = config.TitleId,                            
                        };

                        PlayFabClientAPI.LoginWithEmailAddress(requestEmail, _loginData.OnLoginSuccess, _loginData.OnLoginFailure);
                        logMessage = "Login in with Email.";
                        return true;
                    }
                }
            }

#if UNITY_ANDROID

            var requestAndroid = new LoginWithAndroidDeviceIDRequest
            {
                AndroidDeviceId = _loginData.DeviceId,
                CreateAccount = _loginData.CanCreateAccount
            };
            PlayFabClientAPI.LoginWithAndroidDeviceID(requestAndroid,
                resultCallback: _loginData.OnLoginSuccess,
                errorCallback: _loginData.OnLoginFailure);
            logMessage = "Login in with Android.";
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
            logMessage = "Login in with iOS.";
            return true;
#endif

        }
    }
}
