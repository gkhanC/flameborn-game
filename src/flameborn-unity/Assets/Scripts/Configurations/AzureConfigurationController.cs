using System;
using System.IO;
using HF.Logger;
using Newtonsoft.Json;
using UnityEngine;

namespace Flameborn.Configurations
{
    public class AzureConfigurationController : ConfigurationController<AzureConfiguration>, IConfigurationController<AzureConfiguration>
    {
        public AzureConfigurationController(IConfiguration configuration) : base(configuration)
        {
        }

        public override bool LoadConfiguration(out string errorLog)
        {
            if (!base.LoadConfiguration(out errorLog))
            {
                HFLogger.LogError(this, errorLog);
                return false;
            }

            string json = File.ReadAllText(Configuration.ConfigurationFilePath);
            AzureConfiguration azureConfiguration = JsonConvert.DeserializeObject<AzureConfiguration>(json);
            Configuration = azureConfiguration;

            if (String.IsNullOrEmpty(azureConfiguration.GetRatingFunctionConnection) ||
            String.IsNullOrEmpty(azureConfiguration.AddDeviceDataFunctionConnection) ||
            String.IsNullOrEmpty(azureConfiguration.ValidateUserPassword) ||
            String.IsNullOrEmpty(azureConfiguration.ValidateUserEmailFunctionConnection) ||
            String.IsNullOrEmpty(azureConfiguration.ValidateDeviceIdFunctionConnection) ||
            String.IsNullOrEmpty(azureConfiguration.GetLaunchCountFunctionConnection) ||
            String.IsNullOrEmpty(azureConfiguration.UpdateRatingFunctionConnection) ||
            String.IsNullOrEmpty(azureConfiguration.UpdateDeviceDataFunctionConnection) ||
            String.IsNullOrEmpty(azureConfiguration.UpdateLaunchCountFunctionConnection))
            {
                errorLog = "Connection string is not found, please check the configuration file.";
                return false;
            }

            errorLog = string.Empty;
            return true;
        }

        public override bool SaveConfiguration(out string errorLog, AzureConfiguration config)
        {
            errorLog = String.Empty;
            var isCompleted = false;

            try
            {
                string json = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "AzureConfiguration.json"), json);
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