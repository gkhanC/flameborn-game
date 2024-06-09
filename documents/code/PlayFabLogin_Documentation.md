
# PlayFabLogin Documentation

## Overview

The `PlayFabLogin` class handles PlayFab login operations, allowing users to log in using their device ID on either Android or iOS platforms. It implements the `ILogin` interface.

## Namespaces Used

```csharp
using System;
using Flameborn.PlayFab.Abstract;
using PlayFab;
using PlayFab.ClientModels;
```

## Class: `PlayFabLogin`

### Summary
Handles PlayFab login operations.

### Fields

| Field             | Type              | Description                 |
|-------------------|-------------------|-----------------------------|
| `_loginData`      | `PlayFabLoginData`| Login data for PlayFab.     |

### Methods

| Method            | Return Type | Description                                                                         |
|-------------------|-------------|-------------------------------------------------------------------------------------|
| `PlayFabLogin`    | Constructor | Initializes a new instance of the `PlayFabLogin` class with the specified login data.|
| `Login`           | `bool`      | Logs the user in using PlayFab.                                                     |

### Detailed Method Descriptions

#### `PlayFabLogin`

```csharp
public PlayFabLogin(PlayFabLoginData loginData)
```

- **Description**: Initializes a new instance of the `PlayFabLogin` class with the specified login data.
- **Parameters**: 
  - `loginData`: The login data for PlayFab.

#### `Login`

```csharp
public bool Login(out string logMessage)
```

- **Description**: Logs the user in using PlayFab.
- **Parameters**: 
  - `logMessage`: Outputs a log message indicating the result of the login attempt.
- **Returns**: `True` if the login request was sent, otherwise `False`.

- **Platform-Specific Implementations**:
  - **Android**:
    - Uses `LoginWithAndroidDeviceIDRequest` to log in with an Android device ID.
    - Sets `logMessage` to "Logging in with Android."
  - **iOS**:
    - Uses `LoginWithIOSDeviceIDRequest` to log in with an iOS device ID.
    - Sets `logMessage` to "Logging in with iOS."

### Usage Example

```csharp
var loginData = new PlayFabLoginData(true, SystemInfo.deviceUniqueIdentifier, OnLoginSuccess, OnLoginFailure);
var playFabLogin = new PlayFabLogin(loginData);
string logMessage;
if (playFabLogin.Login(out logMessage))
{
    Debug.Log(logMessage);
}
else
{
    Debug.LogError("Login request failed to send.");
}
```

## Platform Directives

The `Login` method contains conditional compilation directives for Unity Android and iOS platforms:

- `UNITY_ANDROID`: Code specific to Android platform login.
- `UNITY_IOS`: Code specific to iOS platform login.

This ensures that the appropriate login request is sent based on the target platform.

## Interfaces Implemented

### `ILogin`

The `PlayFabLogin` class implements the `ILogin` interface, ensuring that it adheres to the contract defined by the interface for login operations.

## Dependencies

- **PlayFab SDK**: Required for making API calls to PlayFab services.
- **Unity**: Conditional compilation is used to target specific platforms (Android and iOS).

This documentation provides a comprehensive overview of the `PlayFabLogin` class, including its purpose, field descriptions, method functionalities, and usage examples.
