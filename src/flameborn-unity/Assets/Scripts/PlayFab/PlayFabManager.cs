namespace Flameborn.PlayFab
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Flameborn.Azure;
    using Flameborn.Configurations;
    using Flameborn.Managers;
    using Flameborn.User;
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
        private string playFabId;
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

        public bool IsLogin => isLogin;

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
            onLoginFailure = new Action<PlayFabError>(this.OnError);
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

        public PlayerStatistics GetPlayerStatistics()
        {
            return new PlayerStatistics();
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
        private void OnError(PlayFabError playFabError)
        {
            HFLogger.LogError(playFabError, playFabError.ErrorMessage);
            HFLogger.LogError(this, "Exiting the application.");
            UIManager.Instance.AlertController.Show("PlayFab Error", playFabError.ErrorMessage, true);
        }

        /// <summary>
        /// Called when login succeeds.
        /// </summary>
        /// <param name="loginResult">The result of the login operation.</param>
        private void OnLoginSuccess(LoginResult loginResult)
        {
            isLogin = true;
            playFabId = loginResult.PlayFabId;
            HFLogger.LogSuccess(loginResult, "login: ", loginResult.PlayFabId, loginResult.NewlyCreated);


            if (!UserManager.Instance.currentUserData.IsRegistered)
            {
                UserManager.Instance.currentUserData.Email = string.Empty;
                UserManager.Instance.currentUserData.UserName = "#" + loginResult.PlayFabId[..5];
                UserManager.Instance.currentUserData.DeviceId = SystemInfo.deviceUniqueIdentifier;
                UserManager.Instance.currentUserData.Rating = 0;
                UserManager.Instance.currentUserData.LaunchCount = 1;

            }
            else
            {
                UserManager.Instance.UpdateLaunchCount();
            }

            GetLeaderBoardLeaderOnStart();
        }

        private void GetLeaderBoardLeaderOnStart()
        {
            GetLeaderboard(OnGetLeaderboardLeaderOnStart, LeaderboardErrorOnStart, 0, 1);
        }

        private void GetLeaderBoardOnStart()
        {
            GetPlayerLeaderboard(OnGetLeaderboardOnStart, LeaderboardErrorOnStart, 4);
        }

        private void OnGetLeaderboardLeaderOnStart(GetLeaderboardResult result)
        {
            var entity = result.Leaderboard.FirstOrDefault<PlayerLeaderboardEntry>();
            UIManager.Instance.LeaderboardController.SetLeader(entity);
            HFLogger.LogSuccess(result, $"Leaderboard leader saved: [{entity.DisplayName}]: {entity.StatValue}");
            GetLeaderBoardOnStart();
        }

        private void OnGetLeaderboardOnStart(GetLeaderboardAroundPlayerResult result)
        {
            UIManager.Instance.LeaderboardController.SetLeaderboard(result.Leaderboard);
            HFLogger.LogSuccess(result, $"Player leader board saved.");


            var playerData = result.Leaderboard.Where(x => x.PlayFabId == playFabId).FirstOrDefault();
            UserManager.Instance.currentUserData.Rank = playerData.Position;

            if (UserManager.Instance.currentUserData.Rating > playerData.StatValue)
            {
                PostUpdatePlayerStatistics(OnUpdatePlayerStatisticsCompletedOnStart, LeaderboardErrorOnStart, UserManager.Instance.currentUserData.Rating);
                return;
            }

            if (UserManager.Instance.currentUserData.IsRegistered && UserManager.Instance.currentUserData.IsPasswordCorrect)
            {
                if (UserManager.Instance.currentUserData.Rating < playerData.StatValue)
                {
                    var email = UserManager.Instance.currentUserData.Email;
                    var password = UserManager.Instance.currentUserData.Password;
                    UserManager.Instance.SetRating(playerData.StatValue);
                    var rating = UserManager.Instance.currentUserData.Rating;
                    AzureManager.Instance.UpdateRatingRequest(out string errorLog, email, password, rating, OnAzureRatingUpdateCompleted);
                    return;
                }
            }

            OnLeaderboardLoadCompleted();

        }

        public void OnAzureRatingUpdateCompleted(UpdateRatingResponse response)
        {
            OnLeaderboardLoadCompleted();
        }

        public void OnLeaderboardLoadCompleted()
        {
            LoginSuccess.Invoke();
        }

        private void OnUpdatePlayerStatisticsCompletedOnStart(UpdatePlayerStatisticsResult result)
        {
            HFLogger.LogSuccess(result, "User rating updated on Play Fab");
            OnLeaderboardLoadCompleted();
        }

        private void LeaderboardErrorOnStart(PlayFabError result)
        {
            LoginSuccess.Invoke();
        }

        public void GetLeaderboard(Action<GetLeaderboardResult> listener, Action<PlayFabError> errorListener, int indexStart, int resultCount)
        {
            var request = new GetLeaderboardRequest
            {
                StatisticName = "Rank",
                StartPosition = indexStart,
                MaxResultsCount = resultCount
            };

            PlayFabClientAPI.GetLeaderboard(request, listener, errorListener);
        }

        public void GetPlayerLeaderboard(Action<GetLeaderboardAroundPlayerResult> listener, Action<PlayFabError> errorListener, int resultCount)
        {
            var request = new GetLeaderboardAroundPlayerRequest
            {
                StatisticName = "Rank",
                PlayFabId = playFabId,
                MaxResultsCount = resultCount

            };

            PlayFabClientAPI.GetLeaderboardAroundPlayer(request, listener, errorListener);
        }

        public void PostUpdatePlayerStatistics(Action<UpdatePlayerStatisticsResult> listener, Action<PlayFabError> errorListener, int statisticValue, string statisticName = "Rank")
        {
            var request = new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>
                {
                    new StatisticUpdate{
                        StatisticName = statisticName,
                        Value = statisticValue
                    }
                }
            };

            PlayFabClientAPI.UpdatePlayerStatistics(request, listener, errorListener);
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
