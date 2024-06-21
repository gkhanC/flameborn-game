using HF.Library.Utilities.Singleton;
using HF.Logger;
using PlayFab;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using flameborn.Core.Accounts;
using flameborn.Core.User;
using flameborn.Sdk.Controllers.Data;
using flameborn.Sdk.Controllers.Login;
using flameborn.Sdk.Managers.Abstract;
using flameborn.Sdk.Managers;
using flameborn.Sdk.Requests.Data.Abstract;
using flameborn.Sdk.Requests.Data;
using flameborn.Sdk.Requests.Login.Abstract;
using flameborn.Sdk.Requests.Login;

namespace flameborn.Core.Managers
{
    public class AccountManager : MonoBehaviourSingleton<AccountManager>, IAccountManager
    {
        private Account account = new Account();
        public Account Account { get => account; set => account = value; }
        public UnityEvent Event_OnAccountLoad { get; set; } = new UnityEvent();
        public UnityEvent<Account> Event_OnAccountDataOnChanged { get; set; } = new UnityEvent<Account>();
        public UnityEvent<UserData> Event_OnUserDataOnChanged { get; set; } = new UnityEvent<UserData>();

        private PlayfabManager playfabManager;
        private UiManager uiManager;
        private bool isRegisterProcess;
        private bool isStartProcess;

        public AccountManager() { }

        #region Public

        public void Login()
        {
            var request = new LoginRequest(new EmailLoginController_Playfab(account.Email, account.Password, PlayFabSettings.TitleId, true));
            request.SendRequest(out string errorLog, OnGetLoginResponse_EventListener);
            if (errorLog.Length > 0)
            {
                HFLogger.LogError(errorLog);
            }
        }

        public void PasswordResetRequest()
        {
            var request = new PasswordResetRequest(new PasswordResetController_Playfab(account.Email));
            request.SendRequest(out string errorLog, OnGetPasswordResetResponse_EventListener);
            if (errorLog.Length > 0)
            {
                HFLogger.LogError(errorLog);
            }
        }

        public void Register()
        {
            isRegisterProcess = true;
            var request = new RegisterRequest(new RegisterController_Playfab(account.Email, account.Password, account.UserData.UserName));
            request.SendRequest(out string errorLog, OnGetRegisterResponse_EventListener);
            if (errorLog.Length > 0)
            {
                HFLogger.LogError(errorLog);
            }
        }

        public void UpdateStatistics()
        {
            var request = new UpdateStatisticsRequest(new UpdateStatisticsController_Playfab(
                                new (string name, int value)[]
                                {

                        (nameof(account.UserData.Rank),account.UserData.Rank),
                        (nameof(account.UserData.Rating),account.UserData.Rating),
                        (nameof(account.UserData.LaunchCount),account.UserData.LaunchCount)

                                }
                            ));

            request.SendRequest(out string errorLog, OnGetUpdateStatisticsResponse_EventListener);
            if (errorLog.Length > 0)
            {
                HFLogger.LogError(errorLog);
            }
        }

        public void UpdateAccountInfo_AzureTable()
        {
            //AddNewAccount cloud scripti ekler veya güncelleştirir.
            var request = new UpdateStatisticsRequest(new UpdateAccountInfoOnAzureController_Playfab(account, "AddNewAccount"));
            request.SendRequest(out string errorLog, OnGetUpdateAccountResponseAzure_EventListener);
            if (errorLog.Length > 0)
            {
                HFLogger.LogError(errorLog);
            }
        }

        #endregion

        #region Login
        private void LoginOnStart()
        {
            if (account.IsAccountLoaded)
            {
                Login();
                return;
            }

            var request = new LoginRequest(new DeviceLoginController_Playfab(account.DeviceId, PlayFabSettings.TitleId));
            request.SendRequest(out string errorLog, OnGetLoginResponse_EventListener);
            if (errorLog.Length > 0)
            {
                HFLogger.LogError(errorLog);
            }
        }
        #endregion

