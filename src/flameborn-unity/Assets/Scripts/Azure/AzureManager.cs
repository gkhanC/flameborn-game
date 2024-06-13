using Flameborn.Device;
using Flameborn.Configurations;
using Flameborn.Managers;
using HF.Extensions;
using HF.Logger;
using UnityEngine;
using UnityEngine.Events;
using Flameborn.PlayFab;
using System;
using System.Collections.Generic;

namespace Flameborn.Azure
{
    public class AzureManager : MonoBehaviour, IAzureManager
    {
        public static AzureManager Instance { get; private set; }

        public bool isAnyTaskRunning { get; private set; } = false;
        private AzureConfiguration config;

        private AddDeviceDataRequestController addDeviceDataRequestController;
        private GetLaunchCountController getLaunchCountController;
        private GetRatingController getRatingController;
        private UpdateDeviceDataController updateDeviceDataController;
        private RecoveryUserPasswordController recoveryUserPasswordController;
        private UpdateLaunchCountController updateLaunchCountController;
        private UpdateRatingController updateRatingController;
        private ValidateDeviceIdController validateDeviceIdController;
        private ValidateUserEmailController validateUserEmailController;
        private ValidateLoginController validatePasswordController;

        private UnityEvent OnUserDataLoadCompleted { get; set; } = new UnityEvent();
        private UnityEvent OnUserDataAddCompleted { get; set; } = new UnityEvent();

        /// <summary>
        /// Adds device data request with specified parameters.
        /// </summary>
        /// <param name="errorLog">Output parameter for error logging.</param>
        /// <param name="email">Email address of the user.</param>
        /// <param name="userName">Username of the user.</param>
        /// <param name="password">Password of the user.</param>
        /// <param name="launchCount">Launch count of the device.</param>
        /// <param name="rating">Rating of the device.</param>
        public void AddDeviceDataRequest(out string errorLog, string email, string userName, string password, int launchCount, int rating, params Action<AddDeviceDataResponse>[] responseListener)
        {
            errorLog = "";
            if (isAnyTaskRunning)
            {
                errorLog = "Task is busy.";
                return;
            }

            isAnyTaskRunning = true;

            var listeners = new List<Action<AddDeviceDataResponse>>(responseListener)
            {
                OnAddDeviceDataCompleted
            };

            addDeviceDataRequestController = new AddDeviceDataRequestController(config.AddDeviceDataFunctionConnection, listeners.ToArray());
            _ = addDeviceDataRequestController.PostRequestAddDeviceData(email, userName, password, launchCount, rating);
        }

        /// <summary>
        /// Called when add device data response is completed.
        /// </summary>
        /// <param name="response">The response object.</param>
        private void OnAddDeviceDataCompleted(AddDeviceDataResponse response)
        {
            isAnyTaskRunning = false;
            HFLogger.Log(response, response.Message);

            if (response.Success)
            {
                ValidateLogin(UIManager.Instance.ProfileController.UILoginController.OnLoginCompleted);
                return;
            }

            UIManager.Instance.AlertController.Show("Register Failed", response.Message);
            OnUserDataAddCompleted.Invoke();
        }

        public void RecoveryUserPassword(out string errorLog, string email, params Action<RecoveryUserPasswordResponse>[] responseListener)
        {
            errorLog = "";
            if (isAnyTaskRunning)
            {
                errorLog = "Task is busy.";
                return;
            }

            var listeners = new List<Action<RecoveryUserPasswordResponse>>(responseListener) { OnRecoveryUserPasswordCompleted };

            isAnyTaskRunning = true;
            recoveryUserPasswordController = new RecoveryUserPasswordController(config.UpdateUserPasswordFunctionConnection, listeners.ToArray());
            _ = recoveryUserPasswordController.PostRequestRecoveryUserPassword(email);
        }

        private void OnRecoveryUserPasswordCompleted(RecoveryUserPasswordResponse response)
        {
            isAnyTaskRunning = false;
            HFLogger.Log(response, "User password recovered.");
        }

        public void UpdateLaunchCountRequest(out string errorLog, string email, string password, int newLaunchCount, params Action<UpdateLaunchCountResponse>[] responseListener)
        {
            errorLog = "";
            if (isAnyTaskRunning)
            {
                errorLog = "Task is busy.";
                return;
            }

            var listeners = new List<Action<UpdateLaunchCountResponse>>(responseListener) { OnUpdateLaunchCountCompleted };


            isAnyTaskRunning = true;
            updateLaunchCountController = new UpdateLaunchCountController(config.UpdateLaunchCountFunctionConnection, listeners.ToArray());
            _ = updateLaunchCountController.PostRequestUpdateLaunchCount(email, password, newLaunchCount, true);
        }

