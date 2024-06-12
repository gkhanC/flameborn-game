using System;
using Flameborn.Azure;
using Flameborn.PlayFab;
using Flameborn.UI;
using Flameborn.User;
using HF.Extensions;
using HF.Logger;
using PlayFab.Internal;
using UnityEngine;

namespace Flameborn.Managers
{
    public class UserManager : MonoBehaviour
    {
        private bool _isLaunchCountUpdated;
        public static UserManager Instance { get; private set; }

        public UserData currentUserData { get; private set; } = new UserData();

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        public void SetDeviceId(string deviceId)
        {
            currentUserData.DeviceId = deviceId;
        }

        public void SetIsRegistered(bool isRegistered)
        {
            currentUserData.IsRegistered = isRegistered;
        }

        public void SetIsDeviceRegistered(bool isRegistered)
        {
            currentUserData.IsDeviceRegistered = isRegistered;
        }

        public void SetIsContainEmail(bool isContainEmail)
        {
            currentUserData.IsContainEmail = isContainEmail;
        }

        public void SetEmail(string email)
        {
            currentUserData.EMail = email;
        }

        public void SetPassword(string password, bool isCorrect = false)
        {
            currentUserData.Password = password;
            currentUserData.IsPasswordCorrect = isCorrect;
        }

        public void SetUserName(string userName)
        {
            currentUserData.UserName = userName;
        }

        public void SetLaunchCount(int launchCount)
        {
            currentUserData.LaunchCount = launchCount;
            currentUserData.IsLaunchCountLoaded = true;
        }

        public void SetRating(int rating)
        {
            currentUserData.Rating = rating;
            currentUserData.IsRatingLoaded = true;
        }

        public void UpdateLaunchCount()
        {
            if (_isLaunchCountUpdated) return;
            if (!currentUserData.IsRegistered) return;
            if (!currentUserData.IsPasswordCorrect) return;
            if (!PlayFabManager.Instance.IsLogin) return;

            currentUserData.LaunchCount++;
            AzureManager.Instance.UpdateLaunchCountRequest(out var errorLog, currentUserData.EMail, currentUserData.Password, currentUserData.LaunchCount);
            UIManager.Instance.MainMenuUIController.SetUIData();

            if (!string.IsNullOrEmpty(errorLog)) HFLogger.LogError(errorLog, errorLog);
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