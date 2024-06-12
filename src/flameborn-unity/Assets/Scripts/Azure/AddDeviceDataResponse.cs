using System;
using Newtonsoft.Json;

namespace Flameborn.Azure
{
    [Serializable]
    internal class AddDeviceDataResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        
        [JsonProperty("email")]
        public string Email { get; set; }
        
        [JsonProperty("password")]
        public string Password { get; set; }
        
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddDeviceDataResponse"/> class with specified parameters.
        /// </summary>
        /// <param name="success">Indicates whether the operation was successful.</param>
        /// <param name="email">The email associated with the response.</param>
        /// <param name="password">The password associated with the response.</param>
        /// <param name="message">The message associated with the response.</param>
        public AddDeviceDataResponse(bool success, string email, string password, string message)
        {
            Success = success;
            Email = email;
            Password = password;
            Message = message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddDeviceDataResponse"/> class.
        /// </summary>
        public AddDeviceDataResponse() { }
    }
}
