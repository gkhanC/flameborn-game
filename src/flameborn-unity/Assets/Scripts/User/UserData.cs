using System;

namespace Flameborn.User
{
    [Serializable]
    public class UserData : IUserData
    {
        public bool IsRegistered { get; set; } = false;
        public string EMail { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string DeviceId { get; set; } = string.Empty;
        public int LaunchCount { get; set; } = default;
    }
}