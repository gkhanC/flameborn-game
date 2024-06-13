using System;
using Newtonsoft.Json;

namespace Flameborn.Azure
{
    [Serializable]
    public class RecoveryUserPasswordResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
