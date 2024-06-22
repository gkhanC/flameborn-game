using System;
using flameborn.Sdk.Configurations.Abstract;
using Newtonsoft.Json;
using UnityEngine;

namespace flameborn.Sdk.Configurations
{
    /// <summary>
    /// Represents the Playfab configuration settings.
    /// </summary>
    [Serializable]
    public class PlayfabConfiguration : IConfiguration
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Title ID for Playfab.
        /// </summary>
        [field: SerializeField]
        [JsonProperty("TitleId")]
        public string TitleId { get; set; } = string.Empty;

        #endregion
    }
}
