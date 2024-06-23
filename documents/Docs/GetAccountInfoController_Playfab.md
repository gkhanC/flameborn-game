
# GetAccountInfoController_Playfab Class Documentation

## Overview
The `GetAccountInfoController_Playfab` class manages the retrieval of account information from PlayFab within the Flameborn SDK. This class is derived from the `Controller<IAccountInfoResponse>` class and implements the `IApiController<IAccountInfoResponse>` interface.

## Class Definition

```csharp
using System;
using flameborn.Core.Accounts;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.Data.Abstract;
using flameborn.Sdk.Requests.Data.Entity;
using HF.Extensions;
using Newtonsoft.Json;
using PlayFab;
using PlayFab.CloudScriptModels;

namespace flameborn.Sdk.Controllers.Data
{
    /// <summary>
    /// Controller for getting account information from PlayFab.
    /// </summary>
    public class GetAccountInfoController_Playfab : Controller<IAccountInfoResponse>, IApiController<IAccountInfoResponse>
    {
        #region Fields

        private string email;
        private string password;
        private string functionName;
        private event Action<IAccountInfoResponse> onGetResult;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAccountInfoController_Playfab"/> class.
        /// </summary>
        /// <param name="email">The email associated with the account.</param>
        /// <param name="password">The password associated with the account.</param>
        /// <param name="functionName">The name of the function to execute.</param>
        public GetAccountInfoController_Playfab(string email, string password, string functionName = "GetAccountInfo")
        {
            this.email = email;
            this.password = password;
            this.functionName = functionName;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sends the request to get account information.
        /// </summary>
        /// <param name="errorLog">The error log to be populated in case of an error.</param>
        /// <param name="listeners">The listeners to process the response.</param>
        public override void SendRequest(out string errorLog, params Action<IAccountInfoResponse>[] listeners)
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
            if (string.IsNullOrEmpty(functionName)) 
            { 
                errorLog = $"{nameof(functionName)} is null or empty."; 
            }

            listeners.ForEach(l => onGetResult += l);
            var request = TakeRequest();
            PlayFabCloudScriptAPI.ExecuteFunction(request, OnGetAccountInfoResult_EventListener, OnError);
        }

        /// <summary>
        /// Takes the request for getting account information.
        /// </summary>
        /// <returns>The request to get account information.</returns>
        private ExecuteFunctionRequest TakeRequest()
        {
            return new ExecuteFunctionRequest
            {
                FunctionName = functionName,
                FunctionParameter = new
                {
                    email = email,
                    password = password
                },
                GeneratePlayStreamEvent = true
            };
        }

        /// <summary>
        /// Handles the event when the account info result is received.
        /// </summary>
        /// <param name="result">The result of the account info request.</param>
        private void OnGetAccountInfoResult_EventListener(ExecuteFunctionResult result)
        {
            var response = new Requests.Data.Entity.AccountInfoResponse();
            var json = result.FunctionResult.ToString();
            var infoObject = JsonConvert.DeserializeObject<Core.Accounts.AccountInfoResponse>(json);
            response.SetResponse(true, infoObject.UserName, infoObject.Rating, infoObject.LaunchCount, result, "Get account info succeed.");
            onGetResult?.Invoke(response);
        }

        /// <summary>
        /// Handles errors that occur during the account info request.
        /// </summary>
        /// <param name="error">The error that occurred.</param>
        private void OnError(PlayFabError error)
        {
            var response = new Requests.Data.Entity.AccountInfoResponse();
            response.IsRequestSuccess = false;
            response.Message = error.ErrorMessage;
            onGetResult?.Invoke(response);
        }

        #endregion
    }
}
```

## Fields
- **email**: The email associated with the account.
- **password**: The password associated with the account.
- **functionName**: The name of the function to execute on PlayFab.

## Methods
### Public Methods
- **SendRequest(out string errorLog, params Action<IAccountInfoResponse>[] listeners)**: Sends the request to get account information from PlayFab.
  - **Parameters**:
    - **errorLog**: The error log to be populated in case of an error.
    - **listeners**: The listeners to process the response.

### Private Methods
- **TakeRequest()**: Constructs the request for getting account information.
- **OnGetAccountInfoResult_EventListener(ExecuteFunctionResult result)**: Handles the event when the account info result is received.
- **OnError(PlayFabError error)**: Handles errors that occur during the request.

## Usage Example
Below is an example of how to use the `GetAccountInfoController_Playfab` class in a Unity project.

```csharp
using UnityEngine;
using flameborn.Sdk.Controllers.Data;

public class ExampleUsage : MonoBehaviour
{
    private void Start()
    {
        var controller = new GetAccountInfoController_Playfab("example@example.com", "password123");
        controller.SendRequest(out string errorLog, response => 
        {
            if (response.IsRequestSuccess)
            {
                Debug.Log("Account information retrieved successfully.");
            }
            else
            {
                Debug.LogError("Failed to retrieve account information: " + response.Message);
            }
        });
    }
}
```

## See Also
For more information on the `IAccountInfoResponse` interface, refer to the [IAccountInfoResponse documentation](https://github.com/gkhanC/flameborn-game/tree/dev/documents/IAccountInfoResponse).

## File Location
This class is defined in the `GetAccountInfoController_Playfab.cs` file, located in the `flameborn.Sdk.Controllers.Data` namespace.
