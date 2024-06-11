using System;
using Flameborn.User;

namespace Flameborn.Configurations
{
    /// <summary>
    /// Represents the configuration specific to PlayFab.
    /// </summary>
    public class PlayFabConfiguration : Configuration, IConfiguration
    {
        /// <summary>
        /// Gets or sets the Title ID for PlayFab.
        /// </summary>
        public string TitleId { get; set; } = String.Empty;       

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayFabConfiguration"/> class with the specified configuration path.
        /// </summary>
        /// <param name="configurationPath">The path to the configuration file.</param>
        public PlayFabConfiguration(string configurationPath) : base(configurationPath)
        {
        }
    }
}
