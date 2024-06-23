using flameborn.Core.User;

namespace flameborn.Core.Accounts
{
    /// <summary>
    /// Represents an account interface with user data management functionalities.
    /// </summary>
    public interface IAccount
    {
        /// <summary>
        /// Gets the user data associated with the account.
        /// </summary>
        UserData UserData { get; }

        /// <summary>
        /// Sets the user data for the account.
        /// </summary>
        /// <param name="data">The user data to be set.</param>
        void SetUserData(UserData data);
    }
}
