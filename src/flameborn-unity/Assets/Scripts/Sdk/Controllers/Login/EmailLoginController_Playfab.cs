using System;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.Login.Abstract;
using flameborn.Sdk.Requests.Login.Entity;
using HF.Extensions;
using PlayFab;
using PlayFab.ClientModels;

namespace flameborn.Sdk.Controllers.Login
{
    /// <summary>
    /// Controller for logging in with an email address on PlayFab.
    /// </summary>
    public class EmailLoginController_Playfab : Controller<ILoginResponse>, IApiController<ILoginResponse>
    {
        #region Fields

        private string email;
        private string password;
        private string titleId;
        private bool isCombined;
        private event Action<ILoginResponse> onGetResult;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailLoginController_Playfab"/> class.
        /// </summary>
        /// <param name="email">The email address for the login request.</param>
        /// <param name="password">The password for the login request.</param>
        /// <param name="titleId">The title ID for the login request.</param>
        /// <param name="isCombined">Whether to combine user account information in the request.</param>
        public EmailLoginController_Playfab(string email, string password, string titleId, bool isCombined = true)
        {
            this.email = email;
            this.password = password;
            this.titleId = titleId;
            this.isCombined = isCombined;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sends the request to log in with an email address.
        /// </summary>
        /// <param name="errorLog">The error log to be populated in case of an error.</param>
        /// <param name="listeners">The listeners to process the response.</param>
        public override void SendRequest(out string errorLog, params Action<ILoginResponse>[] listeners)
        {
            errorLog = "";
            if (string.IsNullOrEmpty(email)) 
            { 
                errorLog = $"{nameof(email)} is null or empty."; 
            }
            if (string.IsNullOrEmpty(password) || password.Length < 6) 
            { 
                errorLog = $"{nameof(password)} is null or empty."; 
            }
            if (string.IsNullOrEmpty(titleId)) 
            { 
                errorLog = $"{nameof(titleId)} is null or empty."; 
            }

            listeners.ForEach(l => onGetResult += l);
            var request = TakeRequest();
            PlayFabClientAPI.LoginWithEmailAddress(request, OnGetLoginResult_EventListener, OnError);
        }

        /// <summary>
        /// Takes the request for logging in with an email address.
        /// </summary>
        /// <returns>The request to log in with an email address.</returns>
        private LoginWithEmailAddressRequest TakeRequest()
        {
            return new LoginWithEmailAddressRequest
            {
                Email = email,
                Password = password,
                TitleId = titleId,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
                {
                    GetUserAccountInfo = isCombined
                }
            };
        }

        /// <summary>
        /// Handles the event when the login result is received.
        /// </summary>
        /// <param name="result">The result of the login request.</param>
        private void OnGetLoginResult_EventListener(LoginResult result)
        {
            var response = new LoginResponse();
            response.SetResponse(true, true, result.NewlyCreated, result.PlayFabId, result, "Login succeed.");
            onGetResult?.Invoke(response);
        }

        /// <summary>
        /// Handles errors that occur during the login request.
        /// </summary>
        /// <param name="error">The error that occurred.</param>
        private void OnError(PlayFabError error)
        {
            var response = new LoginResponse();
            response.IsRequestSuccess = false;
            response.Message = error.ErrorMessage;
            onGetResult?.Invoke(response);
        }

        #endregion
    }
}
