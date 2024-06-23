
# FindMatchController_Playfab Class Documentation

## Overview
The `FindMatchController_Playfab` class manages finding a match via PlayFab within the Flameborn SDK. This class is derived from the `Controller<IFindMatchResponse>` class and implements the `IApiController<IFindMatchResponse>` interface.

## Class Definition

```csharp
using System;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.MatchMaking.Abstract;
using flameborn.Sdk.Requests.MatchMaking.Entity;
using HF.Extensions;
using PlayFab;
using PlayFab.MultiplayerModels;

namespace flameborn.Sdk.Controllers.MatchMaking
{
    /// <summary>
    /// Controller for finding a match via PlayFab.
    /// </summary>
    [Serializable]
    public class FindMatchController_Playfab : Controller<IFindMatchResponse>, IApiController<IFindMatchResponse>
    {
        #region Fields

        private string ticketId;
        private string queueName;
        private event Action<IFindMatchResponse> onGetResult;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FindMatchController_Playfab"/> class.
        /// </summary>
        /// <param name="ticketId">The ID of the matchmaking ticket.</param>
        /// <param name="queueName">The name of the queue.</param>
        public FindMatchController_Playfab(string ticketId, string queueName)
        {
            this.ticketId = ticketId;
            this.queueName = queueName;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sends the request to find a match.
        /// </summary>
        /// <param name="errorLog">The error log to be populated in case of an error.</param>
        /// <param name="listeners">The listeners to process the response.</param>
        public override void SendRequest(out string errorLog, params Action<IFindMatchResponse>[] listeners)
        {
            errorLog = "";
            if (string.IsNullOrEmpty(queueName)) 
            { 
                errorLog = $"{nameof(queueName)} is null or empty."; 
            }
            if (string.IsNullOrEmpty(ticketId)) 
            { 
                errorLog = $"{nameof(ticketId)} is null or empty."; 
            }

            listeners.ForEach(l => onGetResult += l);

            var request = TakeRequest();
            PlayFabMultiplayerAPI.GetMatchmakingTicket(request, OnGetMatchResult_EventListener, OnError);
        }

        /// <summary>
        /// Takes the request for getting a matchmaking ticket.
        /// </summary>
        /// <returns>The request to get a matchmaking ticket.</returns>
        private GetMatchmakingTicketRequest TakeRequest()
        {
            return new GetMatchmakingTicketRequest
            {
                TicketId = ticketId,
                QueueName = queueName,
            };
        }

        /// <summary>
        /// Handles the event when the match result is received.
        /// </summary>
        /// <param name="result">The result of the matchmaking request.</param>
        private void OnGetMatchResult_EventListener(GetMatchmakingTicketResult result)
        {
            var response = new FindMatchResponse();
            response.IsRequestSuccess = result.Status == "Matched";

            if (response.IsRequestSuccess)
            {
                response.SetResponse(true, result.MatchId, result, "New match found.");
            }
            else
            {
                response.Message = "Available match not found.";
            }

            onGetResult?.Invoke(response);
        }

        /// <summary>
        /// Handles errors that occur during the matchmaking request.
        /// </summary>
        /// <param name="error">The error that occurred.</param>
        private void OnError(PlayFabError error)
        {
            var response = new FindMatchResponse();
            response.IsRequestSuccess = false;
            response.Message = error.ErrorMessage;
            onGetResult?.Invoke(response);
        }

        #endregion
    }
}
```

## Fields
- **ticketId**: The ID of the matchmaking ticket.
- **queueName**: The name of the queue.

## Methods
### Public Methods
- **SendRequest(out string errorLog, params Action<IFindMatchResponse>[] listeners)**: Sends the request to find a match via PlayFab.
  - **Parameters**:
    - **errorLog**: The error log to be populated in case of an error.
    - **listeners**: The listeners to process the response.

### Private Methods
- **TakeRequest()**: Constructs the request for getting a matchmaking ticket.
- **OnGetMatchResult_EventListener(GetMatchmakingTicketResult result)**: Handles the event when the match result is received.
- **OnError(PlayFabError error)**: Handles errors that occur during the request.

## Usage Example
Below is an example of how to use the `FindMatchController_Playfab` class in a Unity project.

```csharp
using UnityEngine;
using flameborn.Sdk.Controllers.MatchMaking;

public class ExampleUsage : MonoBehaviour
{
    private void Start()
    {
        var controller = new FindMatchController_Playfab("ticket-id", "queue-name");
        controller.SendRequest(out string errorLog, response => 
        {
            if (response.IsRequestSuccess)
            {
                Debug.Log("Match found: " + response.MatchId);
            }
            else
            {
                Debug.LogError("Failed to find match: " + response.Message);
            }
        });
    }
}
```

## See Also
For more information on the `IFindMatchResponse` interface, refer to the [IFindMatchResponse documentation](https://gkhanc.github.io/flameborn-game/IFindMatchResponse).

## File Location
This class is defined in the `FindMatchController_Playfab.cs` file, located in the `flameborn.Sdk.Controllers.MatchMaking` namespace.
