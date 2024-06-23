using System;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.Login.Abstract;
using flameborn.Sdk.Requests.Login.Entity;
using HF.Extensions;
using PlayFab;
using PlayFab.ClientModels;

namespace flameborn.Sdk.Controllers.Login
{
    /// <summary>
    /// Controller for logging in with a device ID on PlayFab.
    /// </summary>
    public class DeviceLoginController_Playfab : Controller<ILoginResponse>, IApiController<ILoginResponse>
    {
        #region Fields

        private string deviceId;
        private string titleId;
        private event Action<ILoginResponse> onGetResult;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceLoginController_Playfab"/> class.
        /// </summary>
        /// <param name="deviceId">The device ID for the login request.</param>
        /// <param name="titleId">The title ID for the login request.</param>
        public DeviceLoginController_Playfab(string deviceId, string titleId)
        {
            this.deviceId = deviceId;
            this.titleId = titleId;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sends the request to log in with a device ID.
        /// </summary>
        /// <param name="errorLog">The error log to be populated in case of an error.</param>
        /// <param name="listeners">The listeners to process the response.</param>
        public override void SendRequest(out string errorLog, params Action<ILoginResponse>[] listeners)
        {
            errorLog = "";
            if (string.IsNullOrEmpty(deviceId)) 
            { 
                errorLog = $"{nameof(deviceId)} is null or empty."; 
            }
            if (string.IsNullOrEmpty(titleId)) 
            { 
                errorLog = $"{nameof(titleId)} is null or empty."; 
            }

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

        /// <summary>
        /// Takes the request for logging in with an Android device ID.
        /// </summary>
        /// <returns>The request to log in with an Android device ID.</returns>
        private LoginWithAndroidDeviceIDRequest TakeRequestAndroid()
        {
            return new LoginWithAndroidDeviceIDRequest
            {
                AndroidDeviceId = deviceId,
                CreateAccount = true
            };
        }

        /// <summary>
        /// Takes the request for logging in with an iOS device ID.
        /// </summary>
        /// <returns>The request to log in with an iOS device ID.</returns>
        private LoginWithIOSDeviceIDRequest TakeRequestIOS()
        {
            return new LoginWithIOSDeviceIDRequest
            {
                DeviceId = deviceId,
                CreateAccount = true
            };
        }

        /// <summary>
        /// Handles the event when the login result is received.
        /// </summary>
        /// <param name="result">The result of the login request.</param>
        private void OnGetLoginResult_EventListener(LoginResult result)
        {
            var response = new LoginResponse();
            response.SetResponse(true, false, result.NewlyCreated, result.PlayFabId, result, "Login succeed.");
            onGetResult?.Invoke(response);
        }

        /// <summary>
        /// Handles errors that occur during the login request.
        /// </summary>
        /// <param name="error">The error that occurred.</param>
        private void OnError(PlayFabError error)
        {
            var response = new LoginResponse();
            response.IsRequestSuccess = false;
            response.Message = error.ErrorMessage;
            onGetResult?.Invoke(response);
        }

        #endregion
    }
}
