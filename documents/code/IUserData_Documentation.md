
# IUserData Documentation

## Overview

The `IUserData` interface defines the contract for user data. It provides properties to access user-related information.

## Namespace Used

```csharp
namespace Flameborn.User
{
```

## Interface: `IUserData`

### Summary
Defines the contract for user data.

### Properties

| Property      | Type     | Description                                          |
|---------------|----------|------------------------------------------------------|
| `IsRegistered`| `bool`   | Gets a value indicating whether the user is registered. |
| `EMail`       | `string` | Gets the user's email address.                        |
| `UserName`    | `string` | Gets the user's username.                             |
| `DeviceId`    | `string` | Gets the user's device ID.                            |
| `LaunchCount` | `int`    | Gets the count of how many times the application has been launched. |

### Usage Example

```csharp
public class UserData : IUserData
{
    public bool IsRegistered { get; private set; }
    public string EMail { get; private set; }
    public string UserName { get; private set; }
    public string DeviceId { get; private set; }
    public int LaunchCount { get; private set; }

    public UserData(bool isRegistered, string email, string userName, string deviceId, int launchCount)
    {
        IsRegistered = isRegistered;
        EMail = email;
        UserName = userName;
        DeviceId = deviceId;
        LaunchCount = launchCount;
    }
}

var userData = new UserData(true, "user@example.com", "username", "device_id", 1);
Console.WriteLine(userData.UserName);
Console.WriteLine(userData.LaunchCount);
```

This documentation provides a comprehensive overview of the `IUserData` interface, including its purpose, property descriptions, and usage example.
