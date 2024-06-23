
# Account Class Documentation

## Overview
The `Account` class represents an account with user data management functionalities within the Flameborn SDK. This class implements the `IAccount` interface.

## Class Definition

```csharp
using System;
using flameborn.Core.User;
using Newtonsoft.Json;
using UnityEngine;

namespace flameborn.Core.Accounts
{
    [Serializable]
    public class Account : IAccount
    {
        #region Fields

        [field: SerializeField] private string deviceId = "";
        [field: SerializeField] private string email = "";
        [field: SerializeField] private string password = "";
        [field: SerializeField] private UserData userData = new UserData();

        #endregion

        #region Properties

        [field: SerializeField] public bool IsAccountLoaded { get; set; } = false;
        [field: SerializeField] public bool IsAccountLoggedIn { get; set; } = false;
        public string PlayfabId { get; set; } = string.Empty;

        [JsonProperty("email")]
        public string Email
        {
            get => email;
            set => email = value;
        }

        [JsonProperty("password")]
        public string Password
        {
            get => password;
            set => password = value;
        }

        [JsonProperty("deviceId")]
        public string DeviceId
        {
            get => deviceId;
            set => deviceId = value;
        }

        public UserData UserData
        {
            get => userData;
            set => userData = value;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the user data for the account.
        /// </summary>
        /// <param name="data">The user data to be set.</param>
        public void SetUserData(UserData data)
        {
            userData = data;
        }

        #endregion
    }
}
```

## Fields
- **deviceId**: The device ID associated with the account.
- **email**: The email address associated with the account.
- **password**: The password for the account.
- **userData**: The user data associated with the account.

## Properties
- **IsAccountLoaded**: Indicates whether the account is loaded.
- **IsAccountLoggedIn**: Indicates whether the account is logged in.
- **PlayfabId**: The PlayFab ID associated with the account.
- **Email**: Gets or sets the email address.
- **Password**: Gets or sets the password.
- **DeviceId**: Gets or sets the device ID.
- **UserData**: Gets or sets the user data.

## Methods
### Public Methods
- **SetUserData(UserData data)**: Sets the user data for the account.
  - **Parameters**:
    - **data**: The user data to be set.

## Usage Example
Below is an example of how to use the `Account` class in a Unity project.

```csharp
using flameborn.Core.User;
using flameborn.Core.Accounts;
using UnityEngine;

public class ExampleUsage : MonoBehaviour
{
    private void Start()
    {
        var account = new Account
        {
            Email = "example@example.com",
            Password = "password123",
            DeviceId = "device-id"
        };

        var userData = new UserData
        {
            UserName = "Player1",
            Rating = 100,
            LaunchCount = 5
        };

        account.SetUserData(userData);

        Debug.Log("User Name: " + account.UserData.UserName);
        Debug.Log("Rating: " + account.UserData.Rating);
        Debug.Log("Launch Count: " + account.UserData.LaunchCount);
    }
}
```

## See Also
For more information on the `IAccount` interface, refer to the [IAccount documentation](https://gkhanc.github.io/flameborn-game/IAccount).

## File Location
This class is defined in the `Account.cs` file, located in the `flameborn.Core.Accounts` namespace.
