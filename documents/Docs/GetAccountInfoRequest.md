
# GetAccountInfoRequest Class Documentation

## Overview

The `GetAccountInfoRequest` class represents a request to get account information in the Flameborn SDK. This class extends `Request<IAccountInfoResponse>` and implements the `IApiRequest<IAccountInfoResponse>` interface.

### Namespace
```csharp
namespace flameborn.Sdk.Requests.Data
```

## Inheritance
- `Request<IAccountInfoResponse>`
- Implements: `IApiRequest<IAccountInfoResponse>`

## Constructors

### `GetAccountInfoRequest(IApiController<IAccountInfoResponse> controller)`

Initializes a new instance of the `GetAccountInfoRequest` class.

#### Syntax
```csharp
public GetAccountInfoRequest(IApiController<IAccountInfoResponse> controller)
```

#### Parameters
- **controller**: `IApiController<IAccountInfoResponse>` - The API controller for the request.

#### Example
```csharp
IApiController<IAccountInfoResponse> controller = new GetAccountInfoController();
GetAccountInfoRequest getAccountInfoRequest = new GetAccountInfoRequest(controller);
```

## Methods

### `SendRequest`

Sends the request to get account information.

#### Syntax
```csharp
public override void SendRequest(out string errorLog, params Action<IAccountInfoResponse>[] listeners);
```

#### Parameters
- **errorLog**: `out string` - The error log to be populated in case of an error.
- **listeners**: `params Action<IAccountInfoResponse>[]` - The listeners to process the response.

#### Example
```csharp
getAccountInfoRequest.SendRequest(out string errorLog, response => { /* Handle response */ });
```

## References
- [IAccountInfoResponse](https://gkhanc.github.io/flameborn-game/IAccountInfoResponse)
- [IApiController](https://gkhanc.github.io/flameborn-game/IApiController)
