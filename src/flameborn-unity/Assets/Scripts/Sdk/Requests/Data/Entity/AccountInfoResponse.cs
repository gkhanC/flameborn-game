using System;
using flameborn.Sdk.Requests.Data.Abstract;
using flameborn.Sdk.Requests.Entity;

namespace flameborn.Sdk.Requests.Data.Entity
{
    /// <summary>
    /// Represents the response for account information.
    /// </summary>
    [Serializable]
    public class AccountInfoResponse : ResponseEntity, IAccountInfoResponse
    {
        #region Properties

        /// <summary>
        /// Gets or sets the launch count.
        /// </summary>
        public int LaunchCount { get; set; } = 0;

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        public int Rating { get; set; } = 0;

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        #endregion

        #region Methods

        /// <summary>
        /// Sets the response details.
        /// </summary>
        /// <typeparam name="T">The type of the response.</typeparam>
        /// <param name="isSuccess">Indicates if the response is successful.</param>
        /// <param name="userName">The user name.</param>
        /// <param name="rating">The rating.</param>
        /// <param name="launchCount">The launch count.</param>
        /// <param name="response">The response object.</param>
        /// <param name="message">The message associated with the response.</param>
        public void SetResponse<T>(bool isSuccess, string userName, int rating, int launchCount, T response, string message = "")
        {
            UserName = userName;
            Rating = rating;
            LaunchCount = launchCount;
            SetResponse(isSuccess, response, message);
        }

        #endregion
    }
}
