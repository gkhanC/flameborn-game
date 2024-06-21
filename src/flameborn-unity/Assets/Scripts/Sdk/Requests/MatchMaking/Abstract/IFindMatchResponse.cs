using flameborn.Sdk.Requests.Abstract;

namespace flameborn.Sdk.Requests.MatchMaking.Abstract
{
    public interface IFindMatchResponse : IApiResponse
    {
        string MatchId { get; }
    }
}