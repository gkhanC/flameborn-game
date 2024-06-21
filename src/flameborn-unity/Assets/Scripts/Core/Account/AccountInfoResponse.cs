using Newtonsoft.Json;
using Sirenix.OdinInspector;

namespace flameborn.Core.Accounts
{
    [Searchable]
    public class AccountInfoResponse
    {
        [JsonProperty("Success")]
        public bool Success { get; set; } = false;

        [JsonProperty("UserName")]
        public string UserName { get; set; } = string.Empty;

        [JsonProperty("Rating")]
        public int Rating { get; set; } = 0;

        [JsonProperty("LaunchCount")]
        public int LaunchCount { get; set; } = 0;

    }
}