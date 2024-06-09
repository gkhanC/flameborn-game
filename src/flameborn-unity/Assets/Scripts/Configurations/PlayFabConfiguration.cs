using System;
using Flameborn.User;
using MADD;

namespace Flameborn.Configurations
{
    /// <summary>
    /// Represents the configuration specific to PlayFab.
    /// </summary>
    [Docs("Represents the configuration specific to PlayFab.")]
    public class PlayFabConfiguration : Configuration, IConfiguration
    {
        /// <summary>
        /// Gets or sets the Title ID for PlayFab.
        /// </summary>
        [Docs("Gets or sets the Title ID for PlayFab.")]
        public string TitleId { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the user data.
        /// </summary>
        [Docs("Gets or sets the user data.")]
        public UserData UserData { get; set; } = new UserData();

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayFabConfiguration"/> class with the specified configuration path.
        /// </summary>
        /// <param name="configurationPath">The path to the configuration file.</param>
        [Docs("Initializes a new instance of the PlayFabConfiguration class with the specified configuration path.")]
        public PlayFabConfiguration(string configurationPath) : base(configurationPath)
        {
        }
    }
}
