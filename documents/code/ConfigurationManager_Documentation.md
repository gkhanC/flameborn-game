
# ConfigurationManager Documentation

## Overview

The `ConfigurationManager` class manages the configuration for the application. It ensures that configurations are loaded and accessible throughout the application.

## Namespace Used

```csharp
using System.IO;
using HF.Extensions;
using HF.Logger;
using UnityEngine;
using UnityEngine.Events;
```

## Class: `ConfigurationManager`

### Summary
Manages the configuration for the application.

### Fields

| Field               | Type                           | Description                                |
|---------------------|--------------------------------|--------------------------------------------|
| `isLoaded`          | `bool`                         | Indicates if the configuration is loaded.  |
| `Instance`          | `static ConfigurationManager`  | Singleton instance of the ConfigurationManager. |
| `playFabConfigurationController` | `PlayFabConfigurationController` | Controller for PlayFab configuration.    |

### Properties

| Property                  | Type                           | Description                            |
|---------------------------|--------------------------------|----------------------------------------|
| `OnConfigurationLoad`     | `UnityEvent<PlayFabConfiguration>` | Event triggered when the configuration is loaded. |

### Methods

| Method                                | Return Type    | Description                                                                         |
|---------------------------------------|----------------|-------------------------------------------------------------------------------------|
| `SubscribeOnConfigurationLoad`        | `void`         | Subscribes to the `OnConfigurationLoad` event.                                      |
| `Awake`                               | `void`         | Called when the script instance is being loaded.                                    |
| `Start`                               | `void`         | Called on the frame when a script is enabled just before any of the Update methods are called the first time.         |
| `OnEnable`                            | `void`         | Called when the object becomes enabled and active.                                  |

### Detailed Method Descriptions

#### `SubscribeOnConfigurationLoad`

```csharp
public void SubscribeOnConfigurationLoad(UnityAction<PlayFabConfiguration> onConfigurationLoad)
```

- **Description**: Subscribes to the `OnConfigurationLoad` event.
- **Parameters**: 
  - `onConfigurationLoad`: The action to perform when the configuration is loaded.

#### `Awake`

```csharp
private void Awake()
```

- **Description**: Called when the script instance is being loaded. Ensures the object is not destroyed when loading a new scene.

#### `Start`

```csharp
public void Start()
```

- **Description**: Initializes the `playFabConfigurationController` and loads the PlayFab configuration.

#### `OnEnable`

```csharp
private void OnEnable()
```

- **Description**: Called when the object becomes enabled and active. Manages the singleton instance of `ConfigurationManager`.

### Usage Example

```csharp
// Subscribing to the OnConfigurationLoad event
ConfigurationManager.Instance.SubscribeOnConfigurationLoad(OnConfigLoaded);

void OnConfigLoaded(PlayFabConfiguration config)
{
    Debug.Log("Configuration loaded: " + config.ConfigurationFilePath);
}
```

This documentation provides a comprehensive overview of the `ConfigurationManager` class, including its purpose, field descriptions, method functionalities, and usage examples.
