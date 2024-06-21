using flameborn.Sdk.Requests.Abstract;

namespace flameborn.Sdk.Requests.Login.Abstract
{
    public interface ILoginResponse : IApiResponse
    {
        bool IsAccountLogged { get; }
        bool NewlyCreated { get; }
        string PlayFabId { get; }
    }
}