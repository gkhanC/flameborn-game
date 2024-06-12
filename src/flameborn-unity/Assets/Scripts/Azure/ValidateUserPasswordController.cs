using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flameborn.Device;
using Flameborn.Managers;
using HF.Logger;
using Newtonsoft.Json;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine;

namespace Flameborn.Azure
{
    internal class ValidateUserPasswordController : IValidateUserPasswordController
    {
        private readonly string _connectionString;
        private readonly UnityAction<ValidateUserPasswordResponse> _onResponseCompleted;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateUserPasswordController"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string for the API.</param>
        /// <param name="onResponseCompleted">The action to invoke when the response is completed.</param>
        internal ValidateUserPasswordController(string connectionString, UnityAction<ValidateUserPasswordResponse> onResponseCompleted)
        {
            _connectionString = connectionString;
            _onResponseCompleted = onResponseCompleted;
        }

        /// <summary>
        /// Posts request to validate the user password with the specified email and password.
        /// </summary>
        /// <param name="email">The email associated with the user.</param>
        /// <param name="password">The password to be validated.</param>
        public async Task PostRequestValidateUserPassword(string email, string password, bool isHash = false)
        {
            var deviceData = new DeviceDataFactory().Create();
            if (!isHash)
            {
                deviceData = new DeviceDataFactory()
                     .SetEmail(email)
                     .SetPassword(password)
                     .Create();
            }
            else
            {
                deviceData = new DeviceDataFactory()
                                     .SetEmail(email)
                                     .Create();
                deviceData.deviceData.SetPasswordHash(password);
            }

            if (deviceData.errorLogs.Count > 0)
            {
                HandleErrorLogs(deviceData.errorLogs);
                return;
            }

            string jsonData = JsonConvert.SerializeObject(deviceData.deviceData);

            using (UnityWebRequest request = new UnityWebRequest(_connectionString, "POST"))
            {
                byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");

                await SendRequest(request);
            }
        }

        /// <summary>
        /// Sends the request and handles the response.
        /// </summary>
        /// <param name="request">The UnityWebRequest object.</param>
        private async Task SendRequest(UnityWebRequest request)
        {
            var asyncOperation = request.SendWebRequest();
            while (!asyncOperation.isDone)
            {
                await Task.Yield();
            }

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                HandleRequestError(request);
            }
            else
            {
                HandleRequestSuccess(request);
            }
        }

        /// <summary>
        /// Handles error logs by logging them and showing alert popups.
        /// </summary>
        /// <param name="errorLogs">The list of error logs.</param>
        private void HandleErrorLogs(List<string> errorLogs)
        {
            foreach (var error in errorLogs)
            {
                UIManager.Instance.AlertController.Show("ERROR", error);
                HFLogger.LogError(typeof(DeviceData), error);
                var deviceDataResponse = new ValidateUserPasswordResponse();
                deviceDataResponse.Success = false;
                _onResponseCompleted.Invoke(deviceDataResponse);
            }
        }

        /// <summary>
        /// Handles the request error.
        /// </summary>
        /// <param name="request">The UnityWebRequest object.</param>
        private void HandleRequestError(UnityWebRequest request)
        {
            HFLogger.LogError(this, "API Call error.", request.result, request.error);

            UIManager.Instance.AlertController.Show("ERROR", "API Call error.", true);
        }

        /// <summary>
        /// Handles the request success.
        /// </summary>
        /// <param name="request">The UnityWebRequest object.</param>
        private void HandleRequestSuccess(UnityWebRequest request)
        {
            string responseText = request.downloadHandler.text;
            var deviceDataResponse = JsonConvert.DeserializeObject<ValidateUserPasswordResponse>(responseText);

            if (deviceDataResponse != null)
            {

                _onResponseCompleted.Invoke(deviceDataResponse);
            }
            else
            {
                HFLogger.LogError(this, "Response is null.");
                UIManager.Instance.AlertController.Show("WARNING", "API response is lost.");
            }
        }
    }
}
