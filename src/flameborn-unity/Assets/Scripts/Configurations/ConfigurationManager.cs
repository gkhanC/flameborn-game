using System;
using System.IO;
using HF.Extensions;
using HF.Logger;
using MADD;
using UnityEngine;
using UnityEngine.Events;

namespace Flameborn.Configurations
{
    /// <summary>
    /// Manages the configuration for the application.
    /// </summary>
    [Docs("Manages the configuration for the application.")]
    public class ConfigurationManager : MonoBehaviour, IConfigurationManager
    {
        /// <summary>
        /// Indicates if the configuration is loaded.
        /// </summary>
        [Docs("Indicates if the configuration is loaded.")]
        private bool isLoaded;

        /// <summary>
        /// Singleton instance of the ConfigurationManager.
        /// </summary>
        [Docs("Singleton instance of the ConfigurationManager.")]
        public static ConfigurationManager Instance { get; private set; }

        /// <summary>
        /// Controller for PlayFab configuration.
        /// </summary>
        [Docs("Controller for PlayFab configuration.")]
        public PlayFabConfigurationController playFabConfigurationController { get; private set; }

        /// <summary>
        /// Event triggered when the configuration is loaded.
        /// </summary>
        [Docs("Event triggered when the configuration is loaded.")]
        private UnityEvent<PlayFabConfiguration> OnConfigurationLoad { get; set; } = new UnityEvent<PlayFabConfiguration>();

        /// <summary>
        /// Subscribes to the OnConfigurationLoad event.
        /// </summary>
        /// <param name="onConfigurationLoad">The action to perform when the configuration is loaded.</param>
        [Docs("Subscribes to the OnConfigurationLoad event.")]
        public void SubscribeOnConfigurationLoad(UnityAction<PlayFabConfiguration> onConfigurationLoad)
        {
            OnConfigurationLoad.AddListener(onConfigurationLoad);
            if (isLoaded)
            {
                var conf = playFabConfigurationController.Configuration as PlayFabConfiguration;
                onConfigurationLoad.Invoke(conf);
            }
        }

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        [Docs("Called when the script instance is being loaded.")]
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        /// <summary>
        /// Called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// </summary>
        [Docs("Called on the frame when a script is enabled just before any of the Update methods are called the first time.")]
        public void Start()
        {
            try
            {
                playFabConfigurationController = new PlayFabConfigurationController(new PlayFabConfiguration(Path.Combine(Application.streamingAssetsPath, "PlayFabConfiguration.json")));

                if (!playFabConfigurationController.LoadConfiguration(out var errorLog))
                {
                    HFLogger.LogError(playFabConfigurationController, errorLog);
                }
                else
                {
                    HFLogger.LogSuccess(playFabConfigurationController, "PlayFab Configurations loaded.");
                    isLoaded = true;
                    var conf = playFabConfigurationController.Configuration as PlayFabConfiguration;
                    OnConfigurationLoad.Invoke(conf);
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        /// <summary>
        /// Called when the object becomes enabled and active.
        /// </summary>
        [Docs("Called when the object becomes enabled and active.")]
        private void OnEnable()
        {
            if (Instance.IsNull() || Instance.gameObject.IsNull())
            {
                Instance = this;
            }
            else
            {
                if (!Instance.gameObject.Equals(gameObject))
                {
                    DestroyImmediate(gameObject);
                }
                else if (!Instance.Equals(this))
                {
                    DestroyImmediate(this);
                }
            }
        }
    }
}
