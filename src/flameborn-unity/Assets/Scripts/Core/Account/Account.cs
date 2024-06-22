using System;
using flameborn.Core.User;
using Newtonsoft.Json;
using UnityEngine;

namespace flameborn.Core.Accounts
{
    [Serializable]
    public class Account : IAccount
    {
        #region Fields

        [field: SerializeField] private string deviceId = "";
        [field: SerializeField] private string email = "";
        [field: SerializeField] private string password = "";
        [field: SerializeField] private UserData userData = new UserData();

        #endregion

        #region Properties

        [field: SerializeField] public bool IsAccountLoaded { get; set; } = false;
        [field: SerializeField] public bool IsAccountLoggedIn { get; set; } = false;
        public string PlayfabId { get; set; } = string.Empty;

        [JsonProperty("email")]
        public string Email
        {
            get => email;
            set => email = value;
        }

        [JsonProperty("password")]
        public string Password
        {
            get => password;
            set => password = value;
        }

        [JsonProperty("deviceId")]
        public string DeviceId
        {
            get => deviceId;
            set => deviceId = value;
        }

        public UserData UserData
        {
            get => userData;
            set => userData = value;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the user data for the account.
        /// </summary>
        /// <param name="data">The user data to be set.</param>
        public void SetUserData(UserData data)
        {
            userData = data;
        }

        #endregion
    }
}
