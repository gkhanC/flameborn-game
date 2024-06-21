using System;
using flameborn.Sdk.Requests.Entity;
using flameborn.Sdk.Requests.MatchMaking.Abstract;

namespace flameborn.Sdk.Requests.MatchMaking.Entity
{
    [Serializable]
    public class CreateMatchTicketResponse : ResponseEntity, ICreateMatchTicketResponse
    {
        public string TicketId { get; set; } = string.Empty;

        public CreateMatchTicketResponse()
        {

        }
        public void SetResponse<T>(bool isSuccess, string ticketId, T response, string message = "")
        {
            TicketId = ticketId;
            SetResponse(isSuccess, response, message);
        }
    }
}