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
    public class GetAccountInfoController_Playfab : Controller<IAccountInfoResponse>, IApiController<IAccountInfoResponse>
    {
        string email;
        string password;
        string functionName;
        private event Action<IAccountInfoResponse> onGetResult;

        public GetAccountInfoController_Playfab(string email, string password, string functionName = "GetAccountInfo")
        {
            this.email = email;
            this.password = password;
            this.functionName = functionName;
        }

        public override void SendRequest(out string errorLog, params Action<IAccountInfoResponse>[] listeners)
        {
            errorLog = "";
            if (string.IsNullOrEmpty(email)) { errorLog = $"{nameof(email)} is null or empty."; }
            if (string.IsNullOrEmpty(password) || password.Length < 6) { errorLog = $"{nameof(password)} is null or empty."; }
            if (string.IsNullOrEmpty(functionName)) { errorLog = $"{nameof(functionName)} is null or empty."; }

            listeners.ForEach(l => onGetResult += l);
            var request = TakeRequest();
            PlayFabCloudScriptAPI.ExecuteFunction(request, OnGetAccountInfoResult_EventListener, OnError);
        }

        private void OnGetAccountInfoResult_EventListener(ExecuteFunctionResult result)
        {
            var response = new Requests.Data.Entity.AccountInfoResponse();
            var json = result.FunctionResult.ToString();
            var infoObject = JsonConvert.DeserializeObject<Core.Accounts.AccountInfoResponse>(json);
            response.SetResponse(true, infoObject.UserName, infoObject.Rating, infoObject.LaunchCount, result, "Get account info succeed.");
            onGetResult?.Invoke(response);
        }

        private void OnError(PlayFabError error)
        {
            var response = new Requests.Data.Entity.AccountInfoResponse();
            response.IsRequestSuccess = false;
            response.Message = error.ErrorMessage;
            onGetResult?.Invoke(response);
        }

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
    }
}