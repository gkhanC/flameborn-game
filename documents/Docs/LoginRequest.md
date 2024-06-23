
# LoginRequest Class Documentation

## Overview

The `LoginRequest` class represents a request to log in in the Flameborn SDK. This class extends `Request<ILoginResponse>` and implements the `IApiRequest<ILoginResponse>` interface.

### Namespace
```csharp
namespace flameborn.Sdk.Requests.Login
```

## Inheritance
- `Request<ILoginResponse>`
- Implements: `IApiRequest<ILoginResponse>`

## Constructors

### `LoginRequest(IApiController<ILoginResponse> controller)`

Initializes a new instance of the `LoginRequest` class.

#### Syntax
```csharp
public LoginRequest(IApiController<ILoginResponse> controller)
```

#### Parameters
- **controller**: `IApiController<ILoginResponse>` - The API controller used to send the request.

#### Example
```csharp
IApiController<ILoginResponse> controller = new LoginController();
LoginRequest loginRequest = new LoginRequest(controller);
```

## Methods

### `SendRequest`

Sends the request to log in.

#### Syntax
```csharp
public override void SendRequest(out string errorLog, params Action<ILoginResponse>[] listeners);
```

#### Parameters
- **errorLog**: `out string` - The error log to be populated in case of an error.
- **listeners**: `params Action<ILoginResponse>[]` - The listeners to process the response.

#### Example
```csharp
loginRequest.SendRequest(out string errorLog, response => { /* Handle response */ });
```

## References
- [ILoginResponse](https://gkhanc.github.io/flameborn-game/ILoginResponse)
- [IApiController](https://gkhanc.github.io/flameborn-game/IApiController)
