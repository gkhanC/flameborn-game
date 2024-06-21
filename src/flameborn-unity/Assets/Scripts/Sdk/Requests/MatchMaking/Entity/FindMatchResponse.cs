using System;
using flameborn.Sdk.Requests.Entity;
using flameborn.Sdk.Requests.MatchMaking.Abstract;

namespace flameborn.Sdk.Requests.MatchMaking.Entity
{
    [Serializable]
    public class FindMatchResponse : ResponseEntity, IFindMatchResponse
    {
        public string MatchId { get; set; } = string.Empty;

        public void SetResponse<T>(bool isSuccess, string matchId, T response, string message = "")
        {
            MatchId = matchId;
            SetResponse(isSuccess, response, message);
        }
    }
}