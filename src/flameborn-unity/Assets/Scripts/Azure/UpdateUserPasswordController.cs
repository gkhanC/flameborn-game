using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Flameborn.Device;
using Flameborn.Managers;
using HF.Logger;
using Newtonsoft.Json;
using Sirenix.Utilities;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Flameborn.Azure
{
    internal class RecoveryUserPasswordController : IRecoveryUserPasswordController
    {
        private readonly string _connectionString;
        private readonly UnityEvent<RecoveryUserPasswordResponse> _onResponseCompleted;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="RecoveryUserPasswordController"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string for the API.</param>
        /// <param name="listeners">The action to invoke when the response is completed.</param>
        internal RecoveryUserPasswordController(string connectionString, params Action<RecoveryUserPasswordResponse>[] listeners)
        {
            _onResponseCompleted = new UnityEvent<RecoveryUserPasswordResponse>();
            _connectionString = connectionString;
            listeners.ForEach(a => _onResponseCompleted.AddListener(new UnityAction<RecoveryUserPasswordResponse>(a)));
        }

        /// <summary>
        /// Posts request to update user password with the specified email and password.
        /// </summary>
        /// <param name="email">The email associated with the device.</param>
        /// <param name="password">The password to be updated.</param>
        public async Task PostRequestRecoveryUserPassword(string email)
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
                HFLogger.LogError(typeof(DeviceData), error);
                UIManager.Instance.AlertController.Show("ERROR", "Something went wrong.", true);
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
            var updateDeviceDataResponse = JsonConvert.DeserializeObject<RecoveryUserPasswordResponse>(responseText);

            if (updateDeviceDataResponse != null)
            {
                _onResponseCompleted.Invoke(updateDeviceDataResponse);
            }
            else
            {
                HFLogger.LogError(this, "Response is null.");
                UIManager.Instance.AlertController.Show("ERROR", "API response lost.", true);
            }
        }
    }
}
