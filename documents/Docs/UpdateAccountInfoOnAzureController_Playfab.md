
# UpdateAccountInfoOnAzureController_Playfab Class Documentation

## Overview
The `UpdateAccountInfoOnAzureController_Playfab` class manages the updating of account information on Azure through PlayFab within the Flameborn SDK. This class is derived from the `Controller<IUpdateStatisticsResponse>` class and implements the `IApiController<IUpdateStatisticsResponse>` interface.

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
using UnityEngine;

namespace flameborn.Sdk.Controllers.Data
{
    /// <summary>
    /// Controller for updating account information on Azure through PlayFab.
    /// </summary>
    public class UpdateAccountInfoOnAzureController_Playfab : Controller<IUpdateStatisticsResponse>, IApiController<IUpdateStatisticsResponse>
    {
        Account account;
        string functionName;
        private event Action<IUpdateStatisticsResponse> onGetResult;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAccountInfoOnAzureController_Playfab"/> class.
        /// </summary>
        /// <param name="account">The account to update.</param>
        /// <param name="functionName">The name of the function to execute. Default is "AddNewAccount".</param>
        public UpdateAccountInfoOnAzureController_Playfab(Account account, string functionName = "AddNewAccount")
        {
            this.account = account;
            this.functionName = functionName;
        }

        /// <summary>
        /// Sends a request to update account information on Azure.
        /// </summary>
        /// <param name="errorLog">The error log to be populated in case of an error.</param>
        /// <param name="listeners">The listeners to process the response.</param>
        public override void SendRequest(out string errorLog, params Action<IUpdateStatisticsResponse>[] listeners)
        {
            errorLog = "";

            if (string.IsNullOrEmpty(functionName)) { errorLog = $"{nameof(functionName)} is null or empty."; }
            
            listeners.ForEach(l => onGetResult += l);
            var request = TakeRequest();
            PlayFabCloudScriptAPI.ExecuteFunction(request, OnGetAccountInfoResult_EventListener, OnError);
        }

        private void OnGetAccountInfoResult_EventListener(ExecuteFunctionResult result)
        {
            var response = new UpdateStatisticsResponse();

            var json = result.FunctionResult.ToString();
            var data = JsonConvert.DeserializeObject<UpdatedAccountResponse>(json);

            response.SetResponse(data.Success, result, data.Message);
            onGetResult?.Invoke(response);
        }

        private void OnError(PlayFabError error)
        {
            var response = new UpdateStatisticsResponse();
            response.IsRequestSuccess = false;
            response.Message = error.ErrorMessage;
            onGetResult?.Invoke(response);
        }

        private ExecuteFunctionRequest TakeRequest()
        {
            return new ExecuteFunctionRequest
            {
                FunctionName = functionName,
                FunctionParameter = new
                {
                    deviceId = SystemInfo.deviceUniqueIdentifier,
                    email = account.Email,
                    userName = account.UserData.UserName,
                    password = account.Password,
                    launchCount = account.UserData.LaunchCount,
                    rating = account.UserData.Rating
                },
                GeneratePlayStreamEvent = true
            };
        }
    }
}
```

## Fields
- **account**: An instance of the `Account` class representing the account to be updated.
- **functionName**: The name of the function to execute on Azure.

## Methods
### Public Methods
- **SendRequest(out string errorLog, params Action<IUpdateStatisticsResponse>[] listeners)**: Sends a request to update account information on Azure.
  - **Parameters**:
    - **errorLog**: The error log to be populated in case of an error.
    - **listeners**: The listeners to process the response.

### Private Methods
- **OnGetAccountInfoResult_EventListener(ExecuteFunctionResult result)**: Handles the event when the account info result is received.
- **OnError(PlayFabError error)**: Handles errors that occur during the request.
- **TakeRequest()**: Constructs the request for updating account information.

## Usage Example
Below is an example of how to use the `UpdateAccountInfoOnAzureController_Playfab` class in a Unity project.

```csharp
using UnityEngine;
using flameborn.Core.Accounts;
using flameborn.Sdk.Controllers.Data;

public class ExampleUsage : MonoBehaviour
{
    private void Start()
    {
        var account = new Account { Email = "example@example.com", Password = "password123", UserData = new UserData { UserName = "Player1", LaunchCount = 5, Rating = 100 } };
        var controller = new UpdateAccountInfoOnAzureController_Playfab(account);
        controller.SendRequest(out string errorLog, response => 
        {
            if (response.IsRequestSuccess)
            {
                Debug.Log("Account information updated successfully.");
            }
            else
            {
                Debug.LogError("Failed to update account information: " + response.Message);
            }
        });
    }
}
```

## See Also
For more information on the `IUpdateStatisticsResponse` interface, refer to the [IUpdateStatisticsResponse documentation](https://gkhanc.github.io/flameborn-game/IUpdateStatisticsResponse).

## File Location
This class is defined in the `UpdateAccountInfoOnAzureController_Playfab.cs` file, located in the `flameborn.Sdk.Controllers.Data` namespace.
