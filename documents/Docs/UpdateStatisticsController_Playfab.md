
# UpdateStatisticsController_Playfab Class Documentation

## Overview
The `UpdateStatisticsController_Playfab` class manages the updating of player statistics on PlayFab within the Flameborn SDK. This class is derived from the `Controller<IUpdateStatisticsResponse>` class and implements the `IApiController<IUpdateStatisticsResponse>` interface.

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
    /// Controller for updating player statistics on PlayFab.
    /// </summary>
    public class UpdateStatisticsController_Playfab : Controller<IUpdateStatisticsResponse>, IApiController<IUpdateStatisticsResponse>
    {
        private (string name, int value)[] statistics;
        private event Action<IUpdateStatisticsResponse> onGetResult;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateStatisticsController_Playfab"/> class.
        /// </summary>
        /// <param name="statistics">The statistics to update.</param>
        public UpdateStatisticsController_Playfab(params (string name, int value)[] statistics)
        {
            this.statistics = statistics;
        }

        /// <summary>
        /// Sends the request to update player statistics.
        /// </summary>
        /// <param name="errorLog">The error log to be populated in case of an error.</param>
        /// <param name="listeners">The listeners to process the response.</param>
        public override void SendRequest(out string errorLog, params Action<IUpdateStatisticsResponse>[] listeners)
        {
            errorLog = "";
            if (statistics == null || statistics.Length == 0)
            {
                errorLog = $"{nameof(statistics)} is null or empty.";
                return;
            }

            listeners.ForEach(l => onGetResult += l);
            var request = TakeRequest();
            PlayFabClientAPI.UpdatePlayerStatistics(request, OnUpdateStatisticsResult_EventListener, OnError);
        }

        private void OnUpdateStatisticsResult_EventListener(UpdatePlayerStatisticsResult result)
        {
            var response = new UpdateStatisticsResponse();
            response.SetResponse(true, result, "Update statistics succeeded.");
            onGetResult?.Invoke(response);
        }

        private void OnError(PlayFabError error)
        {
            var response = new UpdateStatisticsResponse();
            response.IsRequestSuccess = false;
            response.Message = error.ErrorMessage;
            onGetResult?.Invoke(response);
        }

        private UpdatePlayerStatisticsRequest TakeRequest()
        {
            return new UpdatePlayerStatisticsRequest
            {
                Statistics = statistics.Select(stat => new StatisticUpdate { StatisticName = stat.name, Value = stat.value }).ToList()
            };
        }
    }
}
```

## Fields
- **statistics**: The statistics to update on PlayFab.

## Methods
### Public Methods
- **SendRequest(out string errorLog, params Action<IUpdateStatisticsResponse>[] listeners)**: Sends the request to update player statistics on PlayFab.
  - **Parameters**:
    - **errorLog**: The error log to be populated in case of an error.
    - **listeners**: The listeners to process the response.

### Private Methods
- **TakeRequest()**: Constructs the request for updating player statistics.
- **OnUpdateStatisticsResult_EventListener(UpdatePlayerStatisticsResult result)**: Handles the event when the player statistics update result is received.
- **OnError(PlayFabError error)**: Handles errors that occur during the request.

## Usage Example
Below is an example of how to use the `UpdateStatisticsController_Playfab` class in a Unity project.

```csharp
using UnityEngine;
using flameborn.Sdk.Controllers.Data;

public class ExampleUsage : MonoBehaviour
{
    private void Start()
    {
        var controller = new UpdateStatisticsController_Playfab(("statistic1", 100), ("statistic2", 200));
        controller.SendRequest(out string errorLog, response => 
        {
            if (response.IsRequestSuccess)
            {
                Debug.Log("Player statistics updated successfully.");
            }
            else
            {
                Debug.LogError("Failed to update player statistics: " + response.Message);
            }
        });
    }
}
```

## See Also
For more information on the `IUpdateStatisticsResponse` interface, refer to the [IUpdateStatisticsResponse documentation](https://github.com/gkhanC/flameborn-game/tree/dev/documents/IUpdateStatisticsResponse).

## File Location
This class is defined in the `UpdateStatisticsController_Playfab.cs` file, located in the `flameborn.Sdk.Controllers.Data` namespace.