        #region Data Loaders

        private void LoadAccountInfo()
        {
            var request = new GetAccountInfoRequest(new GetAccountInfoController_Playfab(account.Email, account.Password, "GetAccountInfo"));
            request.SendRequest(out string errorLog, OnGetAccountInfoResponse_EventListener);
            if (errorLog.Length > 0)
            {
                HFLogger.LogError(errorLog);
            }
        }

        private void LoadPlayerStatistics()
        {
            var request = new GetStatisticsRequest(new GetPlayerStatisticsController_Playfab("Rank", "Rating", "LaunchCount"));
            request.SendRequest(out string errorLog, OnGetStatisticsResponse_EventListener);
            if (errorLog.Length > 0)
            {
                HFLogger.LogError(errorLog);
                uiManager.alert.Show("Alert", errorLog, Application.Quit);
            }
        }

        #endregion

        #region Event Listeners

        private void OnGetLoginResponse_EventListener(ILoginResponse response)
        {
            account.IsAccountLoaded = response.IsAccountLogged;
            account.PlayfabId = response.PlayFabId;

            var ui = GameManager.Instance.GetManager<UiManager>();
            if (ui.IsContain)
            {
                uiManager = ui.Instance;
            }

            if (response.IsRequestSuccess && account.IsAccountLoaded)
            {
                account.IsAccountLoggedIn = true;
                SaveAccountCredentials(account.Email, account.Password);
                LoadAccountInfo();
                return;
            }

            account.UserData.UserName = "#" + SystemInfo.deviceUniqueIdentifier[..4];

            if (response.IsRequestSuccess)
                LoadPlayerStatistics();

            if (!response.IsRequestSuccess)
            {
                HFLogger.LogError(response, response.Message);
                uiManager.alert.Show("Alert", response.Message, Application.Quit);
                return;
            }

            HFLogger.Log(response, response.Message);
        }

        private void OnGetAccountInfoResponse_EventListener(IAccountInfoResponse response)
        {
            account.UserData.IsLogin = response.IsRequestSuccess;
            if (response.IsRequestSuccess)
            {
                account.UserData.UserName = response.UserName;
                account.UserData.Rating = response.Rating;
                account.UserData.LaunchCount = response.LaunchCount;
            }
            LoadPlayerStatistics();

            if (!response.IsRequestSuccess)
            {
                HFLogger.LogError(response, response.Message);
                uiManager.alert.Show("Alert", response.Message, Application.Quit);
                return;
            }

            HFLogger.Log(response, response.Message);
        }

        private void OnGetStatisticsResponse_EventListener(IGetStatisticsResponse response)
        {
            bool isHasChanged = false;
            if (response.IsRequestSuccess)
            {
                ApplyStatistics(out isHasChanged, response.Statistics);
            }

            if (isHasChanged || isStartProcess || isRegisterProcess)
            {
                if (isStartProcess)
                {
                    account.UserData.LaunchCount++;
                }

                UpdateStatistics();

                if (account.IsAccountLoaded)
                {
                    UpdateAccountInfo_AzureTable();
                }
            }

            if (!response.IsRequestSuccess)
            {
                HFLogger.LogError(response, response.Message);
                return;
            }

            Event_OnAccountDataOnChanged.Invoke(account);

            HFLogger.Log(response, response.Message);
        }

        private void OnGetUpdateStatisticsResponse_EventListener(IUpdateStatisticsResponse response)
        {
            if (isStartProcess)
            {
                isStartProcess = false;
                GameManager.Instance.LoadMainMenuScene();
            }
            Event_OnUserDataOnChanged.Invoke(account.UserData);

            if (!response.IsRequestSuccess)
            {
                HFLogger.LogError(response, response.Message);
                return;
            }

            HFLogger.Log(response, response.Message);
        }

