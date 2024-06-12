using System;
using Newtonsoft.Json;

namespace Flameborn.Azure
{
    [Serializable]
    internal class GetLaunchCountResponse
    {
        /// <summary>
        /// Indicates whether the operation was successful.
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// The launch count of the device.
        /// </summary>
        [JsonProperty("launchCount")]
        public int LaunchCount { get; set; }

        /// <summary>
        /// The message associated with the response.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
