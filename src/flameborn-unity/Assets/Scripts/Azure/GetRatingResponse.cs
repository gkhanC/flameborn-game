using System;
using Newtonsoft.Json;

namespace Flameborn.Azure
{
    [Serializable]
    internal class GetRatingResponse
    {
        /// <summary>
        /// Indicates whether the operation was successful.
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// The rating of the device.
        /// </summary>
        [JsonProperty("rating")]
        public int Rating { get; set; }

        /// <summary>
        /// The message associated with the response.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
