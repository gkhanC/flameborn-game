
# UserData Documentation

## Overview

The `UserData` class represents user data and implements the `IUserData` interface. It includes properties for user registration status, email address, username, device ID, and application launch count.

## Namespace Used

```csharp
using System;
```

## Class: `UserData`

### Summary
Represents user data and implements the `IUserData` interface.

### Properties

| Property      | Type     | Description                                          |
|---------------|----------|------------------------------------------------------|
| `IsRegistered`| `bool`   | Gets or sets a value indicating whether the user is registered. |
| `EMail`       | `string` | Gets or sets the user's email address.               |
| `UserName`    | `string` | Gets or sets the user's username.                    |
| `DeviceId`    | `string` | Gets or sets the user's device ID.                   |
| `LaunchCount` | `int`    | Gets or sets the count of how many times the application has been launched. |

### Usage Example

```csharp
var userData = new UserData
{
    IsRegistered = true,
    EMail = "user@example.com",
    UserName = "username",
    DeviceId = "device_id",
    LaunchCount = 1
};

Console.WriteLine(userData.UserName);
Console.WriteLine(userData.LaunchCount);
```

This documentation provides a comprehensive overview of the `UserData` class, including its purpose, property descriptions, and usage example.
