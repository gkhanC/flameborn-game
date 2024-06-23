
# PasswordResetController_Playfab Class Documentation

## Overview
The `PasswordResetController_Playfab` class manages the resetting of passwords via PlayFab within the Flameborn SDK. This class is derived from the `Controller<IPasswordResetResponse>` class and implements the `IApiController<IPasswordResetResponse>` interface.

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
    /// Controller for resetting password via PlayFab.
    /// </summary>
    public class PasswordResetController_Playfab : Controller<IPasswordResetResponse>, IApiController<IPasswordResetResponse>
    {
        #region Fields

        private string email;
        private event Action<IPasswordResetResponse> onGetResult;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordResetController_Playfab"/> class.
        /// </summary>
        /// <param name="email">The email address associated with the account.</param>
        public PasswordResetController_Playfab(string email)
        {
            this.email = email;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sends the request to reset the password.
        /// </summary>
        /// <param name="errorLog">The error log to be populated in case of an error.</param>
        /// <param name="listeners">The listeners to process the response.</param>
        public override void SendRequest(out string errorLog, params Action<IPasswordResetResponse>[] listeners)
        {
            errorLog = "";
            if (string.IsNullOrEmpty(email)) 
            { 
                errorLog = $"{nameof(email)} is null or empty."; 
            }

            listeners.ForEach(l => onGetResult += l);
            var request = TakeRequest();
            PlayFabClientAPI.SendAccountRecoveryEmail(request, OnGetLoginResult_EventListener, OnError);
        }

        /// <summary>
        /// Handles the event when the password reset result is received.
        /// </summary>
        /// <param name="result">The result of the password reset request.</param>
        private void OnGetLoginResult_EventListener(SendAccountRecoveryEmailResult result)
        {
            var response = new PasswordResetResponse();
            response.SetResponse(true, result, "Password reset e-mail sent.");
            onGetResult?.Invoke(response);
        }

        /// <summary>
        /// Handles errors that occur during the password reset request.
        /// </summary>
        /// <param name="error">The error that occurred.</param>
        private void OnError(PlayFabError error)
        {
            var response = new PasswordResetResponse();
            response.IsRequestSuccess = false;
            response.Message = error.ErrorMessage;
            onGetResult?.Invoke(response);
        }

        /// <summary>
        /// Takes the request for sending the account recovery email.
        /// </summary>
        /// <returns>The request to send the account recovery email.</returns>
        private SendAccountRecoveryEmailRequest TakeRequest()
        {
            return new SendAccountRecoveryEmailRequest
            {
                Email = email,
                TitleId = PlayFabSettings.TitleId
            };
        }

        #endregion
    }
}
```

## Fields
- **email**: The email address associated with the account.

## Methods
### Public Methods
- **SendRequest(out string errorLog, params Action<IPasswordResetResponse>[] listeners)**: Sends the request to reset the password.
  - **Parameters**:
    - **errorLog**: The error log to be populated in case of an error.
    - **listeners**: The listeners to process the response.

### Private Methods
- **TakeRequest()**: Constructs the request for sending the account recovery email.
- **OnGetLoginResult_EventListener(SendAccountRecoveryEmailResult result)**: Handles the event when the password reset result is received.
- **OnError(PlayFabError error)**: Handles errors that occur during the request.

## Usage Example
Below is an example of how to use the `PasswordResetController_Playfab` class in a Unity project.

```csharp
using UnityEngine;
using flameborn.Sdk.Controllers.Login;

public class ExampleUsage : MonoBehaviour
{
    private void Start()
    {
        var controller = new PasswordResetController_Playfab("example@example.com");
        controller.SendRequest(out string errorLog, response => 
        {
            if (response.IsRequestSuccess)
            {
                Debug.Log("Password reset email sent successfully.");
            }
            else
            {
                Debug.LogError("Failed to send password reset email: " + response.Message);
            }
        });
    }
}
```

## See Also
For more information on the `IPasswordResetResponse` interface, refer to the [IPasswordResetResponse documentation](https://github.com/gkhanC/flameborn-game/tree/dev/documents/IPasswordResetResponse).

## File Location
This class is defined in the `PasswordResetController_Playfab.cs` file, located in the `flameborn.Sdk.Controllers.Login` namespace.
