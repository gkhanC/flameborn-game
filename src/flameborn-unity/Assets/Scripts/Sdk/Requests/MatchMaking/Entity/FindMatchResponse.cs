using System;
using flameborn.Sdk.Requests.Entity;
using flameborn.Sdk.Requests.MatchMaking.Abstract;

namespace flameborn.Sdk.Requests.MatchMaking.Entity
{
    /// <summary>
    /// Represents a response for finding a match.
    /// </summary>
    [Serializable]
    public class FindMatchResponse : ResponseEntity, IFindMatchResponse
    {
        #region Properties

        /// <summary>
        /// Gets or sets the match ID of the found match.
        /// </summary>
        public string MatchId { get; set; } = string.Empty;

        #endregion

        #region Methods

        /// <summary>
        /// Sets the response details.
        /// </summary>
        /// <typeparam name="T">The type of the response.</typeparam>
        /// <param name="isSuccess">Indicates whether the operation was successful.</param>
        /// <param name="matchId">The match ID of the found match.</param>
        /// <param name="response">The response object.</param>
        /// <param name="message">An optional message describing the result.</param>
        public void SetResponse<T>(bool isSuccess, string matchId, T response, string message = "")
        {
            MatchId = matchId;
            SetResponse(isSuccess, response, message);
        }

        #endregion
    }
}
