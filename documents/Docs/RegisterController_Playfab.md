
# RegisterController_Playfab Class Documentation

## Overview
The `RegisterController_Playfab` class manages the registration of new accounts via PlayFab within the Flameborn SDK. This class is derived from the `Controller<IRegisterResponse>` class and implements the `IApiController<IRegisterResponse>` interface.

## Class Definition

```csharp
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
    /// <summary>
    /// Controller for registering a new account via PlayFab.
    /// </summary>
    public class RegisterController_Playfab : Controller<IRegisterResponse>, IApiController<IRegisterResponse>
    {
        #region Fields

        private string email;
        private string password;
        private string userName;
        private event Action<IRegisterResponse> onGetResult;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterController_Playfab"/> class.
        /// </summary>
        /// <param name="email">The email address for the new account.</param>
        /// <param name="password">The password for the new account.</param>
        /// <param name="userName">The user name for the new account.</param>
        public RegisterController_Playfab(string email, string password, string userName)
        {
            this.email = email;
            this.password = password;
            this.userName = userName;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sends the request to register a new account.
        /// </summary>
        /// <param name="errorLog">The error log to be populated in case of an error.</param>
        /// <param name="listeners">The listeners to process the response.</param>
        public override void SendRequest(out string errorLog, params Action<IRegisterResponse>[] listeners)
        {
            errorLog = "";
            if (string.IsNullOrEmpty(email)) 
            { 
                errorLog = $"{nameof(email)} is null or empty."; 
            }
            if (string.IsNullOrEmpty(password) || password.Length < 6) 
            { 
                errorLog = $"{nameof(password)} is null or empty."; 
            }
            if (string.IsNullOrEmpty(userName)) 
            { 
                errorLog = $"{nameof(userName)} is null or empty."; 
            }

            listeners.ForEach(l => onGetResult += l);
            var request = TakeRequest();
            PlayFabClientAPI.AddUsernamePassword(request, OnGetLoginResult_EventListener, OnError);
        }

        /// <summary>
        /// Takes the request for adding a username and password.
        /// </summary>
        /// <returns>The request to add a username and password.</returns>
        private AddUsernamePasswordRequest TakeRequest()
        {
            return new AddUsernamePasswordRequest
            {
                Email = email,
                Password = password,
                Username = userName
            };
        }

        /// <summary>
        /// Handles the event when the registration result is received.
        /// </summary>
        /// <param name="result">The result of the registration request.</param>
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
#if UNITY_IOS
            PlayfabManager.UnlinkIOSDeviceId(account.DeviceId, UnlinkDeviceId);
#endif
        }

        /// <summary>
        /// Handles the event when the Android device ID is unlinked.
        /// </summary>
        /// <param name="result">The result of the unlinking request.</param>
        private void UnlinkDeviceId(UnlinkAndroidDeviceIDResult result) { }

        /// <summary>
        /// Handles the event when the iOS device ID is unlinked.
        /// </summary>
        /// <param name="result">The result of the unlinking request.</param>
        private void UnlinkDeviceId(UnlinkIOSDeviceIDResult result) { }

        /// <summary>
        /// Handles errors that occur during the registration request.
        /// </summary>
        /// <param name="error">The error that occurred.</param>
        private void OnError(PlayFabError error)
        {
            var response = new RegisterResponse();
            response.IsRequestSuccess = false;
            response.Message = error.ErrorMessage;
            onGetResult?.Invoke(response);
        }

        #endregion
    }
}
```

## Fields
- **email**: The email address for the new account.
- **password**: The password for the new account.
- **userName**: The user name for the new account.

## Methods
### Public Methods
- **SendRequest(out string errorLog, params Action<IRegisterResponse>[] listeners)**: Sends the request to register a new account via PlayFab.
  - **Parameters**:
    - **errorLog**: The error log to be populated in case of an error.
    - **listeners**: The listeners to process the response.

### Private Methods
- **TakeRequest()**: Constructs the request for adding a username and password.
- **OnGetLoginResult_EventListener(AddUsernamePasswordResult result)**: Handles the event when the registration result is received.
- **UnlinkDeviceId(UnlinkAndroidDeviceIDResult result)**: Handles the event when the Android device ID is unlinked.
- **UnlinkDeviceId(UnlinkIOSDeviceIDResult result)**: Handles the event when the iOS device ID is unlinked.
- **OnError(PlayFabError error)**: Handles errors that occur during the request.

## Usage Example
Below is an example of how to use the `RegisterController_Playfab` class in a Unity project.

```csharp
using UnityEngine;
using flameborn.Sdk.Controllers.Login;

public class ExampleUsage : MonoBehaviour
{
    private void Start()
    {
        var controller = new RegisterController_Playfab("example@example.com", "password123", "Player1");
        controller.SendRequest(out string errorLog, response => 
        {
            if (response.IsRequestSuccess)
            {
                Debug.Log("Registration succeeded.");
            }
            else
            {
                Debug.LogError("Failed to register: " + response.Message);
            }
        });
    }
}
```

## See Also
For more information on the `IRegisterResponse` interface, refer to the [IRegisterResponse documentation](https://gkhanc.github.io/flameborn-game/IRegisterResponse).

## File Location
This class is defined in the `RegisterController_Playfab.cs` file, located in the `flameborn.Sdk.Controllers.Login` namespace.
