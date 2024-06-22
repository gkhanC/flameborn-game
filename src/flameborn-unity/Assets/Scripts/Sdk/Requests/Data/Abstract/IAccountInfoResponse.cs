using flameborn.Sdk.Requests.Abstract;

namespace flameborn.Sdk.Requests.Data.Abstract
{
    /// <summary>
    /// Defines an interface for account info responses.
    /// </summary>
    public interface IAccountInfoResponse : IApiResponse
    {
        #region Properties

        /// <summary>
        /// Gets the launch count of the account.
        /// </summary>
        int LaunchCount { get; }

        /// <summary>
        /// Gets the rating of the account.
        /// </summary>
        int Rating { get; }

        /// <summary>
        /// Gets the user name of the account.
        /// </summary>
        string UserName { get; }

        #endregion
    }
}
