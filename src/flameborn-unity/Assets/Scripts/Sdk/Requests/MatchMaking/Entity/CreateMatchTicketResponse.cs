using System;
using flameborn.Sdk.Requests.Entity;
using flameborn.Sdk.Requests.MatchMaking.Abstract;

namespace flameborn.Sdk.Requests.MatchMaking.Entity
{
    /// <summary>
    /// Represents a response for creating a match ticket.
    /// </summary>
    [Serializable]
    public class CreateMatchTicketResponse : ResponseEntity, ICreateMatchTicketResponse
    {
        #region Properties

        /// <summary>
        /// Gets or sets the ticket ID of the created match ticket.
        /// </summary>
        public string TicketId { get; set; } = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMatchTicketResponse"/> class.
        /// </summary>
        public CreateMatchTicketResponse()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the response details.
        /// </summary>
        /// <typeparam name="T">The type of the response.</typeparam>
        /// <param name="isSuccess">Indicates whether the operation was successful.</param>
        /// <param name="ticketId">The ticket ID of the created match ticket.</param>
        /// <param name="response">The response object.</param>
        /// <param name="message">An optional message describing the result.</param>
        public void SetResponse<T>(bool isSuccess, string ticketId, T response, string message = "")
        {
            TicketId = ticketId;
            SetResponse(isSuccess, response, message);
        }

        #endregion
    }
}
