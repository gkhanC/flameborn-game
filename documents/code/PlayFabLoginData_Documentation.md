
# Flameborn.PlayFab Namespace

## PlayFabLoginData Class

### Summary

The `PlayFabLoginData` class holds data required for PlayFab login operations. It includes properties for account creation, device ID, and actions to perform on login success or failure.

### Properties

| Property Name       | Type                  | Access    | Description                                                                 |
|---------------------|-----------------------|-----------|-----------------------------------------------------------------------------|
| CanCreateAccount    | bool                  | Public    | Indicates whether an account can be created if one does not exist.          |
| DeviceId            | string                | Public    | The device ID used for login.                                               |
| OnLoginSuccess      | Action<LoginResult>   | Public    | Action to perform on successful login.                                      |
| OnLoginFailure      | Action<PlayFabError>  | Public    | Action to perform on login failure.                                         |

### Methods

| Method Name         | Return Type           | Parameters                                                           | Description                                                                                   |
|---------------------|-----------------------|----------------------------------------------------------------------|-----------------------------------------------------------------------------------------------|
| PlayFabLoginData    | - (Constructor)       | bool canCreateAccount, string deviceId, ref Action<LoginResult> onLoginSuccess, ref Action<PlayFabError> onLoginFailure | Initializes a new instance of the `PlayFabLoginData` class with the specified parameters.     |

---

### Detailed Description

- **Namespace**: `Flameborn.PlayFab`
  - The namespace `Flameborn.PlayFab` groups related classes and interfaces together, promoting organized code structure.

### Class: PlayFabLoginData

```csharp
using System;
using PlayFab;
using PlayFab.ClientModels;

namespace Flameborn.PlayFab
{
    /// <summary>
    /// Holds data required for PlayFab login operations.
    /// </summary>
    [Serializable]
    public class PlayFabLoginData
    {
        /// <summary>
        /// Indicates whether an account can be created if one does not exist.
        /// </summary>
        public bool CanCreateAccount { get; set; } = false;

        /// <summary>
        /// The device ID used for login.
        /// </summary>
        public string DeviceId { get; set; } = String.Empty;

        /// <summary>
        /// Action to perform on successful login.
        /// </summary>
        public Action<LoginResult> OnLoginSuccess { get; set; }

        /// <summary>
        /// Action to perform on login failure.
        /// </summary>
        public Action<PlayFabError> OnLoginFailure { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayFabLoginData"/> class with the specified parameters.
        /// </summary>
        /// <param name="canCreateAccount">Indicates whether an account can be created if one does not exist.</param>
        /// <param name="deviceId">The device ID used for login.</param>
        /// <param name="onLoginSuccess">Reference to the action to perform on successful login.</param>
        /// <param name="onLoginFailure">Reference to the action to perform on login failure.</param>
        public PlayFabLoginData(bool canCreateAccount, string deviceId, ref Action<LoginResult> onLoginSuccess, ref Action<PlayFabError> onLoginFailure)
        {
            this.CanCreateAccount = canCreateAccount;
            this.DeviceId = deviceId;
            this.OnLoginSuccess = onLoginSuccess;
            this.OnLoginFailure = onLoginFailure;
        }
    }
}
```

### Class Description

The `PlayFabLoginData` class holds data required for PlayFab login operations. It includes properties for determining if an account can be created, the device ID for login, and actions to perform on login success or failure.

### Property Descriptions

- **CanCreateAccount**: Indicates whether an account can be created if one does not exist.
- **DeviceId**: The device ID used for login.
- **OnLoginSuccess**: Action to perform on successful login.
- **OnLoginFailure**: Action to perform on login failure.

### Method Descriptions

- **PlayFabLoginData**: Initializes a new instance of the `PlayFabLoginData` class with the specified parameters.
