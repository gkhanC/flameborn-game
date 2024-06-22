using Newtonsoft.Json;

namespace flameborn.Core.Accounts
{
    /// <summary>
    /// Represents the response received after updating an account.
    /// </summary>
    public class UpdatedAccountResponse
    {
        #region Properties

        /// <summary>
        /// Indicates whether the update was successful.
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; } = false;

        /// <summary>
        /// Gets or sets the message associated with the update response.
        /// </summary>
        [JsonProperty("Message")]
        public string Message { get; set; } = string.Empty;

        #endregion
    }
}
