using System;
using flameborn.Core.User;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.MatchMaking.Abstract;
using flameborn.Sdk.Requests.MatchMaking.Entity;
using HF.Extensions;
using PlayFab;
using PlayFab.MultiplayerModels;

namespace flameborn.Sdk.Controllers.MatchMaking
{
    [Serializable]
    public class CreateMatchTicketController_Playfab : Controller<ICreateMatchTicketResponse>, IApiController<ICreateMatchTicketResponse>
    {
        private string queueName;
        private UserData userData;
        private event Action<ICreateMatchTicketResponse> onGetResult;

        public CreateMatchTicketController_Playfab(string queue, UserData user)
        {
            queueName = queue;
            userData = user;
        }

        public override void SendRequest(out string errorLog, params Action<ICreateMatchTicketResponse>[] listeners)
        {
            errorLog = "";

            if (string.IsNullOrEmpty(queueName)) { errorLog = $"{nameof(queueName)} is null or empty."; }
            if (userData.IsNull()) { errorLog = $"{nameof(userData)} is null or empty."; }

            listeners.ForEach(l => onGetResult += l);

            var request = TakeRequest();
            PlayFabMultiplayerAPI.CreateMatchmakingTicket(request, OnCreatedMatchTicket_EventListener_Playfab, OnError);

        }

        private void OnCreatedMatchTicket_EventListener_Playfab(CreateMatchmakingTicketResult result)
        {
            var response = new CreateMatchTicketResponse();
            response.IsRequestSuccess = !string.IsNullOrEmpty(result.TicketId);

            if (response.IsRequestSuccess)
            {
                response.SetResponse(true, result.TicketId, result, "New ticket saved.");
            }
            else
            {
                response.Message = "Match ticket creation failed";
            }

            onGetResult?.Invoke(response);
        }

        private void OnError(PlayFabError error)
        {
            var response = new CreateMatchTicketResponse();
            response.IsRequestSuccess = false;
            response.Message = error.ErrorMessage;
            onGetResult?.Invoke(response);
        }

        private CreateMatchmakingTicketRequest TakeRequest()
        {
            return new CreateMatchmakingTicketRequest
            {
                QueueName = queueName,
                GiveUpAfterSeconds = 30,
                Creator = new MatchmakingPlayer
                {
                    Entity = new EntityKey
                    {
                        Id = PlayFabSettings.staticPlayer.EntityId,
                        Type = PlayFabSettings.staticPlayer.EntityType
                    },
                    Attributes = new MatchmakingPlayerAttributes
                    {
                        DataObject = new PlayerData
                        (
                            PlayFabSettings.staticPlayer.PlayFabId,
                            userData.UserName,
                            userData.Rating.ToString(),
                            userData.Rank.ToString()
                        )
                    }
                }
            };
        }
    }
}