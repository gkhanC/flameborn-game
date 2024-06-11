using Flameborn.Device;
using Flameborn.Configurations;
using Flameborn.Managers;
using HF.Extensions;
using HF.Logger;
using UnityEngine;
using UnityEngine.Events;
using System;
namespace Flameborn.Azure
{
    public class AzureManager : MonoBehaviour
    {
        public static AzureManager Instance { get; private set; }

        private bool isAnyTaskRunning;
        private AzureConfiguration config;
        private CheckDeviceIdRequestController checkDeviceIdRequestController;
        private CheckDeviceIdEMailRequestController checkDeviceIdEMailRequestController;
        private CheckDeviceDataRequestController checkDeviceDataRequestController;
        private GetLaunchCountRequestController getLaunchCountRequestController;
        private GetRatingRequestController getRatingRequestController;
        private AddDeviceDataRequestController addDeviceDataRequestController;

        private UnityEvent OnUserDataLoadCompleted { get; set; } = new UnityEvent();
        private UnityEvent OnUserDataAddCompleted { get; set; } = new UnityEvent();

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

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

        private void FindUserData()
        {
            if (isAnyTaskRunning)
                return;

            var isContainUserEmail = PlayerPrefs.HasKey("UserEmail");

            if (isContainUserEmail)
            {

                if (string.IsNullOrEmpty(UserManager.Instance.currentUserData.EMail))
                {
                    CheckUserEmail();
                    isAnyTaskRunning = true;
                    return;
                }

                if (UserManager.Instance.currentUserData.IsRegistered)
                {
                    var isContainUserPassword = PlayerPrefs.HasKey("UserEmail");
                    if (isContainUserPassword)
                    {
                        if (string.IsNullOrEmpty(UserManager.Instance.currentUserData.Password))
                        {
                            CheckUserPassword();
                            isAnyTaskRunning = true;
                            return;
                        }

                        if (UserManager.Instance.currentUserData.IsPasswordCorrect)
                        {
                            if (!UserManager.Instance.currentUserData.IsLaunchCountLoaded)
                            {
                                GetLaunchCount();
                                isAnyTaskRunning = true;
                                return;
                            }

                            if (!UserManager.Instance.currentUserData.IsRatingLoaded)
                            {
                                GetRating();
                                isAnyTaskRunning = true;
                                return;
                            }
                        }
                    }
                }
            }
            else
            {
                HFLogger.LogWarning(this,"User email not found.");
                if (string.IsNullOrEmpty(UserManager.Instance.currentUserData.DeviceId))
                {
                    checkDeviceIdRequestController = new CheckDeviceIdRequestController(config.CheckDeviceIdFunctionConnection, OnDeviceIdResponseCompleted);
                    _ = checkDeviceIdRequestController.PostRequestCheckDeviceId();
                    isAnyTaskRunning = true;
                    return;
                }
            }

            OnUserDataLoadCompleted.Invoke();
        }

        public void AddDeviceDataRequest(out string errorLog, string email, string userName, string password, int launchCount = 1, int rating = 0)
        {
            errorLog = "";
            if (isAnyTaskRunning) { errorLog = "Task is busy."; return; }

            addDeviceDataRequestController = new AddDeviceDataRequestController(config.AddDeviceDataFunctionConnection, OnAddDeviceDataResponseCompleted);
            _ = addDeviceDataRequestController.PostRequestAddDeviceData(email, userName, password, launchCount, rating);
        }

        private void OnAddDeviceDataResponseCompleted(AddDeviceDataResponse response)
        {
            isAnyTaskRunning = false;
            PlayerPrefs.SetString("UserEmail", response.email);
            PlayerPrefs.SetString("UserPassword", response.password);
            HFLogger.Log(response, response.message);
            OnUserDataAddCompleted.Invoke();
        }

