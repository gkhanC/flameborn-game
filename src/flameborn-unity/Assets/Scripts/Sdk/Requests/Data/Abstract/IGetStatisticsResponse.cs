using System.Collections.Generic;
using flameborn.Sdk.Requests.Abstract;

namespace flameborn.Sdk.Requests.Data.Abstract
{
    public interface IGetStatisticsResponse : IApiResponse
    {
        Dictionary<string, int> Statistics { get; }
    }
}