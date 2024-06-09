using System;
using MADD;
using PlayFab;
using PlayFab.ClientModels;

namespace Flameborn.PlayFab
{
    /// <summary>
    /// Holds data required for PlayFab login operations.
    /// </summary>
    [Serializable]
    [Docs("Holds data required for PlayFab login operations.")]
    public class PlayFabLoginData
    {
        /// <summary>
        /// Indicates whether an account can be created if one does not exist.
        /// </summary>
        [Docs("Indicates whether an account can be created if one does not exist.")]
        public bool CanCreateAccount { get; set; } = false;

        /// <summary>
        /// The device ID used for login.
        /// </summary>
        [Docs("The device ID used for login.")]
        public string DeviceId { get; set; } = String.Empty;

        /// <summary>
        /// Action to perform on successful login.
        /// </summary>
        [Docs("Action to perform on successful login.")]
        public Action<LoginResult> OnLoginSuccess { get; set; }

        /// <summary>
        /// Action to perform on login failure.
        /// </summary>
        [Docs("Action to perform on login failure.")]
        public Action<PlayFabError> OnLoginFailure { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayFabLoginData"/> class with the specified parameters.
        /// </summary>
        /// <param name="canCreateAccount">Indicates whether an account can be created if one does not exist.</param>
        /// <param name="deviceId">The device ID used for login.</param>
        /// <param name="onLoginSuccess">Reference to the action to perform on successful login.</param>
        /// <param name="onLoginFailure">Reference to the action to perform on login failure.</param>
        [Docs("Initializes a new instance of the PlayFabLoginData class with the specified parameters.")]
        public PlayFabLoginData(bool canCreateAccount, string deviceId, ref Action<LoginResult> onLoginSuccess, ref Action<PlayFabError> onLoginFailure)
        {
            this.CanCreateAccount = canCreateAccount;
            this.DeviceId = deviceId;
            this.OnLoginSuccess = onLoginSuccess;
            this.OnLoginFailure = onLoginFailure;
        }
    }
}