        private void CheckUserEmail()
        {
            var email = PlayerPrefs.GetString("UserEmail");
            checkDeviceIdEMailRequestController = new CheckDeviceIdEMailRequestController(config.CheckDeviceIdAndEmailFunctionConnection, OnDeviceIdEmailResponseCompleted);
            _ = checkDeviceIdEMailRequestController.PostRequestCheckDeviceIdEMail(email);
        }

        private void CheckUserPassword()
        {
            var password = PlayerPrefs.GetString("UserPassword");
            checkDeviceDataRequestController = new CheckDeviceDataRequestController(config.CheckDeviceDataLoginConnection, OnDeviceDataResponseCompleted);
            _ = checkDeviceDataRequestController.PostRequestCheckDeviceIdPassword(UserManager.Instance.currentUserData.EMail, password);
        }

        private void GetLaunchCount()
        {
            var email = UserManager.Instance.currentUserData.EMail;
            var password = PlayerPrefs.GetString("UserPassword");
            getLaunchCountRequestController = new GetLaunchCountRequestController(config.GetLaunchCountFunctionConnection, OnLaunchCountResponseCompleted);
            _ = getLaunchCountRequestController.PostRequestCheckDeviceLaunchCount(email, password);
        }

        private void GetRating()
        {
            var email = UserManager.Instance.currentUserData.EMail;
            var password = PlayerPrefs.GetString("UserPassword");
            getRatingRequestController = new GetRatingRequestController(config.GetRatingFunctionConnection, OnRatingResponseCompleted);
            _ = getRatingRequestController.PostRequestCheckDeviceLaunchCount(email, password);
        }

        private void OnConfigurationLoadedEvent(AzureConfiguration configuration)
        {
            this.config = configuration;
            HFLogger.Log(configuration, "Azure configurations saved.");
            FindUserData();
        }

        private void OnDeviceIdResponseCompleted(CheckDeviceIdResponse response)
        {
            isAnyTaskRunning = false;
            UserManager.Instance.SetDeviceId(new DeviceDataFactory().Create().deviceData.DeviceId);
            UserManager.Instance.SetIsDeviceRegistered(response.success);
            HFLogger.Log(response, response.message);
            FindUserData();
        }

        private void OnDeviceIdEmailResponseCompleted(CheckDeviceIdEMailResponse response)
        {
            isAnyTaskRunning = false;
            var email = PlayerPrefs.GetString("UserEmail");
            UserManager.Instance.SetDeviceId(new DeviceDataFactory().Create().deviceData.DeviceId);
            UserManager.Instance.SetEMail(email);
            UserManager.Instance.SetIsRegistered(response.success);
            HFLogger.Log(response, response.message);

            if (response.email && response.device)
            {
                UIManager.Instance.AlertController.AlertPopUpError(response.message);
            }

            FindUserData();
        }

        private void OnDeviceDataResponseCompleted(CheckDeviceDataResponse response)
        {
            isAnyTaskRunning = false;
            if (response.success)
            {
                UserManager.Instance.SetPassword(response.message, true);
                var pass = response.message[..4];
                PlayerPrefs.SetString("UserPassword", pass);
            }
            else
            {
                UserManager.Instance.SetPassword("-1");
            }

            HFLogger.Log(response, response.message);

            FindUserData();
        }

        private void OnLaunchCountResponseCompleted(GetLaunchCountResponse response)
        {
            isAnyTaskRunning = false;
            UserManager.Instance.SetLaunchCount(response.launchCount);
            HFLogger.Log(response, response.message);
            FindUserData();
        }

        private void OnRatingResponseCompleted(GetRatingResponse response)
        {
            isAnyTaskRunning = false;
            UserManager.Instance.SetRating(response.rating);
            HFLogger.Log(response, response.message);
            FindUserData();
        }

        public void SubscribeOnUserDataLoadCompleted(UnityAction onUserDataLoadCompleted)
        {
            OnUserDataLoadCompleted.AddListener(onUserDataLoadCompleted);
        }

        public void SubscribeOnUserDataAddCompleted(UnityAction onUserDataLoadCompleted)
        {
            OnUserDataAddCompleted.AddListener(onUserDataLoadCompleted);
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