        private void OnGetUpdateAccountResponseAzure_EventListener(IUpdateStatisticsResponse response)
        {
            account.IsAccountLoaded = true;
            account.IsAccountLoggedIn = true;
            account.UserData.IsLogin = true;

            if (isRegisterProcess) isRegisterProcess = false;
            Event_OnAccountDataOnChanged.Invoke(account);

            if (!response.IsRequestSuccess)
            {
                HFLogger.LogError(response, response.Message);
                return;
            }

            HFLogger.Log(response, response.Message);
        }

        private void OnGetRegisterResponse_EventListener(IRegisterResponse response)
        {
            if (response.IsRequestSuccess) account.IsAccountLoaded = true;

            HFLogger.LogError(response, response.Message);
            uiManager.alert.Show("Alert", response.Message);

            LoadPlayerStatistics();
        }

        private void OnGetPasswordResetResponse_EventListener(IPasswordResetResponse response)
        {
            uiManager.alert.Show("Alert", response.Message);
        }

        private void ApplyStatistics(out bool isHasChanged, Dictionary<string, int> statistics)
        {
            isHasChanged = false;

            if (statistics.ContainsKey(nameof(account.UserData.Rank)))
            {
                if (account.UserData.Rank != statistics[nameof(account.UserData.Rank)])
                {
                    isHasChanged = true;
                    account.UserData.Rank = statistics[nameof(account.UserData.Rank)] > account.UserData.Rank ?
                    statistics[nameof(account.UserData.Rank)] : account.UserData.Rank;
                }
            }

            if (statistics.ContainsKey(nameof(account.UserData.Rating)))
            {
                if (account.UserData.Rating != statistics[nameof(account.UserData.Rating)])
                {
                    isHasChanged = true;
                    account.UserData.Rating = statistics[nameof(account.UserData.Rating)] > account.UserData.Rating ?
                    statistics[nameof(account.UserData.Rating)] : account.UserData.Rating;
                }
            }

            if (statistics.ContainsKey(nameof(account.UserData.LaunchCount)))
            {
                if (account.UserData.LaunchCount != statistics[nameof(account.UserData.LaunchCount)])
                {
                    isHasChanged = true;
                    account.UserData.LaunchCount = statistics[nameof(account.UserData.LaunchCount)] > account.UserData.LaunchCount ?
                    statistics[nameof(account.UserData.LaunchCount)] : account.UserData.LaunchCount;
                }
            }

        }

        #endregion

        #region  Initializers
        private void Start()
        {
            isStartProcess = true;
            GameManager.Instance.SetManager(this);
            FindAccountData();
        }
        private void FindAccountData()
        {
            var credentials = LoadAccountCredentials();
            account.DeviceId = SystemInfo.deviceUniqueIdentifier;
            if (credentials.isContain)
            {
                account.Email = credentials.email;
                account.Password = credentials.password;
                account.UserData = new UserData();
                account.IsAccountLoaded = true;
            }
            else
            {
                account.IsAccountLoaded = false;
            }

            LoginOnStart();
        }

        private (bool isContain, string email, string password) LoadAccountCredentials()
        {
            if (PlayerPrefs.HasKey("userEmail") && PlayerPrefs.HasKey("userPassword"))
            {
                string encryptedEmail = PlayerPrefs.GetString("userEmail");
                string encryptedPassword = PlayerPrefs.GetString("userPassword");

                string email = EncryptionUtility.Decrypt(encryptedEmail);
                string password = EncryptionUtility.Decrypt(encryptedPassword);

                return (true, email, password);
            }

            return (false, null, null);
        }

        private void SaveAccountCredentials(string email, string password)
        {
            string encryptedEmail = EncryptionUtility.Encrypt(email);
            string encryptedPassword = EncryptionUtility.Encrypt(password);

            PlayerPrefs.SetString("userEmail", encryptedEmail);
            PlayerPrefs.SetString("userPassword", encryptedPassword);
            PlayerPrefs.Save();
        }

        #endregion
    }
}
