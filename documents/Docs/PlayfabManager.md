
# PlayfabManager Class Documentation

## Overview
The `PlayfabManager` class manages PlayFab operations within the Flameborn SDK. This class is a singleton derived from `MonoBehaviourSingleton` and implements the `IManager` interface.

## Class Definition

```csharp
using System;
using flameborn.Core.Managers;
using flameborn.Core.Managers.Abstract;
using flameborn.Sdk.Configurations;
using HF.Library.Utilities.Singleton;
using HF.Logger;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.Events;

namespace flameborn.Sdk.Managers
{
    /// <summary>
    /// Manager for handling PlayFab operations.
    /// </summary>
    public class PlayfabManager : MonoBehaviourSingleton<PlayfabManager>, IManager
    {
        #region Fields

        private PlayfabConfiguration configuration = null;
        private UnityEvent<LoginResult> event_LoginCompleted;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether the configuration is loaded.
        /// </summary>
        public bool IsLoaded { get; private set; } = false;

        #endregion

        #region Methods

        /// <summary>
        /// Called on the first frame the script is active.
        /// </summary>
        private void Start()
        {
            GameManager.Instance.SetManager(this);
            FindConfiguration();
        }

        /// <summary>
        /// Finds and sets the PlayFab configuration.
        /// </summary>
        private void FindConfiguration()
        {
            var configurationManager = GameManager.Instance.GetManager<ConfigurationManager>();
            if (configurationManager.IsContain)
            {
                configurationManager.Instance.LoadConfiguration<PlayfabConfiguration>(SetConfigurationFile);
                return;
            }

            Invoke(nameof(FindConfiguration), 2f);
        }

        /// <summary>
        /// Sets the PlayFab configuration file.
        /// </summary>
        /// <param name="configuration">The PlayFab configuration to set.</param>
        private void SetConfigurationFile(PlayfabConfiguration configuration)
        {
            this.configuration = configuration;
            IsLoaded = configuration != null;

            if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
            {
                PlayFabSettings.TitleId = configuration.TitleId;
            }
        }

        /// <summary>
        /// Unlinks the Android device ID.
        /// </summary>
        /// <param name="deviceId">The device ID to unlink.</param>
        /// <param name="listener">The listener to call when the operation is complete.</param>
        public void UnlinkAndroidDeviceId(string deviceId, Action<UnlinkAndroidDeviceIDResult> listener)
        {
#if UNITY_ANDROID
            var request = new UnlinkAndroidDeviceIDRequest
            {
                AndroidDeviceId = deviceId
            };
            PlayFabClientAPI.UnlinkAndroidDeviceID(request, listener, Event_OnError);
#endif
        }

        /// <summary>
        /// Unlinks the iOS device ID.
        /// </summary>
        /// <param name="deviceId">The device ID to unlink.</param>
        /// <param name="listener">The listener to call when the operation is complete.</param>
        public void UnlinkIOSDeviceId(string deviceId, Action<UnlinkIOSDeviceIDResult> listener)
        {
#if UNITY_IOS
            var request = new UnlinkIOSDeviceIDRequest
            {
                DeviceId = deviceId
            };
            PlayFabClientAPI.UnlinkIOSDeviceID(request, listener, Event_OnError);
#endif
        }

        /// <summary>
        /// Handles errors that occur during PlayFab operations.
        /// </summary>
        /// <param name="error">The error that occurred.</param>
        private void Event_OnError(PlayFabError error)
        {
            HFLogger.LogError(error, error.ApiEndpoint, error.GenerateErrorReport());
            var uiManager = GameManager.Instance.GetManager<UiManager>();
            if (uiManager.IsContain)
            {
                uiManager.Instance.alert.Show("Error", error.ErrorMessage);
            }
        }

        #endregion
    }
}
```

## Fields
- **configuration**: An instance of the `PlayfabConfiguration` class representing the PlayFab configuration.
- **event_LoginCompleted**: An event triggered when login is completed.

## Properties
- **IsLoaded**: Gets a value indicating whether the configuration is loaded.

## Methods
### Public Methods
- **Start()**: Called on the first frame the script is active. Sets the manager and attempts to find the PlayFab configuration.
- **UnlinkAndroidDeviceId(string deviceId, Action<UnlinkAndroidDeviceIDResult> listener)**: Unlinks the Android device ID.
- **UnlinkIOSDeviceId(string deviceId, Action<UnlinkIOSDeviceIDResult> listener)**: Unlinks the iOS device ID.

### Private Methods
- **FindConfiguration()**: Finds and sets the PlayFab configuration.
- **SetConfigurationFile(PlayfabConfiguration configuration)**: Sets the PlayFab configuration file.
- **Event_OnError(PlayFabError error)**: Handles errors that occur during PlayFab operations.

## Usage Example
Below is an example of how to use the `PlayfabManager` class in a Unity project.

```csharp
using UnityEngine;

public class ExampleUsage : MonoBehaviour
{
    private void Start()
    {
        PlayfabManager.Instance.UnlinkAndroidDeviceId("device-id", result => 
        {
            Debug.Log("Unlinked Android Device ID");
        });
    }
}
```

## See Also
For more information on the `IManager` interface, refer to the [IManager documentation](https://gkhanc.github.io/flameborn-game/IManager).

## File Location
This class is defined in the `PlayfabManager.cs` file, located in the `flameborn.Sdk.Managers` namespace.
