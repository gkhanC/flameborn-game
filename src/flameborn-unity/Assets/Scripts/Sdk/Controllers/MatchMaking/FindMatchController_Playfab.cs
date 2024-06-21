using System;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.MatchMaking.Abstract;
using flameborn.Sdk.Requests.MatchMaking.Entity;
using HF.Extensions;
using PlayFab;
using PlayFab.MultiplayerModels;

namespace flameborn.Sdk.Controllers.MatchMaking
{
    [Serializable]
    public class FindMatchController_Playfab : Controller<IFindMatchResponse>, IApiController<IFindMatchResponse>
    {
        private string ticketId;
        private string queueName;
        private event Action<IFindMatchResponse> onGetResult;

        public FindMatchController_Playfab(string ticketId, string queueName)
        {
            this.ticketId = ticketId;
            this.queueName = queueName;
        }

        public override void SendRequest(out string errorLog, params Action<IFindMatchResponse>[] listeners)
        {
            errorLog = "";
            if (string.IsNullOrEmpty(queueName)) { errorLog = $"{nameof(queueName)} is null or empty."; }
            if (string.IsNullOrEmpty(ticketId)) { errorLog = $"{nameof(ticketId)} is null or empty."; }

            listeners.ForEach(l => onGetResult += l);

            var request = TakeRequest();
            PlayFabMultiplayerAPI.GetMatchmakingTicket(request, OnGetMatchResult_EventListener, OnError);
        }

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

        private void OnError(PlayFabError error)
        {
            var response = new FindMatchResponse();
            response.IsRequestSuccess = false;
            response.Message = error.ErrorMessage;
            onGetResult?.Invoke(response);
        }

        private GetMatchmakingTicketRequest TakeRequest()
        {
            return new GetMatchmakingTicketRequest
            {
                TicketId = ticketId,
                QueueName = queueName,
            };
        }
    }
}