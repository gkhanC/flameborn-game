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
