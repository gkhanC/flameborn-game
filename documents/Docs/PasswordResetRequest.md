
# PasswordResetRequest Class Documentation

## Overview

The `PasswordResetRequest` class represents a request to reset a password in the Flameborn SDK. This class extends `Request<IPasswordResetResponse>` and implements the `IApiRequest<IPasswordResetResponse>` interface.

### Namespace
```csharp
namespace flameborn.Sdk.Requests.Login
```

## Inheritance
- `Request<IPasswordResetResponse>`
- Implements: `IApiRequest<IPasswordResetResponse>`

## Constructors

### `PasswordResetRequest(IApiController<IPasswordResetResponse> controller)`

Initializes a new instance of the `PasswordResetRequest` class.

#### Syntax
```csharp
public PasswordResetRequest(IApiController<IPasswordResetResponse> controller)
```

#### Parameters
- **controller**: `IApiController<IPasswordResetResponse>` - The API controller used to send the request.

#### Example
```csharp
IApiController<IPasswordResetResponse> controller = new PasswordResetController();
PasswordResetRequest passwordResetRequest = new PasswordResetRequest(controller);
```

## Methods

### `SendRequest`

Sends the request to reset a password.

#### Syntax
```csharp
public override void SendRequest(out string errorLog, params Action<IPasswordResetResponse>[] listeners);
```

#### Parameters
- **errorLog**: `out string` - The error log to be populated in case of an error.
- **listeners**: `params Action<IPasswordResetResponse>[]` - The listeners to process the response.

#### Example
```csharp
passwordResetRequest.SendRequest(out string errorLog, response => { /* Handle response */ });
```

## References
- [IPasswordResetResponse](https://gkhanc.github.io/flameborn-game/IPasswordResetResponse)
- [IApiController](https://gkhanc.github.io/flameborn-game/IApiController)
