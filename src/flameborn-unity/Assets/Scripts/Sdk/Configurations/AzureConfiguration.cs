using System;
using flameborn.Sdk.Configurations.Abstract;
using Newtonsoft.Json;
using UnityEngine;

namespace flameborn.Sdk.Configurations
{
    /// <summary>
    /// Represents the Azure configuration settings.
    /// </summary>
    [Serializable]
    public class AzureConfiguration : IConfiguration
    {
        #region Properties

        /// <summary>
        /// Gets or sets the URL for device ID validation.
        /// </summary>
        [field: SerializeField]
        [JsonProperty("DeviceIdValidationUrl")]
        public string DeviceIdValidationUrl { get; set; } = string.Empty;

        #endregion
    }
}
