namespace flameborn.Core.User.Abstract
{
    /// <summary>
    /// Defines an interface for user data.
    /// </summary>
    public interface IUserData
    {
        #region Properties

        /// <summary>
        /// Gets or sets the user's launch count.
        /// </summary>
        int LaunchCount { get; set; }

        /// <summary>
        /// Gets or sets the user's rank.
        /// </summary>
        int Rank { get; set; }

        /// <summary>
        /// Gets or sets the user's rating.
        /// </summary>
        int Rating { get; set; }

        /// <summary>
        /// Gets or sets the user's name.
        /// </summary>
        string UserName { get; set; }

        #endregion
    }
}
