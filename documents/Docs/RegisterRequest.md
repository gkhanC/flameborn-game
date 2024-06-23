
# RegisterRequest Class Documentation

## Overview

The `RegisterRequest` class represents a request to register a new account in the Flameborn SDK. This class extends `Request<IRegisterResponse>` and implements the `IApiRequest<IRegisterResponse>` interface.

### Namespace
```csharp
namespace flameborn.Sdk.Requests.Login
```

## Inheritance
- `Request<IRegisterResponse>`
- Implements: `IApiRequest<IRegisterResponse>`

## Constructors

### `RegisterRequest(IApiController<IRegisterResponse> controller)`

Initializes a new instance of the `RegisterRequest` class.

#### Syntax
```csharp
public RegisterRequest(IApiController<IRegisterResponse> controller)
```

#### Parameters
- **controller**: `IApiController<IRegisterResponse>` - The API controller used to send the request.

#### Example
```csharp
IApiController<IRegisterResponse> controller = new RegisterController();
RegisterRequest registerRequest = new RegisterRequest(controller);
```

## Methods

### `SendRequest`

Sends the request to register a new account.

#### Syntax
```csharp
public override void SendRequest(out string errorLog, params Action<IRegisterResponse>[] listeners);
```

#### Parameters
- **errorLog**: `out string` - The error log to be populated in case of an error.
- **listeners**: `params Action<IRegisterResponse>[]` - The listeners to process the response.

#### Example
```csharp
registerRequest.SendRequest(out string errorLog, response => { /* Handle response */ });
```

## References
- [IRegisterResponse](https://github.com/gkhanC/flameborn-game/tree/dev/documents/IRegisterResponse)
- [IApiController](https://github.com/gkhanC/flameborn-game/tree/dev/documents/IApiController)
