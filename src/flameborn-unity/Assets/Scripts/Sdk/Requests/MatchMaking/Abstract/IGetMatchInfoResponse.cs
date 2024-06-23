using System.Collections.Generic;
using flameborn.Sdk.Requests.Abstract;

namespace flameborn.Sdk.Requests.MatchMaking.Abstract
{
    /// <summary>
    /// Interface for handling responses related to getting match information.
    /// </summary>
    public interface IGetMatchInfoResponse : IApiResponse
    {
        /// <summary>
        /// Gets the list of player data in the match.
        /// </summary>
        List<PlayerData> Players { get; }
    }
}
