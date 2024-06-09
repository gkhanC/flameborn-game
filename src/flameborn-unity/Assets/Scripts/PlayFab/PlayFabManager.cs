namespace Flameborn.PlayFab
{
    using System;
    using Flameborn.Configurations;
    using global::PlayFab;
    using global::PlayFab.ClientModels;
    using HF.Extensions;
    using HF.Logger;
    using UnityEngine;
    using UnityEngine.Events;

    public class PlayFabManager : MonoBehaviour
    {
        private bool isLogin;
        private PlayFabConfiguration config;
        Action<LoginResult> onLoginSuccess;
        Action<PlayFabError> onLoginFailure;
        public static PlayFabManager Instance { get; private set; }
        private UnityEvent LoginSuccess { get; set; } = new UnityEvent();

        public void SubscribeLoginSuccessEvent(UnityAction onLogin)
        {
            LoginSuccess.AddListener(onLogin);
            if (isLogin) onLogin.Invoke();
        }

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        private void Start()
        {
            onLoginFailure = new Action<PlayFabError>(this.OnLoginFailure);
            onLoginSuccess = new Action<LoginResult>(this.OnLoginSuccess);
            ConfigurationManager.Instance.SubscribeOnConfigurationLoad(OnConfigurationLoaded);
        }

        private void CheckPlayFabTitleId(string titleId)
        {
            if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
            {
                PlayFabSettings.TitleId = titleId;
            }
        }

        private void Login(PlayFabConfiguration configuration)
        {
            var loginObj = new PlayFabLogin(new PlayFabLoginData(true, SystemInfo.deviceUniqueIdentifier, ref onLoginSuccess, ref onLoginFailure));

            if (loginObj.Login(out string logMessage))
            {
                HFLogger.Log(loginObj, $"Login process completed.");
            }
            else
            {
                HFLogger.Log(loginObj, $"Login process does not complete.");
            }
        }

        public void OnConfigurationLoaded(PlayFabConfiguration configuration)
        {
            this.config = configuration;
            CheckPlayFabTitleId(configuration.TitleId);
            Login(configuration);

        }

        private void OnLoginFailure(PlayFabError playFabError)
        {
            HFLogger.LogError(playFabError, playFabError.ErrorMessage);
            HFLogger.LogError(this, "Exiting the application ");
            Application.Quit();
        }

        private void OnLoginSuccess(LoginResult loginResult)
        {
            HFLogger.LogSuccess(loginResult, "Device login: " + loginResult.PlayFabId);

            if (!config.UserData.IsRegistered)
            {
                config.UserData.EMail = string.Empty;
                config.UserData.UserName = "#" + loginResult.PlayFabId.Substring(0, 4);
                config.UserData.DeviceId = SystemInfo.deviceUniqueIdentifier;
                config.UserData.LaunchCount += 1;

                if (!ConfigurationManager.Instance.playFabConfigurationController.SaveConfiguration(out var errorLog, config))
                {
                    HFLogger.LogError(this, " Configuration file update error => " + errorLog);
                }
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