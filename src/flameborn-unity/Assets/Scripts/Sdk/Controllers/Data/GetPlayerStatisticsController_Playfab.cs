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
    /// <summary>
    /// Controller for getting player statistics from PlayFab.
    /// </summary>
    public class GetPlayerStatisticsController_Playfab : Controller<IGetStatisticsResponse>, IApiController<IGetStatisticsResponse>
    {
        #region Fields

        private string[] statistics;
        private event Action<IGetStatisticsResponse> onGetResult;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPlayerStatisticsController_Playfab"/> class.
        /// </summary>
        /// <param name="statistics">The statistics to be retrieved.</param>
        public GetPlayerStatisticsController_Playfab(params string[] statistics)
        {
            this.statistics = statistics;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sends the request to get player statistics.
        /// </summary>
        /// <param name="errorLog">The error log to be populated in case of an error.</param>
        /// <param name="listeners">The listeners to process the response.</param>
        public override void SendRequest(out string errorLog, params Action<IGetStatisticsResponse>[] listeners)
        {
            errorLog = "";
            if (statistics.IsNull() || statistics.Length == 0) 
            { 
                errorLog = "Get statistics controller does not have any statistics information."; 
                return; 
            }

            listeners.ForEach(l => onGetResult += l);
            var request = TakeRequest();
            PlayFabClientAPI.GetPlayerStatistics(request, OnGetStatisticsResult_EventListener, OnError);
        }

        /// <summary>
        /// Takes the request for getting player statistics.
        /// </summary>
        /// <returns>The request to get player statistics.</returns>
        private GetPlayerStatisticsRequest TakeRequest()
        {
            return new GetPlayerStatisticsRequest
            {
                StatisticNames = statistics.ToList()
            };
        }

        /// <summary>
        /// Handles the event when the statistics result is received.
        /// </summary>
        /// <param name="result">The result of the statistics request.</param>
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

        /// <summary>
        /// Handles errors that occur during the statistics request.
        /// </summary>
        /// <param name="error">The error that occurred.</param>
        private void OnError(PlayFabError error)
        {
            var response = new GetStatisticsResponse();
            response.IsRequestSuccess = false;
            response.Message = error.ErrorMessage;
            onGetResult?.Invoke(response);
        }

        #endregion
    }
}
