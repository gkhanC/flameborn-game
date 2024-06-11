using System;
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
    internal class CheckDeviceIdRequestController
    {
        private readonly string _connectionString;
        private readonly UnityAction<CheckDeviceIdResponse> _onResponseCompleted;
        internal CheckDeviceIdRequestController(string connectionString, UnityAction<CheckDeviceIdResponse> onResponseCompleted)
        {
            _connectionString = connectionString;
            _onResponseCompleted = onResponseCompleted;
        }

        internal async Task PostRequestCheckDeviceId()
        {
            var data = new DeviceDataFactory().Create();

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
                    CheckDeviceIdResponse deviceIdResponse = JsonConvert.DeserializeObject<CheckDeviceIdResponse>(responseText);

                    if (deviceIdResponse != null)
                    {
                        HFLogger.LogSuccess(deviceIdResponse, $"Response saved. {nameof(deviceIdResponse.success)}: {deviceIdResponse.success}");
                        _onResponseCompleted.Invoke(deviceIdResponse);
                    }
                    else
                    {
                        HFLogger.LogError(deviceIdResponse, "Response is null.");
                        UIManager.Instance.AlertController.ShowCriticalError("Somethings goes wrong.");
                    }
                }
            }
        }
    }
}