        private void OnUpdateLaunchCountCompleted(UpdateLaunchCountResponse response)
        {
            isAnyTaskRunning = false;
            HFLogger.Log(response, response.Message);
        }

        public void UpdateRatingRequest(out string errorLog, string email, string password, int newRating, params Action<UpdateRatingResponse>[] responseListener)
        {
            errorLog = "";
            if (isAnyTaskRunning)
            {
                errorLog = "Task is busy.";
                return;
            }

            var listeners = new List<Action<UpdateRatingResponse>>(responseListener) { OnUpdateRatingCompleted };

            isAnyTaskRunning = true;
            updateRatingController = new UpdateRatingController(config.UpdateRatingFunctionConnection, listeners.ToArray());
            _ = updateRatingController.PostRequestUpdateRating(email, password, newRating, true);
        }

        private void OnUpdateRatingCompleted(UpdateRatingResponse response)
        {
            isAnyTaskRunning = false;
            HFLogger.Log(response, response.Message);
        }

        /// <summary>
        /// Validates the user date for login.
        /// </summary>
        public void ValidateLogin(params Action<ValidateLoginResponse>[] responseListeners)
        {
            if (isAnyTaskRunning) return;

            var listeners = new List<Action<ValidateLoginResponse>>(responseListeners)
            {
                OnValidateLoginCompleted
            };

            validatePasswordController = new ValidateLoginController(config.ValidateUserPassword, listeners.ToArray());
            _ = validatePasswordController.PostRequestValidateUserPassword(UserManager.Instance.currentUserData.Email, UserManager.Instance.currentUserData.Password);
            isAnyTaskRunning = true;
        }

        /// <summary>
        /// Called when validate login response is completed.
        /// </summary>
        /// <param name="response">The response object.</param>
        private void OnValidateLoginCompleted(ValidateLoginResponse response)
        {
            isAnyTaskRunning = false;
            HFLogger.LogWarning(response, response.Message, response.UserName);
        }

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else if (Instance != this)
            {
                DestroyImmediate(gameObject);
            }
        }

        /// <summary>
        /// Called when the script is started.
        /// </summary>
        private void Start()
        {
            if (ConfigurationManager.Instance.IsNull() || ConfigurationManager.Instance.gameObject.IsNull())
            {
                HFLogger.LogError(ConfigurationManager.Instance, "Configuration Manager instance is null.");
                isAnyTaskRunning = true;
                Application.Quit();
                return;
            }

            ConfigurationManager.Instance.SubscribeOnConfigurationLoadAzureEvent(OnConfigurationLoadedEvent);
        }

        /// <summary>
        /// Finds user data and initializes necessary processes.
        /// </summary>
        public void FindUserData()
        {
            if (isAnyTaskRunning) return;

            if (PlayerPrefs.HasKey("UserEmail"))
            {
                ProcessUserData();
            }
            else
            {
                HandleNoUserEmail();
            }
        }

        /// <summary>
        /// Processes user data based on available information.
        /// </summary>
        private void ProcessUserData()
        {
            UserManager.Instance.SetIsContainEmail(true);

            var currentUserData = UserManager.Instance.currentUserData;

            if (string.IsNullOrEmpty(currentUserData.Email))
            {
                isAnyTaskRunning = true;
                ValidateUserEmail();
                return;
            }

            if (currentUserData.IsRegistered)
            {
                if (PlayerPrefs.HasKey("UserPassword") && string.IsNullOrEmpty(currentUserData.Password))
                {
                    isAnyTaskRunning = true;
                    ValidateUserPassword();
                    return;
                }
            }

            OnUserDataLoadCompleted.Invoke();
        }

        /// <summary>
        /// Handles the case where no user email is found.
        /// </summary>
        private void HandleNoUserEmail()
        {
            HFLogger.LogWarning(this, "User email not found.");
            var currentUserData = UserManager.Instance.currentUserData;

            if (string.IsNullOrEmpty(currentUserData.DeviceId))
            {
                validateDeviceIdController = new ValidateDeviceIdController(config.ValidateDeviceIdFunctionConnection, OnValidateDeviceIdCompleted);
                _ = validateDeviceIdController.PostRequestValidateDeviceId();
                isAnyTaskRunning = true;
                return;
            }

            OnUserDataLoadCompleted.Invoke();
        }

        /// <summary>
        /// Called when device ID response is completed.
        /// </summary>
        /// <param name="response">The response object.</param>
        private void OnValidateDeviceIdCompleted(ValidateDeviceIdResponse response)
        {
            isAnyTaskRunning = false;
            UserManager.Instance.SetDeviceId(new DeviceDataFactory().Create().deviceData.DeviceId);
            UserManager.Instance.SetIsDeviceRegistered(response.Success);
            UserManager.Instance.SetIsContainEmail(false);
            HFLogger.Log(response, response.Message);
            FindUserData();
        }

