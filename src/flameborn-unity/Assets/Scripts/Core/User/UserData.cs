using UnityEngine;
using System;
using Newtonsoft.Json;
using flameborn.Core.User.Abstract;

namespace flameborn.Core.User
{
    /// <summary>
    /// Represents the user data.
    /// </summary>
    [Serializable]
    public class UserData : IUserData
    {
        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the user is logged in.
        /// </summary>
        [field: SerializeField]
        public bool IsLogin { get; set; } = false;

        /// <summary>
        /// Gets or sets the user's launch count.
        /// </summary>
        [JsonProperty("launchCount")]
        [field: SerializeField]
        public int LaunchCount { get; set; } = 0;

        /// <summary>
        /// Gets or sets the user's rank.
        /// </summary>
        [JsonProperty("rank")]
        [field: SerializeField]
        public int Rank { get; set; } = 0;

        /// <summary>
        /// Gets or sets the user's rating.
        /// </summary>
        [JsonProperty("rating")]
        [field: SerializeField]
        public int Rating { get; set; } = 0;

        /// <summary>
        /// Gets or sets the user's name.
        /// </summary>
        [JsonProperty("userName")]
        [field: SerializeField]
        public string UserName { get; set; } = "UnknownUser";

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserData"/> class.
        /// </summary>
        public UserData()
        {
        }

        #endregion
    }
}
