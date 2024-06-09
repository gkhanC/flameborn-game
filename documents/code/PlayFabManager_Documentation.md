
# PlayFabManager Documentation

## Overview

The `PlayFabManager` class manages PlayFab-related operations and handles user login functionality within a Unity environment. It ensures that users are authenticated and provides mechanisms for reacting to successful or failed login attempts.

## Namespaces Used

```csharp
using System;
using Flameborn.Configurations;
using global::PlayFab;
using global::PlayFab.ClientModels;
using HF.Extensions;
using HF.Logger;
using UnityEngine;
using UnityEngine.Events;
```

## Class: `PlayFabManager`

### Summary
Manages PlayFab-related operations and handles login functionality.

### Fields

| Field                  | Type                      | Description                                     |
|------------------------|---------------------------|-------------------------------------------------|
| `isLogin`              | `bool`                    | Indicates whether the user is logged in.        |
| `config`               | `PlayFabConfiguration`    | PlayFab configuration instance.                 |
| `onLoginSuccess`       | `Action<LoginResult>`     | Action to perform on successful login.          |
| `onLoginFailure`       | `Action<PlayFabError>`    | Action to perform on login failure.             |
| `Instance`             | `static PlayFabManager`   | Singleton instance of PlayFabManager.           |
| `LoginSuccess`         | `UnityEvent`              | Event triggered on successful login.            |

### Methods

| Method                         | Return Type    | Description                                                                         |
|--------------------------------|----------------|-------------------------------------------------------------------------------------|
| `SubscribeLoginSuccessEvent`   | `void`         | Subscribes to the `LoginSuccess` event.                                             |
| `Awake`                        | `void`         | Called when the script instance is being loaded.                                    |
| `Start`                        | `void`         | Called just before any of the Update methods are called for the first time.         |
| `CheckPlayFabTitleId`          | `void`         | Checks and sets the PlayFab Title ID.                                               |
| `Login`                        | `void`         | Logs the user in using PlayFab.                                                     |
| `OnConfigurationLoaded`        | `void`         | Called when the configuration is loaded.                                            |
| `OnLoginFailure`               | `void`         | Called when login fails.                                                            |
| `OnLoginSuccess`               | `void`         | Called when login succeeds.                                                         |
| `OnEnable`                     | `void`         | Called when the object becomes enabled and active.                                  |

### Detailed Method Descriptions

#### `SubscribeLoginSuccessEvent`

```csharp
public void SubscribeLoginSuccessEvent(UnityAction onLogin)
```

- **Description**: Subscribes to the `LoginSuccess` event.
- **Parameters**: 
  - `onLogin`: The action to perform when login is successful.

#### `Awake`

```csharp
private void Awake()
```

- **Description**: Called when the script instance is being loaded. Ensures the object is not destroyed when loading a new scene.

#### `Start`

```csharp
private void Start()
```

- **Description**: Initializes login actions and subscribes to configuration load event.

#### `CheckPlayFabTitleId`

```csharp
private void CheckPlayFabTitleId(string titleId)
```

- **Description**: Checks and sets the PlayFab Title ID.
- **Parameters**:
  - `titleId`: The PlayFab Title ID.

#### `Login`

```csharp
private void Login(PlayFabConfiguration configuration)
```

- **Description**: Logs the user in using PlayFab.
- **Parameters**:
  - `configuration`: The PlayFab configuration.

#### `OnConfigurationLoaded`

```csharp
public void OnConfigurationLoaded(PlayFabConfiguration configuration)
```

- **Description**: Called when the configuration is loaded.
- **Parameters**:
  - `configuration`: The PlayFab configuration.

#### `OnLoginFailure`

```csharp
private void OnLoginFailure(PlayFabError playFabError)
```

- **Description**: Called when login fails. Logs the error and exits the application.
- **Parameters**:
  - `playFabError`: The PlayFab error that occurred.

#### `OnLoginSuccess`

```csharp
private void OnLoginSuccess(LoginResult loginResult)
```

- **Description**: Called when login succeeds. Logs the success and updates user data if not registered.
- **Parameters**:
  - `loginResult`: The result of the login operation.

#### `OnEnable`

```csharp
private void OnEnable()
```

- **Description**: Called when the object becomes enabled and active. Manages the singleton instance of `PlayFabManager`.

## Event Handling

### `LoginSuccess`

- **Type**: `UnityEvent`
- **Description**: Triggered when the user successfully logs in.

## Usage Example

```csharp
// Subscribing to the LoginSuccess event
PlayFabManager.Instance.SubscribeLoginSuccessEvent(OnUserLoggedIn);

void OnUserLoggedIn()
{
    Debug.Log("User successfully logged in!");
}
```

This documentation provides a comprehensive overview of the `PlayFabManager` class, including its purpose, field descriptions, and method functionalities.
