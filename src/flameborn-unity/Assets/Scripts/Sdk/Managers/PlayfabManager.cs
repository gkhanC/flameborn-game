using System;
using System.Collections.Generic;
using System.Linq;
using flameborn.Core.Accounts;
using flameborn.Core.Managers;
using flameborn.Core.Managers.Abstract;
using flameborn.Sdk.Configurations;
using HF.Library.Utilities.Singleton;
using HF.Logger;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.Events;

namespace flameborn.Sdk.Managers
{
    public class PlayfabManager : MonoBehaviourSingleton<PlayfabManager>, IManager
    {
        PlayfabConfiguration configuration = null;
        private UnityEvent<LoginResult> event_LoginCompleted;
        public bool IsLoaded { get; private set; } = false;

        private void Start()
        {
            GameManager.Instance.SetManager(this);
            FindConfiguration();
        }

        private void FindConfiguration()
        {
            var configurationManager = GameManager.Instance.GetManager<ConfigurationManager>();
            if (configurationManager.IsContain)
            {
                configurationManager.Instance.LoadConfiguration<PlayfabConfiguration>(this.SetConfigurationFile);

                return;
            }

            Invoke(nameof(this.FindConfiguration), 2f);
        }

        private void SetConfigurationFile(PlayfabConfiguration configuration)
        {
            this.configuration = configuration;
            IsLoaded = configuration != null;

            if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
            {
                PlayFabSettings.TitleId = configuration.TitleId;
            }
        }

        public void UnlinkAndroidDeviceId(string deviceId, Action<UnlinkAndroidDeviceIDResult> listener)
        {
#if UNITY_ANDROID
            var request = new UnlinkAndroidDeviceIDRequest
            {
                AndroidDeviceId = deviceId
            };
            PlayFabClientAPI.UnlinkAndroidDeviceID(request, listener, Event_OnError);
#endif
        }
        public void UnlinkIOSDeviceId(string deviceId, Action<UnlinkIOSDeviceIDResult> listener)
        {

#if UNITY_IOS
            var request = new UnlinkIOSDeviceIDRequest
            {
                AndroidDeviceId = deviceId
            };
            PlayFabClientAPI.UnlinkIOSDeviceId(request, listener, Event_OnError);
#endif
        }

        private void Event_OnError(PlayFabError error)
        {
            HFLogger.LogError(error, error.ApiEndpoint, error.GenerateErrorReport());
            var uiManager = GameManager.Instance.GetManager<UiManager>();
            if (uiManager.IsContain)
            {
                uiManager.Instance.alert.Show("Error", error.ErrorMessage);
            }
        }
    }
}