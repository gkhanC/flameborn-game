namespace Flameborn.PlayFab
{
    using System;
    using Flameborn.Azure;
    using Flameborn.Configurations;
    using Flameborn.Managers;
    using global::PlayFab;
    using global::PlayFab.ClientModels;
    using HF.Extensions;
    using HF.Logger;
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    /// Manages PlayFab-related operations and handles login functionality.
    /// </summary>
    public class PlayFabManager : MonoBehaviour
    {
        /// <summary>
        /// Indicates whether the user is logged in.
        /// </summary>
        private bool isLogin;

        /// <summary>
        /// PlayFab configuration instance.
        /// </summary>
        private PlayFabConfiguration config;

        /// <summary>
        /// Action to perform on successful login.
        /// </summary>
        private Action<LoginResult> onLoginSuccess;

        /// <summary>
        /// Action to perform on login failure.
        /// </summary>
        private Action<PlayFabError> onLoginFailure;

        /// <summary>
        /// Singleton instance of PlayFabManager.
        /// </summary>
        public static PlayFabManager Instance { get; private set; }

        /// <summary>
        /// Event triggered on successful login.
        /// </summary>
        private UnityEvent LoginSuccess { get; set; } = new UnityEvent();

        /// <summary>
        /// Subscribes to the LoginSuccess event.
        /// </summary>
        /// <param name="onLogin">The action to perform when login is successful.</param>
        public void SubscribeLoginSuccessEvent(UnityAction onLogin)
        {
            LoginSuccess.AddListener(onLogin);
            if (isLogin) onLogin.Invoke();
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
        private void Start()
        {
            onLoginFailure = new Action<PlayFabError>(this.OnLoginFailure);
            onLoginSuccess = new Action<LoginResult>(this.OnLoginSuccess);

            if (ConfigurationManager.Instance.IsNull() || ConfigurationManager.Instance.gameObject.IsNull())
            {
                HFLogger.LogError(ConfigurationManager.Instance, "Configuration Manager instance is null.");
                Application.Quit();
                return;
            }

            if (AzureManager.Instance.IsNull() || AzureManager.Instance.gameObject.IsNull())
            {
                HFLogger.LogError(ConfigurationManager.Instance, "Azure Manager instance is null.");
                Application.Quit();
                return;
            }

            AzureManager.Instance.SubscribeOnUserDataLoadCompleted(OnUserDataLoadCompleted);
            ConfigurationManager.Instance.SubscribeOnConfigurationLoadPlayFabEvent(OnConfigurationLoaded);
        }

        /// <summary>
        /// Checks and sets the PlayFab Title ID.
        /// </summary>
        /// <param name="titleId">The PlayFab Title ID.</param>
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
        private void Login(PlayFabConfiguration configuration)
        {
            var loginObj = new PlayFabLogin(new PlayFabLoginData(true, SystemInfo.deviceUniqueIdentifier, ref onLoginSuccess, ref onLoginFailure));

            if (loginObj.Login(out string logMessage, config))
            {
                HFLogger.Log(loginObj, "Login process completed.");
            }
            else
            {
                HFLogger.Log(loginObj, "Login process did not complete.");
            }
        }

        public void OnUserDataLoadCompleted()
        {
            Login(config);
        }

        /// <summary>
        /// Called when the configuration is loaded.
        /// </summary>
        /// <param name="configuration">The PlayFab configuration.</param>
        public void OnConfigurationLoaded(PlayFabConfiguration configuration)
        {
            this.config = configuration;
            CheckPlayFabTitleId(configuration.TitleId);
        }

        /// <summary>
        /// Called when login fails.
        /// </summary>
        /// <param name="playFabError">The PlayFab error that occurred.</param>
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
        private void OnLoginSuccess(LoginResult loginResult)
        {
            HFLogger.LogSuccess(loginResult, "Device login: " + loginResult.PlayFabId ) ;

            if (!UserManager.Instance.currentUserData.IsRegistered)
            {
                UserManager.Instance.currentUserData.EMail = string.Empty;
                UserManager.Instance.currentUserData.UserName = "#" + loginResult.PlayFabId.Substring(0, 4);
                UserManager.Instance.currentUserData.DeviceId = SystemInfo.deviceUniqueIdentifier;
            }

            isLogin = true;
            LoginSuccess.Invoke();
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
