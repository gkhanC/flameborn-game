using Newtonsoft.Json;

namespace Flameborn.Azure
{
    internal class ValidateUserEmailResponse : ValidateDeviceIdResponse
    {
        [JsonProperty("email")]
        public bool Email { get; set; }

        [JsonProperty("device")]
        public bool Device { get; set; }
    }
}
