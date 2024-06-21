using flameborn.Sdk.Requests.Abstract;

namespace flameborn.Sdk.Requests.Data.Abstract
{
    public interface IAccountInfoResponse : IApiResponse
    {
        string UserName { get; }
        int Rating { get; }
        int LaunchCount { get; }
    }
}