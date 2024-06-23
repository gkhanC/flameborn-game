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
    /// <summary>
    /// Controller for updating player statistics on PlayFab.
    /// </summary>
    public class UpdateStatisticsController_Playfab : Controller<IUpdateStatisticsResponse>, IApiController<IUpdateStatisticsResponse>
    {
        #region Fields

        private List<StatisticUpdate> statistics = new List<StatisticUpdate>();
        private event Action<IUpdateStatisticsResponse> onGetResult;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateStatisticsController_Playfab"/> class.
        /// </summary>
        /// <param name="statistics">An array of tuples containing the statistic names and values.</param>
        public UpdateStatisticsController_Playfab((string name, int value)[] statistics)
        {
            statistics.ForEach(s =>
            {
                this.statistics.Add(new StatisticUpdate { StatisticName = s.name, Value = s.value });
            });
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sends the request to update player statistics.
        /// </summary>
        /// <param name="errorLog">The error log to be populated in case of an error.</param>
        /// <param name="listeners">The listeners to process the response.</param>
        public override void SendRequest(out string errorLog, params Action<IUpdateStatisticsResponse>[] listeners)
        {
            errorLog = "";
            if (statistics.Count == 0) 
            { 
                errorLog = $"{nameof(statistics)} is null or empty."; 
            }

            listeners.ForEach(l => onGetResult += l);

            var request = TakeRequest();
            PlayFabClientAPI.UpdatePlayerStatistics(request, OnGetUpdateStatisticsResult_EventListener, OnError);
        }

        /// <summary>
        /// Takes the request for updating player statistics.
        /// </summary>
        /// <returns>The request to update player statistics.</returns>
        private UpdatePlayerStatisticsRequest TakeRequest()
        {
            return new UpdatePlayerStatisticsRequest
            {
                Statistics = this.statistics
            };
        }

        /// <summary>
        /// Handles the event when the update statistics result is received.
        /// </summary>
        /// <param name="result">The result of the update statistics request.</param>
        private void OnGetUpdateStatisticsResult_EventListener(UpdatePlayerStatisticsResult result)
        {
            var response = new UpdateStatisticsResponse();
            response.SetResponse(true, result, "Statistics update succeed.");
            onGetResult?.Invoke(response);
        }

        /// <summary>
        /// Handles errors that occur during the update statistics request.
        /// </summary>
        /// <param name="error">The error that occurred.</param>
        private void OnError(PlayFabError error)
        {
            var response = new UpdateStatisticsResponse();
            response.IsRequestSuccess = false;
            response.Message = error.ErrorMessage;
            onGetResult?.Invoke(response);
        }

        #endregion
    }
}
