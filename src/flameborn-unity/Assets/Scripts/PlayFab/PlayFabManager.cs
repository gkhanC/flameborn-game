namespace Flameborn.PlayFab
{
    using System;
    using Flameborn.Configurations;
    using global::PlayFab;
    using global::PlayFab.ClientModels;
    using HF.Extensions;
    using HF.Logger;
    using MADD;
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    /// Manages PlayFab-related operations and handles login functionality.
    /// </summary>
    [Docs("Manages PlayFab-related operations and handles login functionality.")]
    public class PlayFabManager : MonoBehaviour
    {
        /// <summary>
        /// Indicates whether the user is logged in.
        /// </summary>
        [Docs("Indicates whether the user is logged in.")]
        private bool isLogin;

        /// <summary>
        /// PlayFab configuration instance.
        /// </summary>
        [Docs("PlayFab configuration instance.")]
        private PlayFabConfiguration config;

        /// <summary>
        /// Action to perform on successful login.
        /// </summary>
        [Docs("Action to perform on successful login.")]
        private Action<LoginResult> onLoginSuccess;

        /// <summary>
        /// Action to perform on login failure.
        /// </summary>
        [Docs("Action to perform on login failure.")]
        private Action<PlayFabError> onLoginFailure;

        /// <summary>
        /// Singleton instance of PlayFabManager.
        /// </summary>
        [Docs("Singleton instance of PlayFabManager.")]
        public static PlayFabManager Instance { get; private set; }

        /// <summary>
        /// Event triggered on successful login.
        /// </summary>
        [Docs("Event triggered on successful login.")]
        private UnityEvent LoginSuccess { get; set; } = new UnityEvent();

        /// <summary>
        /// Subscribes to the LoginSuccess event.
        /// </summary>
        /// <param name="onLogin">The action to perform when login is successful.</param>
        [Docs("Subscribes to the LoginSuccess event.")]
        public void SubscribeLoginSuccessEvent(UnityAction onLogin)
        {
            LoginSuccess.AddListener(onLogin);
            if (isLogin) onLogin.Invoke();
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
        private void Start()
        {
            onLoginFailure = new Action<PlayFabError>(this.OnLoginFailure);
            onLoginSuccess = new Action<LoginResult>(this.OnLoginSuccess);
            ConfigurationManager.Instance.SubscribeOnConfigurationLoad(OnConfigurationLoaded);
        }

        /// <summary>
        /// Checks and sets the PlayFab Title ID.
        /// </summary>
        /// <param name="titleId">The PlayFab Title ID.</param>
        [Docs("Checks and sets the PlayFab Title ID.")]
        private void CheckPlayFabTitleId(string titleId)
        {
            if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
            {
                PlayFabSettings.TitleId = titleId;
            }
        }

        /// <summary>
        /// Logs the user in using PlayFab.
        /// </summary>
        /// <param name="configuration">The PlayFab configuration.</param>
        [Docs("Logs the user in using PlayFab.")]
        private void Login(PlayFabConfiguration configuration)
        {
            var loginObj = new PlayFabLogin(new PlayFabLoginData(true, SystemInfo.deviceUniqueIdentifier, ref onLoginSuccess, ref onLoginFailure));

            if (loginObj.Login(out string logMessage))
            {
                HFLogger.Log(loginObj, "Login process completed.");
            }
            else
            {
                HFLogger.Log(loginObj, "Login process did not complete.");
            }
        }

        /// <summary>
        /// Called when the configuration is loaded.
        /// </summary>
        /// <param name="configuration">The PlayFab configuration.</param>
        [Docs("Called when the configuration is loaded.")]
        public void OnConfigurationLoaded(PlayFabConfiguration configuration)
        {
            this.config = configuration;
            CheckPlayFabTitleId(configuration.TitleId);
            Login(configuration);
        }

        /// <summary>
        /// Called when login fails.
        /// </summary>
        /// <param name="playFabError">The PlayFab error that occurred.</param>
        [Docs("Called when login fails.")]
        private void OnLoginFailure(PlayFabError playFabError)
        {
            HFLogger.LogError(playFabError, playFabError.ErrorMessage);
            HFLogger.LogError(this, "Exiting the application.");
            Application.Quit();
        }

        /// <summary>
        /// Called when login succeeds.
        /// </summary>
        /// <param name="loginResult">The result of the login operation.</param>
        [Docs("Called when login succeeds.")]
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
                    HFLogger.LogError(this, "Configuration file update error => " + errorLog);
                }
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
