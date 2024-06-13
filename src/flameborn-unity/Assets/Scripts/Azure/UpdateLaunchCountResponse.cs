using System;
using Newtonsoft.Json;

namespace Flameborn.Azure
{
    [Serializable]
    public class UpdateLaunchCountResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("launchCount")]
        public int LaunchCount { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
