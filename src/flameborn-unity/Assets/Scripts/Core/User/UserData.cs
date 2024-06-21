using UnityEngine;
using System;
using Newtonsoft.Json;
using flameborn.Core.User.Abstract;

namespace flameborn.Core.User
{
    [Serializable]
    public class UserData : IUserData
    {
        [field: SerializeField] public bool IsLogin { get; set; } = false;

        [JsonProperty("userName")]
        [field: SerializeField] public string UserName { get; set; } = "UnknownUser";

        [JsonProperty("rating")]
        [field: SerializeField] public int Rating { get; set; } = 0;

        [JsonProperty("rank")]
        [field: SerializeField] public int Rank { get; set; } = 0;

        [JsonProperty("launchCount")]
        [field: SerializeField] public int LaunchCount { get; set; } = 0;

        public UserData()
        {

        }

    }
}