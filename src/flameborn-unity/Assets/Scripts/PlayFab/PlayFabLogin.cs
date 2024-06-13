using System;
using Flameborn.Configurations;
using Flameborn.Managers;
using Flameborn.PlayFab.Abstract;
using HF.Extensions;
using HF.Logger;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

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

        private bool _isLoggedTryEmail;
        private bool _isLoggedTryRegister;

        private void OnRegisterSuccess(RegisterPlayFabUserResult result)
        {
            LoginEmail(out _);
            HFLogger.LogSuccess(this, "User account registered.");
        }

        /// <summary>
        /// Called when login fails.
        /// </summary>
        /// <param name="playFabError">The PlayFab error that occurred.</param>
        private void OnLoginFailure(PlayFabError playFabError)
        {
            if (_isLoggedTryEmail && !_isLoggedTryRegister)
            {
                HFLogger.LogError(this, "Login with email error:" + playFabError.Error);
                var registerRequest = new RegisterPlayFabUserRequest
                {
                    Email = UserManager.Instance.currentUserData.Email,
                    Password = UserManager.Instance.currentUserData.Password,
                    Username = UserManager.Instance.currentUserData.UserName,
                    DisplayName = UserManager.Instance.currentUserData.UserName
                };
                _isLoggedTryRegister = true;
                PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, _loginData.OnLoginFailure);
                return;
            }

            _loginData.OnLoginFailure(playFabError);
        }

        private bool LoginEmail(out string logMessage)
        {
            var requestEmail = new LoginWithEmailAddressRequest
            {
                Email = UserManager.Instance.currentUserData.Email,
                Password = UserManager.Instance.currentUserData.Password
            };

            _isLoggedTryEmail = true;
            PlayFabClientAPI.LoginWithEmailAddress(requestEmail, _loginData.OnLoginSuccess, OnLoginFailure);
            logMessage = "Login in with Email.";

            return true;
        }

        private bool LoginMobile(out string logMessage)
        {

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
                        return LoginEmail(out logMessage);
                    }
                }
            }

            return LoginMobile(out logMessage);
        }
    }
}
