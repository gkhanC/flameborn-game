using System;
using flameborn.Core.Managers;
using flameborn.Sdk.Configurations.Abstract;
using flameborn.Sdk.Managers.Abstract;
using HF.Library.Utilities.Singleton;
using HF.Logger;
using Newtonsoft.Json;
using Sirenix.Utilities;
using UnityEngine;

namespace flameborn.Sdk.Managers
{
    [Serializable]
    public class ConfigurationManager : MonoBehaviourSingleton<ConfigurationManager>, IConfigurationManager, IConfigurationLoader
    {
        private void Start()
        {
            GameManager.Instance.SetManager(this);
        }

        public void LoadConfiguration<T>(params Action<T>[] processListeners) where T : IConfiguration
        {
            var json = Resources.Load<TextAsset>(typeof(T).Name)?.ToString();

            if (string.IsNullOrEmpty(json)) return;

            var configuration = JsonConvert.DeserializeObject<T>(json);

            processListeners?.ForEach(a => a.Invoke(configuration));
            HFLogger.Log(this, $"{nameof(this.LoadConfiguration)} configuration load.");
        }

    }
}