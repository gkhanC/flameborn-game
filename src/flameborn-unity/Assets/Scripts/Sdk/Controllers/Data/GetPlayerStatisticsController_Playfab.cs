using System;
using System.Collections.Generic;
using System.Linq;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.Data.Abstract;
using flameborn.Sdk.Requests.Data.Entity;
using HF.Extensions;
using PlayFab;
using PlayFab.ClientModels;

namespace flameborn.Sdk.Controllers.Data
{
    public class GetPlayerStatisticsController_Playfab : Controller<IGetStatisticsResponse>, IApiController<IGetStatisticsResponse>
    {
        string[] statistics;
        private event Action<IGetStatisticsResponse> onGetResult;
        public GetPlayerStatisticsController_Playfab(params string[] statistics)
        {
            this.statistics = statistics;
        }

        public override void SendRequest(out string errorLog, params Action<IGetStatisticsResponse>[] listeners)
        {
            errorLog = "";
            if (statistics.IsNull() || statistics.Length == 0) { errorLog = "Get statistics controller does not have any statistics information."; return; }

            listeners.ForEach(l => onGetResult += l);
            var request = TakeRequest();
            PlayFabClientAPI.GetPlayerStatistics(request, OnGetStatisticsResult_EventListener, OnError);
        }

        private void OnGetStatisticsResult_EventListener(GetPlayerStatisticsResult result)
        {
            var response = new GetStatisticsResponse();
            var stats = new Dictionary<string, int>();

            result.Statistics.ForEach(s =>
            {
                stats.Add(s.StatisticName, s.Value);
            });

            response.SetResponse((stats.Count > 0), stats, result, "Statistics saved.");

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
                StatisticNames = statistics.ToList()
            };
        }
    }
}