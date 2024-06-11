using System;
using Flameborn.Device.Abstract;
using UnityEngine;

namespace Flameborn.Device
{
    [Serializable]
    public class DeviceData : IDeviceData
    {
        private readonly string _deviceId;

        public string DeviceId => _deviceId;

        [field: SerializeField]
        public string EMail { get; private set; } = string.Empty;

        [field: SerializeField]
        public string Password { get; private set; } = string.Empty;

        [field: SerializeField]
        public string UserName { get; private set; } = string.Empty;

        [field: SerializeField]
        public int LaunchCount { get; private set; } = default;

        [field: SerializeField]
        public int Rating { get; private set; } = default;

        internal DeviceData()
        {
            _deviceId = UnityEngine.SystemInfo.deviceUniqueIdentifier;
        }

        public IDeviceData SetEmail(string email)
        {
            EMail = email;
            return this;
        }

        public IDeviceData SetPassword(string password)
        {
            Password = password;
            return this;
        }

        public IDeviceData SetUserName(string userName)
        {
            UserName = userName;
            return this;
        }

        public IDeviceData SetLaunchCount(int launchCount)
        {
            LaunchCount = launchCount;
            return this;
        }

        public IDeviceData SetRating(int rating)
        {
            Rating = rating;
            return this;
        }
    }
}