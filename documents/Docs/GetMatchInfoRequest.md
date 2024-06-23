
# GetMatchInfoRequest Class Documentation

## Overview

The `GetMatchInfoRequest` class represents a request to get match information in the Flameborn SDK. This class extends `Request<IGetMatchInfoResponse>` and implements the `IApiRequest<IGetMatchInfoResponse>` interface.

### Namespace
```csharp
namespace flameborn.Sdk.Requests.MatchMaking
```

## Inheritance
- `Request<IGetMatchInfoResponse>`
- Implements: `IApiRequest<IGetMatchInfoResponse>`

## Constructors

### `GetMatchInfoRequest(IApiController<IGetMatchInfoResponse> controller)`

Initializes a new instance of the `GetMatchInfoRequest` class.

#### Syntax
```csharp
public GetMatchInfoRequest(IApiController<IGetMatchInfoResponse> controller)
```

#### Parameters
- **controller**: `IApiController<IGetMatchInfoResponse>` - The API controller for the request.

#### Example
```csharp
IApiController<IGetMatchInfoResponse> controller = new GetMatchInfoController();
GetMatchInfoRequest getMatchInfoRequest = new GetMatchInfoRequest(controller);
```

## Methods

### `SendRequest`

Sends the request to get match information.

#### Syntax
```csharp
public override void SendRequest(out string errorLog, params Action<IGetMatchInfoResponse>[] listeners);
```

#### Parameters
- **errorLog**: `out string` - The error log to be populated in case of an error.
- **listeners**: `params Action<IGetMatchInfoResponse>[]` - The listeners to process the response.

#### Example
```csharp
getMatchInfoRequest.SendRequest(out string errorLog, response => { /* Handle response */ });
```

## References
- [IGetMatchInfoResponse](https://gkhanc.github.io/flameborn-game/IGetMatchInfoResponse)
- [IApiController](https://gkhanc.github.io/flameborn-game/IApiController)
