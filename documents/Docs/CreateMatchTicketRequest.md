
# CreateMatchTicketRequest Class Documentation

## Overview

The `CreateMatchTicketRequest` class represents a request to create a match ticket in the Flameborn SDK. This class extends `Request<ICreateMatchTicketResponse>` and implements the `IApiRequest<ICreateMatchTicketResponse>` interface.

### Namespace
```csharp
namespace flameborn.Sdk.Requests.MatchMaking
```

## Inheritance
- `Request<ICreateMatchTicketResponse>`
- Implements: `IApiRequest<ICreateMatchTicketResponse>`

## Constructors

### `CreateMatchTicketRequest(IApiController<ICreateMatchTicketResponse> controller)`

Initializes a new instance of the `CreateMatchTicketRequest` class.

#### Syntax
```csharp
public CreateMatchTicketRequest(IApiController<ICreateMatchTicketResponse> controller)
```

#### Parameters
- **controller**: `IApiController<ICreateMatchTicketResponse>` - The API controller for the request.

#### Example
```csharp
IApiController<ICreateMatchTicketResponse> controller = new CreateMatchTicketController();
CreateMatchTicketRequest createMatchTicketRequest = new CreateMatchTicketRequest(controller);
```

## Methods

### `SendRequest`

Sends the request to create a match ticket.

#### Syntax
```csharp
public override void SendRequest(out string errorLog, params Action<ICreateMatchTicketResponse>[] listeners);
```

#### Parameters
- **errorLog**: `out string` - The error log to be populated in case of an error.
- **listeners**: `params Action<ICreateMatchTicketResponse>[]` - The listeners to process the response.

#### Example
```csharp
createMatchTicketRequest.SendRequest(out string errorLog, response => { /* Handle response */ });
```

## References
- [ICreateMatchTicketResponse](https://gkhanc.github.io/flameborn-game/ICreateMatchTicketResponse)
- [IApiController](https://gkhanc.github.io/flameborn-game/IApiController)
