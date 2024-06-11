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
    internal class CheckDeviceIdEMailRequestController
    {
        private readonly string _connectionString;
        private readonly UnityAction<CheckDeviceIdEMailResponse> _onResponseCompleted;
        internal CheckDeviceIdEMailRequestController(string connectionString, UnityAction<CheckDeviceIdEMailResponse> onResponseCompleted)
        {
            _connectionString = connectionString;
            _onResponseCompleted = onResponseCompleted;
        }

        internal async Task PostRequestCheckDeviceIdEMail(string email)
        {
            var data = new DeviceDataFactory().SetEmail(email).Create();

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
                    CheckDeviceIdEMailResponse deviceIdEMailResponse = JsonConvert.DeserializeObject<CheckDeviceIdEMailResponse>(responseText);

                    if (deviceIdEMailResponse != null)
                    {
                        HFLogger.LogSuccess(deviceIdEMailResponse, $"Response saved. {nameof(deviceIdEMailResponse.success)}: {deviceIdEMailResponse.success}");
                        _onResponseCompleted.Invoke(deviceIdEMailResponse);
                    }
                    else
                    {
                        HFLogger.LogError(deviceIdEMailResponse, "Response is null.");
                        UIManager.Instance.AlertController.ShowCriticalError("Somethings goes wrong.");
                    }
                }
            }
        }
    }
}