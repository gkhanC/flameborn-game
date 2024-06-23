
# GetMatchInfoController_Playfab Class Documentation

## Overview
The `GetMatchInfoController_Playfab` class manages getting match information via PlayFab within the Flameborn SDK. This class is derived from the `Controller<IGetMatchInfoResponse>` class and implements the `IApiController<IGetMatchInfoResponse>` interface.

## Class Definition

```csharp
using System;
using System.Collections.Generic;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.MatchMaking.Abstract;
using flameborn.Sdk.Requests.MatchMaking.Entity;
using HF.Extensions;
using Newtonsoft.Json;
using PlayFab;
using PlayFab.MultiplayerModels;

namespace flameborn.Sdk.Controllers.MatchMaking
{
    /// <summary>
    /// Controller for getting match information via PlayFab.
    /// </summary>
    [Serializable]
    public class GetMatchInfoController_Playfab : Controller<IGetMatchInfoResponse>, IApiController<IGetMatchInfoResponse>
    {
        #region Fields

        private string matchId;
        private string queueName;
        private event Action<IGetMatchInfoResponse> onGetResult;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GetMatchInfoController_Playfab"/> class.
        /// </summary>
        /// <param name="matchId">The ID of the match.</param>
        /// <param name="queue">The name of the queue.</param>
        public GetMatchInfoController_Playfab(string matchId, string queue)
        {
            this.matchId = matchId;
            this.queueName = queue;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sends the request to get match information.
        /// </summary>
        /// <param name="errorLog">The error log to be populated in case of an error.</param>
        /// <param name="listeners">The listeners to process the response.</param>
        public override void SendRequest(out string errorLog, params Action<IGetMatchInfoResponse>[] listeners)
        {
            errorLog = "";
            if (string.IsNullOrEmpty(queueName)) 
            { 
                errorLog = $"{nameof(queueName)} is null or empty."; 
            }
            if (string.IsNullOrEmpty(matchId)) 
            { 
                errorLog = $"{nameof(matchId)} is null or empty."; 
            }

            listeners.ForEach(l => onGetResult += l);

            var request = TakeRequest();
            PlayFabMultiplayerAPI.GetMatch(request, OnGetMatchResult_EventListener, OnError);
        }

        /// <summary>
        /// Takes the request for getting match information.
        /// </summary>
        /// <returns>The request to get match information.</returns>
        public GetMatchRequest TakeRequest()
        {
            return new GetMatchRequest
            {
                MatchId = matchId,
                QueueName = queueName,
                ReturnMemberAttributes = true
            };
        }

        /// <summary>
        /// Handles the event when the match information result is received.
        /// </summary>
        /// <param name="result">The result of the match information request.</param>
        private void OnGetMatchResult_EventListener(GetMatchResult result)
        {
            var response = new GetMatchInfoResponse();
            var data = new List<PlayerData>();
            var success = result.Members.Count > 1;
            var message = success ? "Players data saved." : "Players data not found.";

            if (success)
            {
                result.Members.ForEach(m =>
                {
                    var json = m.Attributes.DataObject.ToString();
                    var playerData = JsonConvert.DeserializeObject<PlayerData>(json);
                    data.Add(playerData);
                });
            }

            response.SetResponse(success, data, result, message);

            onGetResult?.Invoke(response);
        }

        /// <summary>
        /// Handles errors that occur during the match information request.
        /// </summary>
        /// <param name="error">The error that occurred.</param>
        private void OnError(PlayFabError error)
        {
            var response = new GetMatchInfoResponse();
            response.IsRequestSuccess = false;
            response.Message = error.ErrorMessage;
            onGetResult?.Invoke(response);
        }

        #endregion
    }
}
```

## Fields
- **matchId**: The ID of the match.
- **queueName**: The name of the queue.

## Methods
### Public Methods
- **SendRequest(out string errorLog, params Action<IGetMatchInfoResponse>[] listeners)**: Sends the request to get match information via PlayFab.
  - **Parameters**:
    - **errorLog**: The error log to be populated in case of an error.
    - **listeners**: The listeners to process the response.

### Private Methods
- **TakeRequest()**: Constructs the request for getting match information.
- **OnGetMatchResult_EventListener(GetMatchResult result)**: Handles the event when the match information result is received.
- **OnError(PlayFabError error)**: Handles errors that occur during the request.

## Usage Example
Below is an example of how to use the `GetMatchInfoController_Playfab` class in a Unity project.

```csharp
using UnityEngine;
using flameborn.Sdk.Controllers.MatchMaking;

public class ExampleUsage : MonoBehaviour
{
    private void Start()
    {
        var controller = new GetMatchInfoController_Playfab("match-id", "queue-name");
        controller.SendRequest(out string errorLog, response => 
        {
            if (response.IsRequestSuccess)
            {
                Debug.Log("Match information retrieved.");
            }
            else
            {
                Debug.LogError("Failed to retrieve match information: " + response.Message);
            }
        });
    }
}
```

## See Also
For more information on the `IGetMatchInfoResponse` interface, refer to the [IGetMatchInfoResponse documentation](https://github.com/gkhanC/flameborn-game/tree/dev/documents/IGetMatchInfoResponse).

## File Location
This class is defined in the `GetMatchInfoController_Playfab.cs` file, located in the `flameborn.Sdk.Controllers.MatchMaking` namespace.
