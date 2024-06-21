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
    [Serializable]
    public class GetMatchInfoController_Playfab : Controller<IGetMatchInfoResponse>, IApiController<IGetMatchInfoResponse>
    {
        string matchId;
        string queueName;
        private event Action<IGetMatchInfoResponse> onGetResult;

        public GetMatchInfoController_Playfab(string matchId, string queue)
        {
            this.matchId = matchId;
            this.queueName = queue;
        }

        public override void SendRequest(out string errorLog, params Action<IGetMatchInfoResponse>[] listeners)
        {
            errorLog = "";
            if (string.IsNullOrEmpty(queueName)) { errorLog = $"{nameof(queueName)} is null or empty."; }
            if (string.IsNullOrEmpty(matchId)) { errorLog = $"{nameof(matchId)} is null or empty."; }

            listeners.ForEach(l => onGetResult += l);

            var request = TakeRequest();
            PlayFabMultiplayerAPI.GetMatch(request, OnGetMatchResult_EventListener, OnError);
        }

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

        private void OnError(PlayFabError error)
        {
            var response = new GetMatchInfoResponse();
            response.IsRequestSuccess = false;
            response.Message = error.ErrorMessage;
            onGetResult?.Invoke(response);
        }

        public GetMatchRequest TakeRequest()
        {
            return new GetMatchRequest
            {
                MatchId = matchId,
                QueueName = queueName,
                ReturnMemberAttributes = true
            };
        }
    }
}