using System;
using System.Collections.Generic;
using flameborn.Sdk.Requests.Entity;
using flameborn.Sdk.Requests.MatchMaking.Abstract;

namespace flameborn.Sdk.Requests.MatchMaking.Entity
{
    /// <summary>
    /// Represents a response for getting match information.
    /// </summary>
    [Serializable]
    public class GetMatchInfoResponse : ResponseEntity, IGetMatchInfoResponse
    {
        #region Properties

        /// <summary>
        /// Gets or sets the list of player data in the match.
        /// </summary>
        public List<PlayerData> Players { get; set; } = new List<PlayerData>();

        #endregion

        #region Methods

        /// <summary>
        /// Sets the response details.
        /// </summary>
        /// <typeparam name="T">The type of the response.</typeparam>
        /// <param name="isSuccess">Indicates whether the operation was successful.</param>
        /// <param name="players">The list of player data.</param>
        /// <param name="response">The response object.</param>
        /// <param name="message">An optional message describing the result.</param>
        public void SetResponse<T>(bool isSuccess, List<PlayerData> players, T response, string message = "")
        {
            Players = players;
            SetResponse(isSuccess, response, message);
        }

        #endregion
    }
}
