using System;
using MADD;

namespace Flameborn.User
{
    /// <summary>
    /// Represents user data and implements the IUserData interface.
    /// </summary>
    [Serializable]
    [Docs("Represents user data and implements the IUserData interface.")]
    public class UserData : IUserData
    {
        /// <summary>
        /// Gets or sets a value indicating whether the user is registered.
        /// </summary>
        [Docs("Gets or sets a value indicating whether the user is registered.")]
        public bool IsRegistered { get; set; } = false;

        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        [Docs("Gets or sets the user's email address.")]
        public string EMail { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's username.
        /// </summary>
        [Docs("Gets or sets the user's username.")]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's device ID.
        /// </summary>
        [Docs("Gets or sets the user's device ID.")]
        public string DeviceId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the count of how many times the application has been launched.
        /// </summary>
        [Docs("Gets or sets the count of how many times the application has been launched.")]
        public int LaunchCount { get; set; } = default;
    }
}
