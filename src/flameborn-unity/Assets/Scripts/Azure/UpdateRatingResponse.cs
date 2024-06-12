using System;
using Newtonsoft.Json;

namespace Flameborn.Azure
{
    [Serializable]
    internal class UpdateRatingResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
