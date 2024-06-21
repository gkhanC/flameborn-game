using System;
using flameborn.Sdk.Configurations.Abstract;
using Newtonsoft.Json;
using UnityEngine;

namespace flameborn.Sdk.Configurations
{
    [Serializable]
    public class AzureConfiguration : IConfiguration
    {
        [field:SerializeField][JsonProperty("DeviceIdValidationUrl")]
        public string DeviceIdValidationUrl { get; set; } = string.Empty;
    }
}