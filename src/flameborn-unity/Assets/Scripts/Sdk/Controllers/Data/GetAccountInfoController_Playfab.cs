using System;
using flameborn.Core.Accounts;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.Data.Abstract;
using flameborn.Sdk.Requests.Data.Entity;
using HF.Extensions;
using Newtonsoft.Json;
using PlayFab;
using PlayFab.CloudScriptModels;

namespace flameborn.Sdk.Controllers.Data
{
    /// <summary>
    /// Controller for getting account information from PlayFab.
    /// </summary>
    public class GetAccountInfoController_Playfab : Controller<IAccountInfoResponse>, IApiController<IAccountInfoResponse>
    {
        #region Fields

        private string email;
        private string password;
        private string functionName;
        private event Action<IAccountInfoResponse> onGetResult;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAccountInfoController_Playfab"/> class.
        /// </summary>
        /// <param name="email">The email associated with the account.</param>
        /// <param name="password">The password associated with the account.</param>
        /// <param name="functionName">The name of the function to execute.</param>
        public GetAccountInfoController_Playfab(string email, string password, string functionName = "GetAccountInfo")
        {
            this.email = email;
            this.password = password;
            this.functionName = functionName;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sends the request to get account information.
        /// </summary>
        /// <param name="errorLog">The error log to be populated in case of an error.</param>
        /// <param name="listeners">The listeners to process the response.</param>
        public override void SendRequest(out string errorLog, params Action<IAccountInfoResponse>[] listeners)
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
            if (string.IsNullOrEmpty(functionName)) 
            { 
                errorLog = $"{nameof(functionName)} is null or empty."; 
            }

            listeners.ForEach(l => onGetResult += l);
            var request = TakeRequest();
            PlayFabCloudScriptAPI.ExecuteFunction(request, OnGetAccountInfoResult_EventListener, OnError);
        }

        /// <summary>
        /// Takes the request for getting account information.
        /// </summary>
        /// <returns>The request to get account information.</returns>
        private ExecuteFunctionRequest TakeRequest()
        {
            return new ExecuteFunctionRequest
            {
                FunctionName = functionName,
                FunctionParameter = new
                {
                    email = email,
                    password = password
                },
                GeneratePlayStreamEvent = true
            };
        }

        /// <summary>
        /// Handles the event when the account info result is received.
        /// </summary>
        /// <param name="result">The result of the account info request.</param>
        private void OnGetAccountInfoResult_EventListener(ExecuteFunctionResult result)
        {
            var response = new Requests.Data.Entity.AccountInfoResponse();
            var json = result.FunctionResult.ToString();
            var infoObject = JsonConvert.DeserializeObject<Core.Accounts.AccountInfoResponse>(json);
            response.SetResponse(true, infoObject.UserName, infoObject.Rating, infoObject.LaunchCount, result, "Get account info succeed.");
            onGetResult?.Invoke(response);
        }

        /// <summary>
        /// Handles errors that occur during the account info request.
        /// </summary>
        /// <param name="error">The error that occurred.</param>
        private void OnError(PlayFabError error)
        {
            var response = new Requests.Data.Entity.AccountInfoResponse();
            response.IsRequestSuccess = false;
            response.Message = error.ErrorMessage;
            onGetResult?.Invoke(response);
        }

        #endregion
    }
}
