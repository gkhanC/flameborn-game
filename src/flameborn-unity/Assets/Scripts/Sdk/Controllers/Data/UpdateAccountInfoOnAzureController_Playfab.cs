using System;
using flameborn.Core.Accounts;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.Data.Abstract;
using flameborn.Sdk.Requests.Data.Entity;
using HF.Extensions;
using Newtonsoft.Json;
using PlayFab;
using PlayFab.CloudScriptModels;
using UnityEngine;

namespace flameborn.Sdk.Controllers.Data
{
    public class UpdateAccountInfoOnAzureController_Playfab : Controller<IUpdateStatisticsResponse>, IApiController<IUpdateStatisticsResponse>
    {
        Account account;
        string functionName;
        private event Action<IUpdateStatisticsResponse> onGetResult;
        public UpdateAccountInfoOnAzureController_Playfab(Account account, string functionName = "AddNewAccount")
        {
            this.account = account;
            this.functionName = functionName;
        }

        public override void SendRequest(out string errorLog, params Action<IUpdateStatisticsResponse>[] listeners)
        {
            errorLog = "";

            if (string.IsNullOrEmpty(functionName)) { errorLog = $"{nameof(functionName)} is null or empty."; }
            
            listeners.ForEach(l => onGetResult += l);
            var request = TakeRequest();
            PlayFabCloudScriptAPI.ExecuteFunction(request, OnGetAccountInfoResult_EventListener, OnError);
        }

        private void OnGetAccountInfoResult_EventListener(ExecuteFunctionResult result)
        {
            var response = new UpdateStatisticsResponse();

            var json = result.FunctionResult.ToString();
            var data = JsonConvert.DeserializeObject<UpdatedAccountResponse>(json);

            response.SetResponse(data.Success, result, data.Message);
            onGetResult?.Invoke(response);
        }

        private void OnError(PlayFabError error)
        {
            var response = new UpdateStatisticsResponse();
            response.IsRequestSuccess = false;
            response.Message = error.ErrorMessage;
            onGetResult?.Invoke(response);
        }

        private ExecuteFunctionRequest TakeRequest()
        {
            return new ExecuteFunctionRequest
            {
                FunctionName = "AddNewAccount",
                FunctionParameter = new
                {
                    deviceId = SystemInfo.deviceUniqueIdentifier,
                    email = account.Email,
                    userName = account.UserData.UserName,
                    password = account.Password,
                    launchCount = account.UserData.LaunchCount,
                    rating = account.UserData.Rating
                },
                GeneratePlayStreamEvent = true
            };
        }
    }
}