using System.Collections.Generic;
using flameborn.Sdk.Requests.Abstract;

namespace flameborn.Sdk.Requests.MatchMaking.Abstract
{
    public interface IGetMatchInfoResponse : IApiResponse
    {
        List<PlayerData> players { get; }
    }
}