        /// <summary>
        /// Checks the user email.
        /// </summary>
        private void ValidateUserEmail()
        {
            var email = PlayerPrefs.GetString("UserEmail");
            validateUserEmailController = new ValidateUserEmailController(config.ValidateUserEmailFunctionConnection, OnValidateUserEmailCompleted);
            _ = validateUserEmailController.PostRequestValidateUserEmail(email);
        }

        /// <summary>
        /// Called when validate user email response is completed.
        /// </summary>
        /// <param name="response">The response object.</param>
        private void OnValidateUserEmailCompleted(ValidateUserEmailResponse response)
        {
            isAnyTaskRunning = false;
            var email = PlayerPrefs.GetString("UserEmail");
            UserManager.Instance.SetDeviceId(new DeviceDataFactory().Create().deviceData.DeviceId);
            UserManager.Instance.SetEmail(email);
            UserManager.Instance.SetIsRegistered(response.Email);
            UserManager.Instance.SetIsDeviceRegistered(response.Device);
            HFLogger.Log(response, response.Message);

            if (!response.Email || !response.Device)
            {
                UIManager.Instance.AlertController.Show("Your Account Lost", response.Message);
            }

            FindUserData();
        }

        /// <summary>
        /// Validates the user password.
        /// </summary>
        private void ValidateUserPassword()
        {
            var password = PlayerPrefs.GetString("UserPassword");
            validatePasswordController = new ValidateLoginController(config.ValidateUserPassword, OnValidateUserPasswordCompleted);
            _ = validatePasswordController.PostRequestValidateUserPassword(UserManager.Instance.currentUserData.Email, password, true);
        }

        /// <summary>
        /// Called when device data response is completed.
        /// </summary>
        /// <param name="response">The response object.</param>
        private void OnValidateUserPasswordCompleted(ValidateLoginResponse response)
        {
            isAnyTaskRunning = false;
            if (response.Success)
            {
                UserManager.Instance.SetPassword(response.Message, true);
                UserManager.Instance.SetUserName(response.UserName);
                UserManager.Instance.SetLaunchCount(response.LaunchCount);
                UserManager.Instance.SetRating(response.Rating);
                HFLogger.LogValidate(response, $"User legged in. {response.UserName}");
            }
            else
            {
                UserManager.Instance.SetPassword("-1");
                HFLogger.LogError(response, response.Message);
            }

            FindUserData();
        }

        /// <summary>
        /// Gets the launch count.
        /// </summary>
        public void GetLaunchCount()
        {
            var email = UserManager.Instance.currentUserData.Email;
            var password = PlayerPrefs.GetString("UserPassword");
            getLaunchCountController = new GetLaunchCountController(config.GetLaunchCountFunctionConnection, OnGetLaunchCount);
            _ = getLaunchCountController.PostRequestGetLaunchCount(email, password, true);
        }

        /// <summary>
        /// Gets the rating.
        /// </summary>
        public void GetRating()
        {
            var email = UserManager.Instance.currentUserData.Email;
            var password = PlayerPrefs.GetString("UserPassword");
            getRatingController = new GetRatingController(config.GetRatingFunctionConnection, OnGetRatingCompleted);
            _ = getRatingController.PostRequestGetRating(email, password, true);
        }

        /// <summary>
        /// Called when configuration is loaded.
        /// </summary>
        /// <param name="configuration">The Azure configuration object.</param>
        private void OnConfigurationLoadedEvent(AzureConfiguration configuration)
        {
            this.config = configuration;
            HFLogger.Log(configuration, "Azure configurations saved.");
            FindUserData();
        }

        /// <summary>
        /// Called when launch count response is completed.
        /// </summary>
        /// <param name="response">The response object.</param>
        private void OnGetLaunchCount(GetLaunchCountResponse response)
        {
            isAnyTaskRunning = false;
            UserManager.Instance.SetLaunchCount(response.LaunchCount);
            HFLogger.Log(response, response.Message);
        }

        /// <summary>
        /// Called when rating response is completed.
        /// </summary>
        /// <param name="response">The response object.</param>
        private void OnGetRatingCompleted(GetRatingResponse response)
        {
            isAnyTaskRunning = false;
            UserManager.Instance.SetRating(response.Rating);
            HFLogger.Log(response, response.Message);
        }

        public void SubscribeOnUserDataLoadCompleted(UnityAction onUserDataLoadCompleted)
        {
            OnUserDataLoadCompleted.AddListener(onUserDataLoadCompleted);
        }

        public void SubscribeOnUserDataAddCompleted(UnityAction onUserDataAddCompleted)
        {
            OnUserDataAddCompleted.AddListener(onUserDataAddCompleted);
        }

    }
}
