using System;
using flameborn.Sdk.Configurations.Abstract;
using Newtonsoft.Json;
using UnityEngine;

namespace flameborn.Sdk.Configurations
{
    [Serializable]
    public class PlayfabConfiguration : IConfiguration
    {
        [field: SerializeField]
        [JsonProperty("TitleId")]
        public string TitleId { get; set; } = string.Empty;
    }
}