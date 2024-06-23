
# UpdateStatisticsRequest Class Documentation

## Overview

The `UpdateStatisticsRequest` class represents a request to update statistics in the Flameborn SDK. This class extends `Request<IUpdateStatisticsResponse>` and implements the `IApiRequest<IUpdateStatisticsResponse>` interface.

### Namespace
```csharp
namespace flameborn.Sdk.Requests.Data
```

## Inheritance
- `Request<IUpdateStatisticsResponse>`
- Implements: `IApiRequest<IUpdateStatisticsResponse>`

## Constructors

### `UpdateStatisticsRequest(IApiController<IUpdateStatisticsResponse> controller)`

Initializes a new instance of the `UpdateStatisticsRequest` class.

#### Syntax
```csharp
public UpdateStatisticsRequest(IApiController<IUpdateStatisticsResponse> controller)
```

#### Parameters
- **controller**: `IApiController<IUpdateStatisticsResponse>` - The API controller for the request.

#### Example
```csharp
IApiController<IUpdateStatisticsResponse> controller = new UpdateStatisticsController();
UpdateStatisticsRequest updateStatisticsRequest = new UpdateStatisticsRequest(controller);
```

## Methods

### `SendRequest`

Sends the request to update statistics.

#### Syntax
```csharp
public override void SendRequest(out string errorLog, params Action<IUpdateStatisticsResponse>[] listeners);
```

#### Parameters
- **errorLog**: `out string` - The error log to be populated in case of an error.
- **listeners**: `params Action<IUpdateStatisticsResponse>[]` - The listeners to process the response.

#### Example
```csharp
updateStatisticsRequest.SendRequest(out string errorLog, response => { /* Handle response */ });
```

## References
- [IUpdateStatisticsResponse](https://gkhanc.github.io/flameborn-game/IUpdateStatisticsResponse)
- [IApiController](https://gkhanc.github.io/flameborn-game/IApiController)
