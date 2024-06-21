using System;
using System.Collections.Generic;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.Data.Abstract;
using flameborn.Sdk.Requests.Data.Entity;
using HF.Extensions;
using PlayFab;
using PlayFab.ClientModels;

namespace flameborn.Sdk.Controllers.Data
{
    public class UpdateStatisticsController_Playfab : Controller<IUpdateStatisticsResponse>, IApiController<IUpdateStatisticsResponse>
    {
        List<StatisticUpdate> statistics = new List<StatisticUpdate>();
        private event Action<IUpdateStatisticsResponse> onGetResult;

        public UpdateStatisticsController_Playfab((string name, int value)[] statistics)
        {
            statistics.ForEach(s =>
            {
                this.statistics.Add(new StatisticUpdate { StatisticName = s.name, Value = s.value });
            });
        }

        public override void SendRequest(out string errorLog, params Action<IUpdateStatisticsResponse>[] listeners)
        {
            errorLog = "";
            if (statistics.Count == 0) { errorLog = $"{nameof(statistics)} is null or empty."; }

            listeners.ForEach(l => onGetResult += l);

            var request = TakeRequest();
            PlayFabClientAPI.UpdatePlayerStatistics(request, OnGetUpdateStatisticsResult_EventListener, OnError);
        }

        private void OnGetUpdateStatisticsResult_EventListener(UpdatePlayerStatisticsResult result)
        {
            var response = new UpdateStatisticsResponse();
            response.SetResponse(true, result, "Statistics update succeed.");
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
                Statistics = this.statistics
            };
        }
    }
}