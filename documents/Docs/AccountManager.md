
# AccountManager Class Documentation

## Overview
The `AccountManager` class manages account-related operations such as login, registration, and statistics updates. This class is a singleton derived from `MonoBehaviourSingleton` and implements the `IAccountManager` interface.

## Class Definition

```csharp
using HF.Library.Utilities.Singleton;
using HF.Logger;
using PlayFab;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using flameborn.Core.Accounts;
using flameborn.Core.User;
using flameborn.Sdk.Controllers.Data;
using flameborn.Sdk.Controllers.Login;
using flameborn.Sdk.Managers.Abstract;
using flameborn.Sdk.Requests.Data.Abstract;
using flameborn.Sdk.Requests.Data;
using flameborn.Sdk.Requests.Login.Abstract;
using flameborn.Sdk.Requests.Login;

namespace flameborn.Core.Managers
{
    /// <summary>
    /// Manages account-related operations including login, registration, and statistics updates.
    /// </summary>
    public class AccountManager : MonoBehaviourSingleton<AccountManager>, IAccountManager
    {
        #region Fields

        private Account account = new Account();
        private UiManager uiManager;
        private bool isRegisterProcess;
        private bool isStartProcess;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current account.
        /// </summary>
        public Account Account { get => account; set => account = value; }

        /// <summary>
        /// Event triggered when the account is loaded.
        /// </summary>
        public UnityEvent Event_OnAccountLoad { get; set; } = new UnityEvent();

        /// <summary>
        /// Event triggered when the account data is changed.
        /// </summary>
        public UnityEvent<Account> Event_OnAccountDataOnChanged { get; set; } = new UnityEvent<Account>();

        /// <summary>
        /// Event triggered when the user data is changed.
        /// </summary>
        public UnityEvent<UserData> Event_OnUserDataOnChanged { get; set; } = new UnityEvent<UserData>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountManager"/> class.
        /// </summary>
        public AccountManager() { }

        #endregion

        #region Public Methods

        /// <summary>
        /// Logs in the user.
        /// </summary>
        public void Login()
        {
            var request = new LoginRequest(new EmailLoginController_Playfab(account.Email, account.Password, PlayFabSettings.TitleId, true));
            request.SendRequest(out string errorLog, OnGetLoginResponse_EventListener);
            if (errorLog.Length > 0)
            {
                HFLogger.LogError(errorLog);
            }
        }

        /// <summary>
        /// Sends a password reset request.
        /// </summary>
        public void PasswordResetRequest()
        {
            var request = new PasswordResetRequest(new PasswordResetController_Playfab(account.Email));
            request.SendRequest(out string errorLog, OnGetPasswordResetResponse_EventListener);
            if (errorLog.Length > 0)
            {
                HFLogger.LogError(errorLog);
            }
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
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

        /// <summary>
        /// Updates user statistics.
        /// </summary>
        public void UpdateStatistics()
        {
            var request = new UpdateStatisticsRequest(new UpdateStatisticsController_Playfab(
                                new (string name, int value)[] {
                                    (nameof(account.UserData.Rank), account.UserData.Rank),
                                    (nameof(account.UserData.Rating), account.UserData.Rating),
                                    (nameof(account.UserData.LaunchCount), account.UserData.LaunchCount)
                                }
                            ));

            request.SendRequest(out string errorLog, OnGetUpdateStatisticsResponse_EventListener);
            if (errorLog.Length > 0)
            {
                HFLogger.LogError(errorLog);
            }
        }

        /// <summary>
        /// Updates account information in Azure Table.
        /// </summary>
        public void UpdateAccountInfo_AzureTable()
        {
            var request = new UpdateStatisticsRequest(new UpdateAccountInfoOnAzureController_Playfab(account, "AddNewAccount"));
            request.SendRequest(out string errorLog, OnGetUpdateAccountResponseAzure_EventListener);
            if (errorLog.Length > 0)
            {
                HFLogger.LogError(errorLog);
            }
        }

        #endregion

        #region Private Methods

        private void ApplyStatistics(out bool isHasChanged, Dictionary<string, int> statistics)
        {
            isHasChanged = false;

            if (statistics.ContainsKey(nameof(account.UserData.Rank)) &&
                account.UserData.Rank != statistics[nameof(account.UserData.Rank)])
            {
                isHasChanged = true;
                account.UserData.Rank = statistics[nameof(account.UserData.Rank)] > account.UserData.Rank ? 
                    statistics[nameof(account.UserData.Rank)] : account.UserData.Rank;
            }

            if (statistics.ContainsKey(nameof(account.UserData.Rating)) &&
                account.UserData.Rating != statistics[nameof(account.UserData.Rating)])
            {
                isHasChanged = true;
                account.UserData.Rating = statistics[nameof(account.UserData.Rating)] > account.UserData.Rating ? 
                    statistics[nameof(account.UserData.Rating)] : account.UserData.Rating;
            }

            if (statistics.ContainsKey(nameof(account.UserData.LaunchCount)) &&
                account.UserData.LaunchCount != statistics[nameof(account.UserData.LaunchCount)])
            {
                isHasChanged = true;
                account.UserData.LaunchCount = statistics[nameof(account.UserData.LaunchCount)] > account.UserData.LaunchCount ? 
                    statistics[nameof(account.UserData.LaunchCount)] : account.UserData.LaunchCount;
            }
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

        #region Event Handlers

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
            SaveAccountCredentials(account.Email, account.Password);
            LoadPlayerStatistics();
            Login();
        }

        private void OnGetPasswordResetResponse_EventListener(IPasswordResetResponse response)
        {
            uiManager.alert.Show("Alert", response.Message);
        }

        #endregion

        #region Initializers

        /// <summary>
        /// Called on the first frame the script is active.
        /// </summary>
        private void Start()
        {
            isStartProcess = true;
            GameManager.Instance.SetManager(this);
            FindAccountData();
        }

        #endregion
    }
}
```

