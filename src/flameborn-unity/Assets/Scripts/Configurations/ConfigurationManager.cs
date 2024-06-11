using System;
using System.IO;
using System.Threading.Tasks;
using Flameborn.Managers;
using HF.Extensions;
using HF.Logger;
using UnityEngine;
using UnityEngine.Events;

namespace Flameborn.Configurations
{
    /// <summary>
    /// Manages the configuration for the application.
    /// </summary>
    public class ConfigurationManager : MonoBehaviour, IConfigurationManager
    {
        private bool isAzureConfLoaded, isPlayFabConfLoaded;

        /// <summary>
        /// Indicates if the configuration is loaded.
        /// </summary>
        public bool isLoaded => isAzureConfLoaded && isPlayFabConfLoaded;

        /// <summary>
        /// Singleton instance of the ConfigurationManager.
        /// </summary>
        public static ConfigurationManager Instance { get; private set; }

        /// <summary>
        /// Controller for PlayFab configuration.
        /// </summary>
        public PlayFabConfigurationController playFabConfigurationController { get; private set; }
        public AzureConfigurationController azureConfigurationController { get; private set; }

        /// <summary>
        /// Event triggered when the configuration is loaded.
        /// </summary>
        private UnityEvent<PlayFabConfiguration> OnConfigurationLoadPlayFabEvent { get; set; } = new UnityEvent<PlayFabConfiguration>();
        private UnityEvent<AzureConfiguration> OnConfigurationLoadAzureEvent { get; set; } = new UnityEvent<AzureConfiguration>();

        /// <summary>
        /// Subscribes to the OnConfigurationLoad event.
        /// </summary>
        /// <param name="onConfigurationLoad">The action to perform when the configuration is loaded.</param>
        public void SubscribeOnConfigurationLoadPlayFabEvent(UnityAction<PlayFabConfiguration> onConfigurationLoad)
        {
            OnConfigurationLoadPlayFabEvent.AddListener(onConfigurationLoad);
            if (isPlayFabConfLoaded)
            {
                var conf = playFabConfigurationController.Configuration as PlayFabConfiguration;
                onConfigurationLoad.Invoke(conf);
            }
        }

        public void SubscribeOnConfigurationLoadAzureEvent(UnityAction<AzureConfiguration> onConfigurationLoad)
        {
            OnConfigurationLoadAzureEvent.AddListener(onConfigurationLoad);
            if (isAzureConfLoaded)
            {
                var conf = azureConfigurationController.Configuration as AzureConfiguration;
                onConfigurationLoad.Invoke(conf);
            }
        }

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        /// <summary>
        /// Called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// </summary>
        public void Start()
        {
            playFabConfigurationController = new PlayFabConfigurationController(new PlayFabConfiguration(Path.Combine(Application.streamingAssetsPath, "PlayFabConfiguration.json")));
            azureConfigurationController = new AzureConfigurationController(new AzureConfiguration(Path.Combine(Application.streamingAssetsPath, "AzureConfiguration.json")));

            if (GameManager.Instance.IsNull() || GameManager.Instance.gameObject.IsNull())
            {
                HFLogger.LogError(GameManager.Instance, "Game Manager instance is null.");
                Application.Quit();
                return;
            }

            GameManager.Instance.SubscribeOnGameRunningEvent(this.OnGameRunningEvent);
        }

        public void OnGameRunningEvent(bool isRunning)
        {
            if (isRunning)
            {
                LoadConf();
            }
        }

        private async void LoadConf()
        {
            try
            {
                // Load Azure configuration asynchronously
                string azureErrorLog = await Task.Run(() =>
                {
                    if (!azureConfigurationController.LoadConfiguration(out var errorLog))
                    {
                        UIManager.Instance.AlertController.ShowCriticalError("error");
                        return errorLog;
                    }
                    return null;
                });

                // Process Azure configuration result on the main thread
                if (!string.IsNullOrEmpty(azureErrorLog))
                {
                    HFLogger.LogError(azureConfigurationController, azureErrorLog);
                    UIManager.Instance.AlertController.ShowCriticalError("error");
                }
                else
                {
                    HFLogger.LogSuccess(azureConfigurationController, "Azure Configurations loaded.");
                    isAzureConfLoaded = true;
                    var confA = azureConfigurationController.Configuration as AzureConfiguration;

                    OnConfigurationLoadAzureEvent?.Invoke(confA);
                }

                // Load PlayFab configuration asynchronously
                string playFabErrorLog = await Task.Run(() =>
                {
                    if (!playFabConfigurationController.LoadConfiguration(out var errorLog))
                    {
                        UIManager.Instance.AlertController.ShowCriticalError("error");
                        return errorLog;
                    }
                    return null;
                });

                // Process PlayFab configuration result on the main thread
                if (!string.IsNullOrEmpty(playFabErrorLog))
                {
                    UIManager.Instance.AlertController.ShowCriticalError("error");
                    HFLogger.LogError(playFabConfigurationController, playFabErrorLog);
                }
                else
                {
                    HFLogger.LogSuccess(playFabConfigurationController, "PlayFab Configurations loaded.");
                    isPlayFabConfLoaded = true;
                    var confP = playFabConfigurationController.Configuration as PlayFabConfiguration;
                    OnConfigurationLoadPlayFabEvent?.Invoke(confP);
                }
            }
            catch (Exception ex)
            {
                UIManager.Instance.AlertController.ShowCriticalError("error");
                HFLogger.LogError(ex, ex.Message);
            }
        }



        /// <summary>
        /// Called when the object becomes enabled and active.
        /// </summary>
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
