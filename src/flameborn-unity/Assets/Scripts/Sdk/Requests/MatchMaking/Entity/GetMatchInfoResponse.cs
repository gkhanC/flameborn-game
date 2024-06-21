using System;
using System.Collections.Generic;
using flameborn.Sdk.Requests.Entity;
using flameborn.Sdk.Requests.MatchMaking.Abstract;

namespace flameborn.Sdk.Requests.MatchMaking.Entity
{
    [Serializable]
    public class GetMatchInfoResponse : ResponseEntity, IGetMatchInfoResponse
    {
        public List<PlayerData> players { get; set; } = new List<PlayerData>();

        public void SetResponse<T>(bool isSuccess, List<PlayerData> players, T response, string message = "")
        {
            this.players = players;
            SetResponse(isSuccess, response, message);
        }
    }
}