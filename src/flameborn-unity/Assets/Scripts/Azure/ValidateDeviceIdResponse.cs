using System;
using Newtonsoft.Json;

namespace Flameborn.Azure
{
    [Serializable]
    internal class ValidateDeviceIdResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; } = false;

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
