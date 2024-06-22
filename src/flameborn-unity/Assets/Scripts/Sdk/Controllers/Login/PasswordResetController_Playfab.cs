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
    /// Controller for resetting password via PlayFab.
    /// </summary>
    public class PasswordResetController_Playfab : Controller<IPasswordResetResponse>, IApiController<IPasswordResetResponse>
    {
        #region Fields

        private string email;
        private event Action<IPasswordResetResponse> onGetResult;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordResetController_Playfab"/> class.
        /// </summary>
        /// <param name="email">The email address associated with the account.</param>
        public PasswordResetController_Playfab(string email)
        {
            this.email = email;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sends the request to reset the password.
        /// </summary>
        /// <param name="errorLog">The error log to be populated in case of an error.</param>
        /// <param name="listeners">The listeners to process the response.</param>
        public override void SendRequest(out string errorLog, params Action<IPasswordResetResponse>[] listeners)
        {
            errorLog = "";
            if (string.IsNullOrEmpty(email)) 
            { 
                errorLog = $"{nameof(email)} is null or empty."; 
            }

            listeners.ForEach(l => onGetResult += l);
            var request = TakeRequest();
            PlayFabClientAPI.SendAccountRecoveryEmail(request, OnGetLoginResult_EventListener, OnError);
        }

        /// <summary>
        /// Handles the event when the password reset result is received.
        /// </summary>
        /// <param name="result">The result of the password reset request.</param>
        private void OnGetLoginResult_EventListener(SendAccountRecoveryEmailResult result)
        {
            var response = new PasswordResetResponse();
            response.SetResponse(true, result, "Password reset e-mail sent.");
            onGetResult?.Invoke(response);
        }

        /// <summary>
        /// Handles errors that occur during the password reset request.
        /// </summary>
        /// <param name="error">The error that occurred.</param>
        private void OnError(PlayFabError error)
        {
            var response = new PasswordResetResponse();
            response.IsRequestSuccess = false;
            response.Message = error.ErrorMessage;
            onGetResult?.Invoke(response);
        }

        /// <summary>
        /// Takes the request for sending the account recovery email.
        /// </summary>
        /// <returns>The request to send the account recovery email.</returns>
        private SendAccountRecoveryEmailRequest TakeRequest()
        {
            return new SendAccountRecoveryEmailRequest
            {
                Email = email,
                TitleId = PlayFabSettings.TitleId
            };
        }

        #endregion
    }
}
