using Flameborn.Device;
using Flameborn.Configurations;
using Flameborn.Managers;
using HF.Extensions;
using HF.Logger;
using UnityEngine;
using UnityEngine.Events;
using Flameborn.PlayFab;

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
        private ValidateUserPasswordController validatePasswordController;

        private UnityEvent OnUserDataLoadCompleted { get; set; } = new UnityEvent();
        private UnityEvent OnUserDataAddCompleted { get; set; } = new UnityEvent();

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

        private UnityEvent<bool> loginValidationEvent;

        /// <summary>
        /// Validates the user date for login.
        /// </summary>
        public void ValidateLogin(UnityAction<bool> validationEventListener)
        {
            if (isAnyTaskRunning) return;

            loginValidationEvent = new UnityEvent<bool>();
            loginValidationEvent.AddListener(validationEventListener);
            validatePasswordController = new ValidateUserPasswordController(config.ValidateUserPassword, OnValidateLoginCompleted);
            _ = validatePasswordController.PostRequestValidateUserPassword(UserManager.Instance.currentUserData.EMail, UserManager.Instance.currentUserData.Password);
            isAnyTaskRunning = true;
        }

        /// <summary>
        /// Called when validate login response is completed.
        /// </summary>
        /// <param name="response">The response object.</param>
        private void OnValidateLoginCompleted(ValidateUserPasswordResponse response)
        {
            isAnyTaskRunning = false;
            if (response.Success)
            {
                UserManager.Instance.SetPassword(response.Message, true);
                UserManager.Instance.SetUserName(response.UserName);
                UserManager.Instance.SetLaunchCount(response.LaunchCount);
                UserManager.Instance.SetRating(response.Rating);
                UserManager.Instance.SetIsRegistered(true);
                var pass = response.Message[..6];
                UserManager.Instance.SetPassword(pass, true);
                PlayerPrefs.SetString("UserEmail", UserManager.Instance.currentUserData.EMail);
                PlayerPrefs.SetString("UserPassword", pass);
                PlayFabManager.Instance.OnUserDataLoadCompleted();
                UIManager.Instance.AlertController.Show("SUCCESS", "You are logged in.");
                HFLogger.LogValidate(response, $"User is logged in. {response.UserName}");
            }
            else
            {
                UserManager.Instance.SetPassword("-1");
                HFLogger.LogWarning(response, response.Message);
                UIManager.Instance.AlertController.Show("ERROR", response.Message);
            }

            loginValidationEvent.Invoke(response.Success);
        }

        /// <summary>
        /// Finds user data and initializes necessary processes.
        /// </summary>
        private void FindUserData()
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

            if (string.IsNullOrEmpty(currentUserData.EMail))
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

        private UnityEvent<bool> onAddDeviceDataCompleted = new UnityEvent<bool>();

        /// <summary>
        /// Adds device data request with specified parameters.
        /// </summary>
        /// <param name="errorLog">Output parameter for error logging.</param>
        /// <param name="email">Email address of the user.</param>
        /// <param name="userName">Username of the user.</param>
        /// <param name="password">Password of the user.</param>
        /// <param name="launchCount">Launch count of the device.</param>
        /// <param name="rating">Rating of the device.</param>
        public void AddDeviceDataRequest(out string errorLog, string email, string userName, string password, int launchCount, int rating, UnityAction<bool> onCompletedListener)
        {
            errorLog = "";
            if (isAnyTaskRunning)
            {
                errorLog = "Task is busy.";
                return;
            }

            isAnyTaskRunning = true;
            onAddDeviceDataCompleted.AddListener(onCompletedListener);
            addDeviceDataRequestController = new AddDeviceDataRequestController(config.AddDeviceDataFunctionConnection, OnAddDeviceDataCompleted);
            _ = addDeviceDataRequestController.PostRequestAddDeviceData(email, userName, password, launchCount, rating);
        }

        public void OnRegisterLoginCompleted(bool success)
        {
            isAnyTaskRunning = false;
            HFLogger.LogSuccess(this, "User registered and logged in.");
            onAddDeviceDataCompleted.Invoke(success);
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
                ValidateLogin(OnRegisterLoginCompleted);
                return;
            }

            UIManager.Instance.AlertController.Show("Register Failed", response.Message);
            OnUserDataAddCompleted.Invoke();
            onAddDeviceDataCompleted.Invoke(response.Success);
        }

        public void UpdateDeviceIdData(out string errorLog, string email, string password)
        {
            errorLog = "";
            if (isAnyTaskRunning)
            {
                errorLog = "Task is busy.";
                return;
            }
            isAnyTaskRunning = true;
            updateDeviceDataController = new UpdateDeviceDataController(config.UpdateDeviceDataFunctionConnection, OnUpdateDeviceDateCompleted);
            _ = updateDeviceDataController.PostRequestUpdateDeviceData(email, password, true);

        }

        public void OnUpdateDeviceDateCompleted(UpdateDeviceDataResponse response)
        {
            isAnyTaskRunning = false;
            HFLogger.Log(response, response.Message);
        }

        private UnityEvent<bool> onPasswordRecoveryCompleted = new UnityEvent<bool>();

        public void RecoveryUserPassword(out string errorLog, string email, UnityAction<bool> onRecoveryCompleted)
        {
            errorLog = "";
            if (isAnyTaskRunning)
            {
                errorLog = "Task is busy.";
                return;
            }

            onPasswordRecoveryCompleted.AddListener(onRecoveryCompleted);
            isAnyTaskRunning = true;
            recoveryUserPasswordController = new RecoveryUserPasswordController(config.UpdateUserPasswordFunctionConnection, OnRecoveryUserPasswordCompleted);
            _ = recoveryUserPasswordController.PostRequestRecoveryUserPassword(email);
        }

        private void OnRecoveryUserPasswordCompleted(RecoveryUserPasswordResponse response)
        {
            isAnyTaskRunning = false;

            HFLogger.Log(response, "User password recovered.");
            onPasswordRecoveryCompleted.Invoke(response.Success);

            if (!response.Success)
            {
                UIManager.Instance.AlertController.Show("Account Recovery Error", response.Message);
            }
            else
            {
                PlayerPrefs.SetString("UserEmail", UserManager.Instance.currentUserData.EMail);
                PlayerPrefs.SetString("UserPassword", response.Password);
                UserManager.Instance.SetEmail("");
                UserManager.Instance.SetPassword("");
                Debug.Log(response.Password);
                FindUserData();
                UIManager.Instance.AlertController.Show("Success", "Your password recovered.");
            }
        }

        public void UpdateLaunchCountRequest(out string errorLog, string email, string password, int newLaunchCount)
        {
            errorLog = "";
            if (isAnyTaskRunning)
            {
                errorLog = "Task is busy.";
                return;
            }

            isAnyTaskRunning = true;
            updateLaunchCountController = new UpdateLaunchCountController(config.UpdateLaunchCountFunctionConnection, OnUpdateLaunchCountCompleted);
            _ = updateLaunchCountController.PostRequestUpdateLaunchCount(email, password, newLaunchCount, true);
        }

        private void OnUpdateLaunchCountCompleted(UpdateLaunchCountResponse response)
        {
            isAnyTaskRunning = false;
            HFLogger.Log(response, response.Message);
        }

        public void UpdateRatingRequest(out string errorLog, string email, string password, int newRating)
        {
            errorLog = "";
            if (isAnyTaskRunning)
            {
                errorLog = "Task is busy.";
                return;
            }

            isAnyTaskRunning = true;
            updateRatingController = new UpdateRatingController(config.UpdateRatingFunctionConnection, OnUpdateRatingCompleted);
            _ = updateRatingController.PostRequestUpdateRating(email, password, newRating, true);
        }

        private void OnUpdateRatingCompleted(UpdateRatingResponse response)
        {
            isAnyTaskRunning = false;
            HFLogger.Log(response, response.Message);
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
            validatePasswordController = new ValidateUserPasswordController(config.ValidateUserPassword, OnValidateUserPasswordCompleted);
            _ = validatePasswordController.PostRequestValidateUserPassword(UserManager.Instance.currentUserData.EMail, password, true);
        }

        /// <summary>
        /// Called when device data response is completed.
        /// </summary>
        /// <param name="response">The response object.</param>
        private void OnValidateUserPasswordCompleted(ValidateUserPasswordResponse response)
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
            var email = UserManager.Instance.currentUserData.EMail;
            var password = PlayerPrefs.GetString("UserPassword");
            getLaunchCountController = new GetLaunchCountController(config.GetLaunchCountFunctionConnection, OnGetLaunchCount);
            _ = getLaunchCountController.PostRequestGetLaunchCount(email, password, true);
        }

        /// <summary>
        /// Gets the rating.
        /// </summary>
        public void GetRating()
        {
            var email = UserManager.Instance.currentUserData.EMail;
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
