using System;
using System.IO;
using UnityEngine;

namespace Flameborn.Configurations
{
    public abstract class ConfigurationController<T> : IConfigurationController<T> where T : class, IConfiguration
    {
        private IConfiguration _configuration;
        public IConfiguration Configuration { get => _configuration; protected set => _configuration = value; }

        public ConfigurationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private bool CheckConfigurationFilePath(out string errorLog)
        {
            if (String.IsNullOrEmpty(Configuration.ConfigurationFilePath))
            {
                errorLog = $"{typeof(T).Name} is not found, Please check configuration file where {Application.streamingAssetsPath}.";
                return false;
            }

            errorLog = String.Empty;
            return true;
        }

        private bool CheckConfigurationFile(out string errorLog)
        {
            if (!File.Exists(Configuration.ConfigurationFilePath))
            {
                errorLog = $"File is not found {Configuration.ConfigurationFilePath}";
                return false;
            }

            errorLog = String.Empty;
            return true;
        }

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