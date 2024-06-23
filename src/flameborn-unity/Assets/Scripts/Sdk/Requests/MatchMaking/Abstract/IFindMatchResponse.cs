using flameborn.Sdk.Requests.Abstract;

namespace flameborn.Sdk.Requests.MatchMaking.Abstract
{
    /// <summary>
    /// Interface for handling responses related to finding matches.
    /// </summary>
    public interface IFindMatchResponse : IApiResponse
    {
        /// <summary>
        /// Gets the match ID of the found match.
        /// </summary>
        string MatchId { get; }
    }
}
