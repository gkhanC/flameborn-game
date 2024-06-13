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
    internal class AddDeviceDataRequestController : IAddDeviceDataRequestController
    {
        private readonly string _connectionString;
        private readonly UnityEvent<AddDeviceDataResponse> _onResponseCompleted;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddDeviceDataRequestController"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string for the API.</param>
        /// <param name="onResponseCompleted">The action to invoke when the response is completed.</param>
        internal AddDeviceDataRequestController(string connectionString, params Action<AddDeviceDataResponse>[] onResponseCompleted)
        {
            _onResponseCompleted = new UnityEvent<AddDeviceDataResponse>();
            _connectionString = connectionString;
            onResponseCompleted.ForEach(a => _onResponseCompleted.AddListener(new UnityAction<AddDeviceDataResponse>(a)));
        }

        /// <summary>
        /// Posts request to add device data with the specified parameters.
        /// </summary>
        /// <param name="email">The email associated with the device.</param>
        /// <param name="userName">The username associated with the device.</param>
        /// <param name="password">The password associated with the device.</param>
        /// <param name="launchCount">The launch count of the device.</param>
        /// <param name="rating">The rating of the device.</param>
        public async Task PostRequestAddDeviceData(string email, string userName, string password, int launchCount = 1, int rating = 0)
        {
            var deviceData = new DeviceDataFactory()
                     .SetEmail(email)
                     .SetPassword(password)
                     .SetUserName(userName)
                     .SetLaunchCount(launchCount)
                     .SetRating(rating)
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
            }
        }

        /// <summary>
        /// Handles the request error.
        /// </summary>
        /// <param name="request">The UnityWebRequest object.</param>
        private void HandleRequestError(UnityWebRequest request)
        {
            HFLogger.LogError(this.GetType(), "API Call error.", request.result);
            UIManager.Instance.AlertController.Show("ERROR", "API Call error.", true);
        }

        /// <summary>
        /// Handles the request success.
        /// </summary>
        /// <param name="request">The UnityWebRequest object.</param>
        private void HandleRequestSuccess(UnityWebRequest request)
        {
            string responseText = request.downloadHandler.text;
            var addDeviceDataResponse = JsonConvert.DeserializeObject<AddDeviceDataResponse>(responseText);

            if (addDeviceDataResponse != null)
            {
                _onResponseCompleted.Invoke(addDeviceDataResponse);
            }
            else
            {
                HFLogger.LogError(this.GetType(), "Response is null.");
                UIManager.Instance.AlertController.Show("ERROR", "API response lost.");
            }
        }
    }
}