## Fields
- **account**: An instance of the `Account` class representing the current account.
- **uiManager**: An instance of the `UiManager` class.
- **isRegisterProcess**: A boolean indicating if the registration process is ongoing.
- **isStartProcess**: A boolean indicating if the start process is ongoing.

## Properties
- **Account**: Gets or sets the current account.
- **Event_OnAccountLoad**: Event triggered when the account is loaded.
- **Event_OnAccountDataOnChanged**: Event triggered when the account data is changed.
- **Event_OnUserDataOnChanged**: Event triggered when the user data is changed.

## Methods
### Public Methods
- **Login()**: Logs in the user.
- **PasswordResetRequest()**: Sends a password reset request.
- **Register()**: Registers a new user.
- **UpdateStatistics()**: Updates user statistics.
- **UpdateAccountInfo_AzureTable()**: Updates account information in Azure Table.

### Private Methods
- **ApplyStatistics(out bool isHasChanged, Dictionary<string, int> statistics)**: Applies the retrieved statistics to the account.
- **FindAccountData()**: Finds account data.
- **LoadAccountCredentials()**: Loads account credentials from PlayerPrefs.
- **SaveAccountCredentials(string email, string password)**: Saves account credentials to PlayerPrefs.
- **LoginOnStart()**: Logs in the user on start.
- **LoadAccountInfo()**: Loads account information.
- **LoadPlayerStatistics()**: Loads player statistics.

### Event Handlers
- **OnGetLoginResponse_EventListener(ILoginResponse response)**: Handles the login response event.
- **OnGetAccountInfoResponse_EventListener(IAccountInfoResponse response)**: Handles the account information response event.
- **OnGetStatisticsResponse_EventListener(IGetStatisticsResponse response)**: Handles the statistics response event.
- **OnGetUpdateStatisticsResponse_EventListener(IUpdateStatisticsResponse response)**: Handles the update statistics response event.
- **OnGetUpdateAccountResponseAzure_EventListener(IUpdateStatisticsResponse response)**: Handles the update account response for Azure event.
- **OnGetRegisterResponse_EventListener(IRegisterResponse response)**: Handles the register response event.
- **OnGetPasswordResetResponse_EventListener(IPasswordResetResponse response)**: Handles the password reset response event.

### Initializers
- **Start()**: Called on the first frame the script is active.

## Usage Example
Below is an example of how to use the `AccountManager` class in a Unity project.

```csharp
using UnityEngine;

public class ExampleUsage : MonoBehaviour
{
    private void Start()
    {
        AccountManager.Instance.Login();
    }
}
```

## See Also
For more information on the `IAccountManager` interface, refer to the [IAccountManager documentation](https://gkhanc.github.io/flameborn-game/IAccountManager).

## File Location
This class is defined in the `AccountManager.cs` file, located in the `flameborn.Core.Managers` namespace.
