using Newtonsoft.Json;
using Sirenix.OdinInspector;

namespace flameborn.Core.Accounts
{
    /// <summary>
    /// Represents the response containing account information.
    /// </summary>
    [Searchable]
    public class AccountInfoResponse
    {
        #region Properties

        /// <summary>
        /// Indicates whether the response was successful.
        /// </summary>
        [JsonProperty("Success")]
        public bool Success { get; set; } = false;

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        [JsonProperty("UserName")]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's rating.
        /// </summary>
        [JsonProperty("Rating")]
        public int Rating { get; set; } = 0;

        /// <summary>
        /// Gets or sets the launch count.
        /// </summary>
        [JsonProperty("LaunchCount")]
        public int LaunchCount { get; set; } = 0;

        #endregion
    }
}
