using Newtonsoft.Json;

namespace Flameborn.Azure
{
    public class UpdateDeviceDataResponse
    {
        /// <summary>
        /// Indicates whether the operation was successful.
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// The device ID associated with the response.
        /// </summary>
        [JsonProperty("deviceId")]
        public string DeviceId { get; set; }

        /// <summary>
        /// The message associated with the response.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
