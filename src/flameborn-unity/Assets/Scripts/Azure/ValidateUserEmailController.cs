using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flameborn.Device;
using Flameborn.Managers;
using HF.Logger;
using Newtonsoft.Json;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Flameborn.Azure
{
    internal class ValidateUserEmailController : IValidateUserEmailController
    {
        private readonly string _connectionString;
        private readonly UnityAction<ValidateUserEmailResponse> _onResponseCompleted;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateUserEmailController"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string for the API.</param>
        /// <param name="onResponseCompleted">The action to invoke when the response is completed.</param>
        internal ValidateUserEmailController(string connectionString, UnityAction<ValidateUserEmailResponse> onResponseCompleted)
        {
            _connectionString = connectionString;
            _onResponseCompleted = onResponseCompleted;
        }

        /// <summary>
        /// Posts request to validate the user email.
        /// </summary>
        /// <param name="email">The email to be validated.</param>
        public async Task PostRequestValidateUserEmail(string email)
        {
            var deviceData = new DeviceDataFactory()
                .SetEmail(email)
                .Create();

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
            }
        }

        /// <summary>
        /// Handles the request error.
        /// </summary>
        /// <param name="request">The UnityWebRequest object.</param>
        private void HandleRequestError(UnityWebRequest request)
        {
            HFLogger.LogError(this, "API Call error.", request.result);
            UIManager.Instance.AlertController.Show("ERROR", "API Call error.", true);
        }

        /// <summary>
        /// Handles the request success.
        /// </summary>
        /// <param name="request">The UnityWebRequest object.</param>
        private void HandleRequestSuccess(UnityWebRequest request)
        {
            string responseText = request.downloadHandler.text;
            var deviceIdEmailResponse = JsonConvert.DeserializeObject<ValidateUserEmailResponse>(responseText);

            if (deviceIdEmailResponse != null)
            {
                _onResponseCompleted.Invoke(deviceIdEmailResponse);
            }
            else
            {
                HFLogger.LogError(this, "Response is null.");
                UIManager.Instance.AlertController.Show("ERROR", "API response lost.");
            }
        }
    }
}
