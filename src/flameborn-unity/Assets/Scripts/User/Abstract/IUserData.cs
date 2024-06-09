using MADD;

namespace Flameborn.User
{
    /// <summary>
    /// Interface for user data.
    /// </summary>
    [Docs("Interface for user data.")]
    public interface IUserData
    {
        /// <summary>
        /// Gets a value indicating whether the user is registered.
        /// </summary>
        [Docs("Gets a value indicating whether the user is registered.")]
        bool IsRegistered { get; }

        /// <summary>
        /// Gets the user's email address.
        /// </summary>
        [Docs("Gets the user's email address.")]
        string EMail { get; }

        /// <summary>
        /// Gets the user's username.
        /// </summary>
        [Docs("Gets the user's username.")]
        string UserName { get; }

        /// <summary>
        /// Gets the user's device ID.
        /// </summary>
        [Docs("Gets the user's device ID.")]
        string DeviceId { get; }

        /// <summary>
        /// Gets the count of how many times the application has been launched.
        /// </summary>
        [Docs("Gets the count of how many times the application has been launched.")]
        int LaunchCount { get; }
    }
}
