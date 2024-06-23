
# GetPlayerStatisticsController_Playfab Class Documentation

## Overview
The `GetPlayerStatisticsController_Playfab` class manages the retrieval of player statistics from PlayFab within the Flameborn SDK. This class is derived from the `Controller<IGetStatisticsResponse>` class and implements the `IApiController<IGetStatisticsResponse>` interface.

## Class Definition

```csharp
using System;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.Data.Abstract;
using flameborn.Sdk.Requests.Data.Entity;
using HF.Extensions;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace flameborn.Sdk.Controllers.Data
{
    /// <summary>
    /// Controller for getting player statistics from PlayFab.
    /// </summary>
    public class GetPlayerStatisticsController_Playfab : Controller<IGetStatisticsResponse>, IApiController<IGetStatisticsResponse>
    {
        private string[] statisticNames;
        private event Action<IGetStatisticsResponse> onGetResult;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPlayerStatisticsController_Playfab"/> class.
        /// </summary>
        /// <param name="statisticNames">The names of the statistics to retrieve.</param>
        public GetPlayerStatisticsController_Playfab(params string[] statisticNames)
        {
            this.statisticNames = statisticNames;
        }

        /// <summary>
        /// Sends the request to get player statistics.
        /// </summary>
        /// <param name="errorLog">The error log to be populated in case of an error.</param>
        /// <param name="listeners">The listeners to process the response.</param>
        public override void SendRequest(out string errorLog, params Action<IGetStatisticsResponse>[] listeners)
        {
            errorLog = "";
            if (statisticNames == null || statisticNames.Length == 0)
            {
                errorLog = $"{nameof(statisticNames)} is null or empty.";
                return;
            }

            listeners.ForEach(l => onGetResult += l);
            var request = TakeRequest();
            PlayFabClientAPI.GetPlayerStatistics(request, OnGetPlayerStatisticsResult_EventListener, OnError);
        }

        private void OnGetPlayerStatisticsResult_EventListener(GetPlayerStatisticsResult result)
        {
            var response = new GetStatisticsResponse();
            response.SetResponse(result.Statistics.ToDictionary(stat => stat.StatisticName, stat => stat.Value));
            onGetResult?.Invoke(response);
        }

        private void OnError(PlayFabError error)
        {
            var response = new GetStatisticsResponse();
            response.IsRequestSuccess = false;
            response.Message = error.ErrorMessage;
            onGetResult?.Invoke(response);
        }

        private GetPlayerStatisticsRequest TakeRequest()
        {
            return new GetPlayerStatisticsRequest
            {
                StatisticNames = statisticNames
            };
        }
    }
}
```

## Fields
- **statisticNames**: The names of the statistics to retrieve from PlayFab.

## Methods
### Public Methods
- **SendRequest(out string errorLog, params Action<IGetStatisticsResponse>[] listeners)**: Sends the request to get player statistics from PlayFab.
  - **Parameters**:
    - **errorLog**: The error log to be populated in case of an error.
    - **listeners**: The listeners to process the response.

### Private Methods
- **TakeRequest()**: Constructs the request for getting player statistics.
- **OnGetPlayerStatisticsResult_EventListener(GetPlayerStatisticsResult result)**: Handles the event when the player statistics result is received.
- **OnError(PlayFabError error)**: Handles errors that occur during the request.

## Usage Example
Below is an example of how to use the `GetPlayerStatisticsController_Playfab` class in a Unity project.

```csharp
using UnityEngine;
using flameborn.Sdk.Controllers.Data;

public class ExampleUsage : MonoBehaviour
{
    private void Start()
    {
        var controller = new GetPlayerStatisticsController_Playfab("statistic1", "statistic2");
        controller.SendRequest(out string errorLog, response => 
        {
            if (response.IsRequestSuccess)
            {
                Debug.Log("Player statistics retrieved successfully.");
            }
            else
            {
                Debug.LogError("Failed to retrieve player statistics: " + response.Message);
            }
        });
    }
}
```

## See Also
For more information on the `IGetStatisticsResponse` interface, refer to the [IGetStatisticsResponse documentation](https://gkhanc.github.io/flameborn-game/IGetStatisticsResponse).

## File Location
This class is defined in the `GetPlayerStatisticsController_Playfab.cs` file, located in the `flameborn.Sdk.Controllers.Data` namespace.
