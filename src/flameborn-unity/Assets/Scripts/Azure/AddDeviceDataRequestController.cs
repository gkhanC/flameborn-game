using System.Text;
using System.Threading.Tasks;
using Flameborn.Device;
using Flameborn.Managers;
using HF.Logger;
using Newtonsoft.Json;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Flameborn.Azure
{
    internal class AddDeviceDataRequestController
    {
        private readonly string _connectionString;
        private readonly UnityAction<AddDeviceDataResponse> _onResponseCompleted;
        internal AddDeviceDataRequestController(string connectionString, UnityAction<AddDeviceDataResponse> onResponseCompleted)
        {
            _connectionString = connectionString;
            _onResponseCompleted = onResponseCompleted;
        }

        internal async Task PostRequestAddDeviceData(string email, string userName, string password, int launchCount = 1, int rating = 0)
        {
            var data = new DeviceDataFactory()
            .SetEmail(email)
            .SetUserName(userName)
            .SetPassword(password)
            .SetLaunchCount(launchCount)
            .SetRating(rating).Create();

            if (data.errorLogs.Count > 0)
            {
                foreach (var e in data.errorLogs)
                {
                    UIManager.Instance.AlertController.AlertPopUpError(e);
                    HFLogger.LogError(typeof(DeviceData), e);
                }
                return;
            }

            string jsonData = JsonConvert.SerializeObject(data.deviceData);

            using (UnityWebRequest request = new UnityWebRequest(_connectionString, "POST"))
            {
                byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");

                var asyncOperation = request.SendWebRequest();
                while (!asyncOperation.isDone)
                {
                    await Task.Yield();
                }

                if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
                {
                    HFLogger.LogError(request, "API Call error.", request.result);
                    UIManager.Instance.AlertController.ShowCriticalError("API Call error.");
                }
                else
                {
                    string responseText = request.downloadHandler.text;
                    AddDeviceDataResponse addDeviceDataResponse = JsonConvert.DeserializeObject<AddDeviceDataResponse>(responseText);

                    if (addDeviceDataResponse != null)
                    {
                        HFLogger.LogSuccess(addDeviceDataResponse, $"Response saved. {nameof(addDeviceDataResponse.success)}: {addDeviceDataResponse.success} ", addDeviceDataResponse.message);
                        _onResponseCompleted.Invoke(addDeviceDataResponse);
                    }
                    else
                    {
                        HFLogger.LogError(addDeviceDataResponse, "Response is null.");
                        UIManager.Instance.AlertController.ShowCriticalError("Somethings goes wrong.");
                    }
                }
            }
        }
    }
}