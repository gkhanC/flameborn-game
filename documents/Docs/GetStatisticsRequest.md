
# GetStatisticsRequest Class Documentation

## Overview

The `GetStatisticsRequest` class represents a request to get statistics in the Flameborn SDK. This class extends `Request<IGetStatisticsResponse>` and implements the `IApiRequest<IGetStatisticsResponse>` interface.

### Namespace
```csharp
namespace flameborn.Sdk.Requests.Login
```

## Inheritance
- `Request<IGetStatisticsResponse>`
- Implements: `IApiRequest<IGetStatisticsResponse>`

## Constructors

### `GetStatisticsRequest(IApiController<IGetStatisticsResponse> controller)`

Initializes a new instance of the `GetStatisticsRequest` class.

#### Syntax
```csharp
public GetStatisticsRequest(IApiController<IGetStatisticsResponse> controller)
```

#### Parameters
- **controller**: `IApiController<IGetStatisticsResponse>` - The API controller for the request.

#### Example
```csharp
IApiController<IGetStatisticsResponse> controller = new GetStatisticsController();
GetStatisticsRequest getStatisticsRequest = new GetStatisticsRequest(controller);
```

## Methods

### `SendRequest`

Sends the request to get statistics.

#### Syntax
```csharp
public override void SendRequest(out string errorLog, params Action<IGetStatisticsResponse>[] listeners);
```

#### Parameters
- **errorLog**: `out string` - The error log to be populated in case of an error.
- **listeners**: `params Action<IGetStatisticsResponse>[]` - The listeners to process the response.

#### Example
```csharp
getStatisticsRequest.SendRequest(out string errorLog, response => { /* Handle response */ });
```

## References
- [IGetStatisticsResponse](https://gkhanc.github.io/flameborn-game/IGetStatisticsResponse)
- [IApiController](https://gkhanc.github.io/flameborn-game/IApiController)
