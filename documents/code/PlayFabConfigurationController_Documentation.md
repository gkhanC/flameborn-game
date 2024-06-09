
# PlayFabConfigurationController Documentation

## Overview

The `PlayFabConfigurationController` class manages the PlayFab configuration. It provides methods to load and save the configuration from/to a file.

## Namespace Used

```csharp
using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
```

## Class: `PlayFabConfigurationController`

### Summary
Controller for managing PlayFab configuration.

### Constructors

| Constructor                                           | Description                                                       |
|-------------------------------------------------------|-------------------------------------------------------------------|
| `PlayFabConfigurationController(IConfiguration configuration)` | Initializes a new instance of the `PlayFabConfigurationController` class with the specified configuration. |

### Methods

| Method                        | Return Type | Description                                                                         |
|-------------------------------|-------------|-------------------------------------------------------------------------------------|
| `LoadConfiguration`           | `bool`      | Loads the PlayFab configuration.                                                    |
| `SaveConfiguration`           | `bool`      | Saves the PlayFab configuration.                                                    |

### Detailed Method Descriptions

#### `PlayFabConfigurationController`

```csharp
public PlayFabConfigurationController(IConfiguration configuration) : base(configuration)
```

- **Description**: Initializes a new instance of the `PlayFabConfigurationController` class with the specified configuration.
- **Parameters**: 
  - `configuration`: The configuration instance.

#### `LoadConfiguration`

```csharp
public override bool LoadConfiguration(out string errorLog)
```

- **Description**: Loads the PlayFab configuration.
- **Parameters**: 
  - `errorLog`: Outputs an error log if the load fails.
- **Returns**: `True` if the configuration was loaded successfully, otherwise `False`.

#### `SaveConfiguration`

```csharp
public override bool SaveConfiguration(out string errorLog, PlayFabConfiguration config)
```

- **Description**: Saves the PlayFab configuration.
- **Parameters**: 
  - `errorLog`: Outputs an error log if the save fails.
  - `config`: The configuration to save.
- **Returns**: `True` if the configuration was saved successfully, otherwise `False`.

### Usage Example

```csharp
var playFabConfig = new PlayFabConfiguration("/path/to/config/file");
var configController = new PlayFabConfigurationController(playFabConfig);
string errorLog;
if (configController.LoadConfiguration(out errorLog))
{
    Debug.Log("Configuration loaded successfully.");
}
else
{
    Debug.LogError(errorLog);
}

if (configController.SaveConfiguration(out errorLog, playFabConfig))
{
    Debug.Log("Configuration saved successfully.");
}
else
{
    Debug.LogError(errorLog);
}
```

This documentation provides a comprehensive overview of the `PlayFabConfigurationController` class, including its purpose, method functionalities, and usage examples.
