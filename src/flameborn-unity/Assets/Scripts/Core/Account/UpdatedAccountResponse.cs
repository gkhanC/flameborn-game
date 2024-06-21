using Newtonsoft.Json;

namespace flameborn.Core.Accounts
{
    public class UpdatedAccountResponse
    {
        [JsonProperty("success")]
        public bool success { get; set; } = false;

        [JsonProperty("Message")]
        public string Message { get; set; } = string.Empty;
    }
}