using System;

namespace flameborn
{
    /// <summary>
    /// Represents the data for a player.
    /// </summary>
    [Serializable]
    public class PlayerData
    {
        #region Properties

        /// <summary>
        /// Gets the player's Fab ID.
        /// </summary>
        public string FabId { get; private set; }

        /// <summary>
        /// Gets the player's rank.
        /// </summary>
        public string Rank { get; private set; }

        /// <summary>
        /// Gets the player's rating.
        /// </summary>
        public string Rating { get; private set; }

        /// <summary>
        /// Gets the player's user name.
        /// </summary>
        public string UserName { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerData"/> class.
        /// </summary>
        /// <param name="fabId">The player's Fab ID.</param>
        /// <param name="userName">The player's user name.</param>
        /// <param name="rating">The player's rating.</param>
        /// <param name="rank">The player's rank.</param>
        public PlayerData(string fabId, string userName, string rating, string rank)
        {
            FabId = fabId;
            UserName = userName;
            Rating = rating;
            Rank = rank;
        }

        #endregion
    }
}
