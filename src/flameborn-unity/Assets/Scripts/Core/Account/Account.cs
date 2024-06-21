using System;
using flameborn.Core.User;
using HF.Logger;
using Newtonsoft.Json;
using UnityEngine;

namespace flameborn.Core.Accounts
{
    [Serializable]
    public class Account : IAccount
    {
        [field: SerializeField] private string deviceId = "";
        [field: SerializeField] private string email = "";
        [field: SerializeField] private string password = "";
        [field: SerializeField] private UserData userData = new UserData();

        [field: SerializeField] public bool IsAccountLoaded { get; set; } = false;
        [field: SerializeField] public bool IsAccountLoggedIn { get; set; } = false;

        public string PlayfabId { get; set; } = string.Empty;
        
        [JsonProperty("email")]
        public string Email { get => email; set => email = value; }

        [JsonProperty("password")]
        public string Password { get => password; set => password = value; }

        [JsonProperty("deviceId")]
        public string DeviceId { get => deviceId; set => deviceId = value; }
        public UserData UserData { get => userData; set => userData = value; }

        public void SetUserData(UserData data)
        {
            userData = data;            
        }
    }
}