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
    /// <summary>
    /// Manager for handling configurations.
    /// </summary>
    [Serializable]
    public class ConfigurationManager : MonoBehaviourSingleton<ConfigurationManager>, IConfigurationManager, IConfigurationLoader
    {
        #region Methods

        /// <summary>
        /// Called on the first frame the script is active.
        /// </summary>
        private void Start()
        {
            GameManager.Instance.SetManager(this);
        }

        /// <summary>
        /// Loads the configuration of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of configuration to load.</typeparam>
        /// <param name="processListeners">The listeners to process the configuration.</param>
        public void LoadConfiguration<T>(params Action<T>[] processListeners) where T : IConfiguration
        {
            var json = Resources.Load<TextAsset>(typeof(T).Name)?.ToString();

            if (string.IsNullOrEmpty(json)) return;

            var configuration = JsonConvert.DeserializeObject<T>(json);

            processListeners?.ForEach(a => a.Invoke(configuration));
            HFLogger.Log(this, $"{nameof(LoadConfiguration)} configuration load.");
        }

        #endregion
    }
}
