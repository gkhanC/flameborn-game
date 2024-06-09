using System;
using System.IO;
using HF.Extensions;
using HF.Logger;
using UnityEngine;
using UnityEngine.Events;

namespace Flameborn.Configurations
{
    public class ConfigurationManager : MonoBehaviour, IConfigurationManager
    {
        private bool isLoaded;
        public static ConfigurationManager Instance { get; private set; }

        public PlayFabConfigurationController playFabConfigurationController { get; private set; }

        private UnityEvent<PlayFabConfiguration> OnConfigurationLoad { get; set; } = new UnityEvent<PlayFabConfiguration>();

        public void SubscribeOnConfigurationLoad(UnityAction<PlayFabConfiguration> onConfigurationLoad)
        {
            OnConfigurationLoad.AddListener(onConfigurationLoad);
            if (isLoaded)
            {
                var conf = playFabConfigurationController.Configuration as PlayFabConfiguration;
                onConfigurationLoad.Invoke(conf);
            }
        }

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

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
                    HFLogger.LogSuccess(playFabConfigurationController, $"PlayFab Configurations loaded.");
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