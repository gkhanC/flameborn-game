namespace Flameborn.User
{
    /// <summary>
    /// Interface for user data.
    /// </summary>
    public interface IUserData
    {
        /// <summary>
        /// Gets a value indicating whether the user is registered.
        /// </summary>
        bool IsRegistered { get; }

        /// <summary>
        /// Gets a value indicating whether the user device is registered.
        /// </summary>
        bool IsDeviceRegistered { get; }

        /// <summary>
        /// Gets the user's email address.
        /// </summary>
        string EMail { get; }

        // <summary>
        /// Gets or sets the user's password.
        /// </summary>
        string Password { get; }

        /// <summary>
        /// Gets the user's username.
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Gets the user's device ID.
        /// </summary>
        string DeviceId { get; }

        int LaunchCount { get; }
        int Rating { get; }
    }
}
