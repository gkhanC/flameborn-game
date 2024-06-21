using flameborn.Sdk.Requests.Abstract;

namespace flameborn.Sdk.Requests.MatchMaking.Abstract
{
    public interface ICreateMatchTicketResponse : IApiResponse
    {
        string TicketId { get; }
    }
}