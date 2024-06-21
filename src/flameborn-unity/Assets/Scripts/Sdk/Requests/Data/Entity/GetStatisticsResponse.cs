using System.Collections.Generic;
using flameborn.Sdk.Requests.Data.Abstract;
using flameborn.Sdk.Requests.Entity;

namespace flameborn.Sdk.Requests.Data.Entity
{
    public class GetStatisticsResponse : ResponseEntity, IGetStatisticsResponse
    {
        public Dictionary<string, int> Statistics { get; set; } = new Dictionary<string, int>();

        public void SetResponse<T>(bool isSuccess, Dictionary<string, int> statistics, T response, string message = "")
        {
            Statistics = statistics;
            SetResponse(isSuccess, response, message);
        }
    }
}