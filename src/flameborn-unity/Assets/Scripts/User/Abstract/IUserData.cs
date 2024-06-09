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
        /// Gets the user's email address.
        /// </summary>
        string EMail { get; }

        /// <summary>
        /// Gets the user's username.
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Gets the user's device ID.
        /// </summary>
        string DeviceId { get; }

        /// <summary>
        /// Gets the count of how many times the application has been launched.
        /// </summary>
        int LaunchCount { get; }
    }
}
