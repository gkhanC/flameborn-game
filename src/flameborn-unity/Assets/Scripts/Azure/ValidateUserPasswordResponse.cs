using System;
using Newtonsoft.Json;

namespace Flameborn.Azure
{
    [Serializable]
    internal class ValidateUserPasswordResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
