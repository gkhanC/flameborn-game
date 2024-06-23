
# DeviceLoginController_Playfab Class Documentation

## Overview
The `DeviceLoginController_Playfab` class manages logging in with a device ID on PlayFab within the Flameborn SDK. This class is derived from the `Controller<ILoginResponse>` class and implements the `IApiController<ILoginResponse>` interface.

## Class Definition

```csharp
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
```

## Fields
- **deviceId**: The device ID for the login request.
- **titleId**: The title ID for the login request.

## Methods
### Public Methods
- **SendRequest(out string errorLog, params Action<ILoginResponse>[] listeners)**: Sends the request to log in with a device ID on PlayFab.
  - **Parameters**:
    - **errorLog**: The error log to be populated in case of an error.
    - **listeners**: The listeners to process the response.

### Private Methods
- **TakeRequestAndroid()**: Constructs the request for logging in with an Android device ID.
- **TakeRequestIOS()**: Constructs the request for logging in with an iOS device ID.
- **OnGetLoginResult_EventListener(LoginResult result)**: Handles the event when the login result is received.
- **OnError(PlayFabError error)**: Handles errors that occur during the request.

## Usage Example
Below is an example of how to use the `DeviceLoginController_Playfab` class in a Unity project.

```csharp
using UnityEngine;
using flameborn.Sdk.Controllers.Login;

public class ExampleUsage : MonoBehaviour
{
    private void Start()
    {
        var controller = new DeviceLoginController_Playfab("device-id", "title-id");
        controller.SendRequest(out string errorLog, response => 
        {
            if (response.IsRequestSuccess)
            {
                Debug.Log("Login succeeded.");
            }
            else
            {
                Debug.LogError("Failed to log in: " + response.Message);
            }
        });
    }
}
```

## See Also
For more information on the `ILoginResponse` interface, refer to the [ILoginResponse documentation](https://github.com/gkhanC/flameborn-game/tree/dev/documents/ILoginResponse).

## File Location
This class is defined in the `DeviceLoginController_Playfab.cs` file, located in the `flameborn.Sdk.Controllers.Login` namespace.
