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
    internal class UpdateRatingController : IUpdateRatingController
    {
        private readonly string _connectionString;
        private readonly UnityEvent<UpdateRatingResponse> _onResponseCompleted;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRatingController"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string for the API.</param>
        /// <param name="onResponseCompleted">The action to invoke when the response is completed.</param>
        internal UpdateRatingController(string connectionString, params Action<UpdateRatingResponse>[] onResponseCompleted)
        {
            _onResponseCompleted = new UnityEvent<UpdateRatingResponse>();
            _connectionString = connectionString;
            onResponseCompleted.ForEach(a => _onResponseCompleted.AddListener(new UnityAction<UpdateRatingResponse>(a)));
        }

        /// <summary>
        /// Posts request to update rating with the specified email, password, and rating.
        /// </summary>
        /// <param name="email">The email associated with the device.</param>
        /// <param name="password">The password associated with the device.</param>
        /// <param name="rating">The rating to be updated.</param>
        public async Task PostRequestUpdateRating(string email, string password, int rating, bool isHash = false)
        {
            var deviceData = new DeviceDataFactory().Create();
            if (!isHash)
            {
                deviceData = new DeviceDataFactory()
                     .SetEmail(email)
                     .SetPassword(password)
                     .SetRating(rating)
                     .Create();
            }
            else
            {
                deviceData = new DeviceDataFactory()
                                     .SetEmail(email)
                                     .SetRating(rating)
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
        }

        /// <summary>
        /// Handles the request success.
        /// </summary>
        /// <param name="request">The UnityWebRequest object.</param>
        private void HandleRequestSuccess(UnityWebRequest request)
        {
            string responseText = request.downloadHandler.text;
            var ratingResponse = JsonConvert.DeserializeObject<UpdateRatingResponse>(responseText);

            if (ratingResponse != null)
            {
                _onResponseCompleted.Invoke(ratingResponse);
            }
            else
            {
                HFLogger.LogError(this, "Response is null.");
            }
        }
    }
}
