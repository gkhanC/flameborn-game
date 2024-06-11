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
    internal class CheckDeviceDataRequestController
    {
        private readonly string _connectionString;
        private readonly UnityAction<CheckDeviceDataResponse> _onResponseCompleted;
        internal CheckDeviceDataRequestController(string connectionString, UnityAction<CheckDeviceDataResponse> onResponseCompleted)
        {
            _connectionString = connectionString;
            _onResponseCompleted = onResponseCompleted;
        }

        internal async Task PostRequestCheckDeviceIdPassword(string email, string password)
        {
            var data = new DeviceDataFactory().SetEmail(email).SetPassword(password).Create();

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
                    CheckDeviceDataResponse deviceDataResponse = JsonConvert.DeserializeObject<CheckDeviceDataResponse>(responseText);

                    if (deviceDataResponse != null)
                    {
                        HFLogger.LogSuccess(deviceDataResponse, $"Response saved. {nameof(deviceDataResponse.success)}: {deviceDataResponse.success} {deviceDataResponse.message}");
                        _onResponseCompleted.Invoke(deviceDataResponse);
                    }
                    else
                    {
                        HFLogger.LogError(deviceDataResponse, "Response is null.");
                        UIManager.Instance.AlertController.ShowCriticalError("Somethings goes wrong.");
                    }
                }
            }
        }
    }
}