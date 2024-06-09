using System;
using PlayFab;
using PlayFab.ClientModels;

namespace Flameborn.PlayFab
{
    [Serializable]
    public class PlayFabLoginData
    {
        public bool CanCreateAccount { get; set; } = false;
        public string DeviceId { get; set; } = String.Empty;

        public Action<LoginResult> OnLoginSuccess { get; set; }
        public Action<PlayFabError> OnLoginFailure { get; set; }

        public PlayFabLoginData(bool canCreateAccount, string deviceId, ref Action<LoginResult> onLoginSuccess, ref Action<PlayFabError> onLoginFailure)
        {
            this.CanCreateAccount = canCreateAccount;
            this.DeviceId = deviceId;
            this.OnLoginSuccess = onLoginSuccess;
            this.OnLoginFailure = onLoginFailure;
        }

    }
}