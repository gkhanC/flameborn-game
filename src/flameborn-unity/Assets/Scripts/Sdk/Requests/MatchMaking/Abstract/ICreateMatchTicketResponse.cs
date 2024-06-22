using flameborn.Sdk.Requests.Abstract;

namespace flameborn.Sdk.Requests.MatchMaking.Abstract
{
    /// <summary>
    /// Interface for handling responses related to creating match tickets.
    /// </summary>
    public interface ICreateMatchTicketResponse : IApiResponse
    {
        /// <summary>
        /// Gets the ticket ID of the created match ticket.
        /// </summary>
        string TicketId { get; }
    }
}
