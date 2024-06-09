using System;
using System.IO;
using HF.Logger;
using Newtonsoft.Json;
using UnityEngine;

namespace Flameborn.Configurations
{
    public class PlayFabConfigurationController : ConfigurationController<PlayFabConfiguration>, IConfigurationController<PlayFabConfiguration>
    {
        public PlayFabConfigurationController(IConfiguration configuration) : base(configuration)
        {
        }

        public override bool LoadConfiguration(out string errorLog)
        {
            base.LoadConfiguration(out errorLog);

            string json = File.ReadAllText(Configuration.ConfigurationFilePath);
            PlayFabConfiguration fabConfiguration = JsonConvert.DeserializeObject<PlayFabConfiguration>(json);
            Configuration = fabConfiguration;

            if (String.IsNullOrEmpty(fabConfiguration.TitleId))
            {
                errorLog = $"Title Id is not found, Please check configuration file.";
                return false;
            }

            errorLog = string.Empty;
            return true;

        }

        public override bool SaveConfiguration(out string errorLog, PlayFabConfiguration config)
        {
            base.SaveConfiguration(out errorLog, config);

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