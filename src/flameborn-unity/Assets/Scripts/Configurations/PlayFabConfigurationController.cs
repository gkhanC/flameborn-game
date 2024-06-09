using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Flameborn.Configurations
{
    /// <summary>
    /// Controller for managing PlayFab configuration.
    /// </summary>
    public class PlayFabConfigurationController : ConfigurationController<PlayFabConfiguration>, IConfigurationController<PlayFabConfiguration>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayFabConfigurationController"/> class with the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration instance.</param>
        public PlayFabConfigurationController(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Loads the PlayFab configuration.
        /// </summary>
        /// <param name="errorLog">Outputs an error log if the load fails.</param>
        /// <returns>True if the configuration was loaded successfully, otherwise false.</returns>
        public override bool LoadConfiguration(out string errorLog)
        {
            if (!base.LoadConfiguration(out errorLog))
            {
                return false;
            }

            string json = File.ReadAllText(Configuration.ConfigurationFilePath);
            PlayFabConfiguration fabConfiguration = JsonConvert.DeserializeObject<PlayFabConfiguration>(json);
            Configuration = fabConfiguration;

            if (String.IsNullOrEmpty(fabConfiguration.TitleId))
            {
                errorLog = "Title Id is not found, please check the configuration file.";
                return false;
            }

            errorLog = string.Empty;
            return true;
        }

        /// <summary>
        /// Saves the PlayFab configuration.
        /// </summary>
        /// <param name="errorLog">Outputs an error log if the save fails.</param>
        /// <param name="config">The configuration to save.</param>
        /// <returns>True if the configuration was saved successfully, otherwise false.</returns>
        public override bool SaveConfiguration(out string errorLog, PlayFabConfiguration config)
        {
            errorLog = String.Empty;
            var isCompleted = false;

            try
            {
                string json = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "PlayFabConfiguration.json"), json);
                isCompleted = true;
            }
            catch (Exception ex)
            {
                errorLog = ex.Message;
            }

            return isCompleted;
        }
    }
}
