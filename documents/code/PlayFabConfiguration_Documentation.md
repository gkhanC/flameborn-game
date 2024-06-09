
# PlayFabConfiguration Documentation

## Overview

The `PlayFabConfiguration` class represents the configuration specific to PlayFab. It includes properties for the PlayFab Title ID and user data.

## Namespace Used

```csharp
using System;
using Flameborn.User;
```

## Class: `PlayFabConfiguration`

### Summary
Represents the configuration specific to PlayFab.

### Properties

| Property   | Type       | Description                          |
|------------|------------|--------------------------------------|
| `TitleId`  | `string`   | Gets or sets the Title ID for PlayFab.|
| `UserData` | `UserData` | Gets or sets the user data.          |

### Constructors

| Constructor                                     | Description                                                       |
|-------------------------------------------------|-------------------------------------------------------------------|
| `PlayFabConfiguration(string configurationPath)`| Initializes a new instance of the `PlayFabConfiguration` class with the specified configuration path. |

### Detailed Constructor Descriptions

#### `PlayFabConfiguration`

```csharp
public PlayFabConfiguration(string configurationPath) : base(configurationPath)
```

- **Description**: Initializes a new instance of the `PlayFabConfiguration` class with the specified configuration path.
- **Parameters**: 
  - `configurationPath`: The path to the configuration file.

### Usage Example

```csharp
var playFabConfig = new PlayFabConfiguration("/path/to/config/file");
playFabConfig.TitleId = "YOUR_PLAYFAB_TITLE_ID";
playFabConfig.UserData = new UserData
{
    EMail = "user@example.com",
    UserName = "username",
    DeviceId = "device_id",
    LaunchCount = 1
};
Console.WriteLine(playFabConfig.TitleId);
Console.WriteLine(playFabConfig.UserData.UserName);
```

This documentation provides a comprehensive overview of the `PlayFabConfiguration` class, including its purpose, property descriptions, and usage examples.
