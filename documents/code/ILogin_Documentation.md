
# ILogin Documentation

## Overview

The `ILogin` interface defines a contract for handling login operations. Any class that implements this interface must provide an implementation for the `Login` method.

## Namespace Used

```csharp
namespace Flameborn.PlayFab.Abstract
{
```

## Interface: `ILogin`

### Summary
Defines a contract for handling login operations.

### Methods

| Method            | Return Type | Description                                                                         |
|-------------------|-------------|-------------------------------------------------------------------------------------|
| `Login`           | `bool`      | Logs the user in. Outputs a log message indicating the result of the login attempt.  |

### Detailed Method Descriptions

#### `Login`

```csharp
bool Login(out string logMessage)
```

- **Description**: Logs the user in.
- **Parameters**: 
  - `logMessage`: Outputs a log message indicating the result of the login attempt.
- **Returns**: `True` if the login was successful, otherwise `False`.

### Usage Example

```csharp
public class CustomLogin : ILogin
{
    public bool Login(out string logMessage)
    {
        // Implement custom login logic here
        logMessage = "Login successful.";
        return true;
    }
}
```

This documentation provides a comprehensive overview of the `ILogin` interface, including its purpose, method description, and usage example.
