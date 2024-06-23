
# FindMatchRequest Class Documentation

## Overview

The `FindMatchRequest` class represents a request to find a match in the Flameborn SDK. This class extends `Request<IFindMatchResponse>` and implements the `IApiRequest<IFindMatchResponse>` interface.

### Namespace
```csharp
namespace flameborn.Sdk.Requests.MatchMaking
```

## Inheritance
- `Request<IFindMatchResponse>`
- Implements: `IApiRequest<IFindMatchResponse>`

## Constructors

### `FindMatchRequest(IApiController<IFindMatchResponse> controller)`

Initializes a new instance of the `FindMatchRequest` class.

#### Syntax
```csharp
public FindMatchRequest(IApiController<IFindMatchResponse> controller)
```

#### Parameters
- **controller**: `IApiController<IFindMatchResponse>` - The API controller for the request.

#### Example
```csharp
IApiController<IFindMatchResponse> controller = new FindMatchController();
FindMatchRequest findMatchRequest = new FindMatchRequest(controller);
```

## Methods

### `SendRequest`

Sends the request to find a match.

#### Syntax
```csharp
public override void SendRequest(out string errorLog, params Action<IFindMatchResponse>[] listeners);
```

#### Parameters
- **errorLog**: `out string` - The error log to be populated in case of an error.
- **listeners**: `params Action<IFindMatchResponse>[]` - The listeners to process the response.

#### Example
```csharp
findMatchRequest.SendRequest(out string errorLog, response => { /* Handle response */ });
```

## References
- [IFindMatchResponse](https://github.com/gkhanC/flameborn-game/tree/dev/documents/IFindMatchResponse)
- [IApiController](https://github.com/gkhanC/flameborn-game/tree/dev/documents/IApiController)
