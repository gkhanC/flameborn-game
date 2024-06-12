using System;
using Newtonsoft.Json;

namespace Flameborn.Azure
{
    [Serializable]
    internal class ValidateUserPasswordResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("launchCount")]
        public int LaunchCount { get; set; }

        [JsonProperty("rating")]
        public int Rating { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
