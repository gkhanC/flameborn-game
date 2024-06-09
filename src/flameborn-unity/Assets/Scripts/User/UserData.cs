using System;

namespace Flameborn.User
{
    /// <summary>
    /// Represents user data and implements the IUserData interface.
    /// </summary>
    [Serializable]
    public class UserData : IUserData
    {
        /// <summary>
        /// Gets or sets a value indicating whether the user is registered.
        /// </summary>
        public bool IsRegistered { get; set; } = false;

        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        public string EMail { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's username.
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's device ID.
        /// </summary>
        public string DeviceId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the count of how many times the application has been launched.
        /// </summary>
        public int LaunchCount { get; set; } = default;
    }
}
