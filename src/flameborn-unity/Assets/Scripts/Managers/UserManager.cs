using Flameborn.User;
using HF.Extensions;
using UnityEngine;

namespace Flameborn.Managers
{
    public class UserManager : MonoBehaviour
    {
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

        public void SetEMail(string email)
        {
            currentUserData.EMail = email;
        }

        public void SetPassword(string password, bool isCorrect = false)
        {
            currentUserData.Password = password;
            currentUserData.IsPasswordCorrect = isCorrect;
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