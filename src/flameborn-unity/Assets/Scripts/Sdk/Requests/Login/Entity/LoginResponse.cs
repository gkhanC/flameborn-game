using System;
using flameborn.Sdk.Requests.Entity;
using flameborn.Sdk.Requests.Login.Abstract;

namespace flameborn.Sdk.Requests.Login.Entity
{
    /// <summary>
    /// Represents the response for a login request.
    /// </summary>
    [Serializable]
    public class LoginResponse : ResponseEntity, ILoginResponse
    {
        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the account is logged in.
        /// </summary>
        public bool IsAccountLogged { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether the account is newly created.
        /// </summary>
        public bool NewlyCreated { get; set; } = false;

        /// <summary>
        /// Gets or sets the PlayFab ID associated with the account.
        /// </summary>
        public string PlayFabId { get; set; } = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginResponse"/> class.
        /// </summary>
        public LoginResponse()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the response details.
        /// </summary>
        /// <typeparam name="T">The type of the response object.</typeparam>
        /// <param name="isSuccess">Indicates if the response is successful.</param>
        /// <param name="isAccountLogged">Indicates if the account is logged in.</param>
        /// <param name="isNewly">Indicates if the account is newly created.</param>
        /// <param name="fabId">The PlayFab ID associated with the account.</param>
        /// <param name="response">The response object.</param>
        /// <param name="message">The message associated with the response.</param>
        public void SetResponse<T>(bool isSuccess, bool isAccountLogged, bool isNewly, string fabId, T response, string message = "")
        {
            IsAccountLogged = isAccountLogged;
            NewlyCreated = isNewly;
            PlayFabId = fabId;
            SetResponse(isSuccess, response, message);
        }

        #endregion
    }
}
