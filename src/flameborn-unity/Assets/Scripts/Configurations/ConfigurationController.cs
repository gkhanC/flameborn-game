using System;
using System.IO;
using UnityEngine;

namespace Flameborn.Configurations
{
    /// <summary>
    /// Abstract base class for managing configuration controllers.
    /// </summary>
    public abstract class ConfigurationController<T> : IConfigurationController<T> where T : class, IConfiguration
    {
        /// <summary>
        /// The configuration instance.
        /// </summary>
        private IConfiguration _configuration;

        /// <summary>
        /// Gets or sets the configuration instance.
        /// </summary>
        public IConfiguration Configuration 
        { 
            get => _configuration; 
            protected set => _configuration = value; 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationController{T}"/> class with the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration instance.</param>
        public ConfigurationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Checks if the configuration file path is valid.
        /// </summary>
        /// <param name="errorLog">Outputs an error log if the check fails.</param>
        /// <returns>True if the configuration file path is valid, otherwise false.</returns>
        private bool CheckConfigurationFilePath(out string errorLog)
        {
            if (String.IsNullOrEmpty(Configuration.ConfigurationFilePath))
            {
                errorLog = $"{typeof(T).Name} is not found. Please check the configuration file at {Application.streamingAssetsPath}.";
                return false;
            }

            errorLog = String.Empty;
            return true;
        }

        /// <summary>
        /// Checks if the configuration file exists.
        /// </summary>
        /// <param name="errorLog">Outputs an error log if the check fails.</param>
        /// <returns>True if the configuration file exists, otherwise false.</returns>
        private bool CheckConfigurationFile(out string errorLog)
        {
            if (!File.Exists(Configuration.ConfigurationFilePath))
            {
                errorLog = $"File not found at {Configuration.ConfigurationFilePath}.";
                return false;
            }

            errorLog = String.Empty;
            return true;
        }

        /// <summary>
        /// Loads the configuration.
        /// </summary>
        /// <param name="errorLog">Outputs an error log if the load fails.</param>
        /// <returns>True if the configuration was loaded successfully, otherwise false.</returns>
        public virtual bool LoadConfiguration(out string errorLog)
        {
            if (!CheckConfigurationFilePath(out errorLog))
            {
                return false;
            }

            if (!CheckConfigurationFile(out errorLog))
            {
                return false;
            }

            errorLog = String.Empty;
            return true;
        }

        /// <summary>
        /// Saves the configuration.
        /// </summary>
        /// <param name="errorLog">Outputs an error log if the save fails.</param>
        /// <param name="configuration">The configuration to save.</param>
        /// <returns>True if the configuration was saved successfully, otherwise false.</returns>
        public virtual bool SaveConfiguration(out string errorLog, T configuration)
        {
            if (!CheckConfigurationFilePath(out errorLog))
            {
                return false;
            }

            errorLog = String.Empty;
            return true;
        }
    }
}
