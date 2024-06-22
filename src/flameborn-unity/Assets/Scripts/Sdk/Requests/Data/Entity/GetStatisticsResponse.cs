using System.Collections.Generic;
using flameborn.Sdk.Requests.Data.Abstract;
using flameborn.Sdk.Requests.Entity;

namespace flameborn.Sdk.Requests.Data.Entity
{
    /// <summary>
    /// Represents the response for getting statistics.
    /// </summary>
    public class GetStatisticsResponse : ResponseEntity, IGetStatisticsResponse
    {
        #region Properties

        /// <summary>
        /// Gets or sets the dictionary containing statistics data.
        /// </summary>
        public Dictionary<string, int> Statistics { get; set; } = new Dictionary<string, int>();

        #endregion

        #region Methods

        /// <summary>
        /// Sets the response details.
        /// </summary>
        /// <typeparam name="T">The type of the response.</typeparam>
        /// <param name="isSuccess">Indicates if the response is successful.</param>
        /// <param name="statistics">The dictionary containing statistics data.</param>
        /// <param name="response">The response object.</param>
        /// <param name="message">The message associated with the response.</param>
        public void SetResponse<T>(bool isSuccess, Dictionary<string, int> statistics, T response, string message = "")
        {
            Statistics = statistics;
            SetResponse(isSuccess, response, message);
        }

        #endregion
    }
}
