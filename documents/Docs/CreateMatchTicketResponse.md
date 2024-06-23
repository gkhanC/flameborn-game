
# CreateMatchTicketResponse Class Documentation

## Overview

The `CreateMatchTicketResponse` class represents a response for creating a match ticket in the Flameborn SDK. This class extends `ResponseEntity` and implements the `ICreateMatchTicketResponse` interface.

### Namespace
```csharp
namespace flameborn.Sdk.Requests.MatchMaking.Entity
```

## Inheritance
- `ResponseEntity`
- Implements: `ICreateMatchTicketResponse`

## Attributes
- `[Serializable]`

## Properties

### `TicketId`

Gets or sets the ticket ID of the created match ticket.

#### Syntax
```csharp
public string TicketId { get; set; }
```

## Methods

### `SetResponse`

Sets the response details.

#### Syntax
```csharp
public void SetResponse<T>(bool isSuccess, string ticketId, T response, string message = "");
```

#### Parameters
- **isSuccess**: `bool` - Indicates if the response is successful.
- **ticketId**: `string` - The ticket ID of the created match ticket.
- **response**: `T` - The response object.
- **message**: `string` - The message associated with the response.

#### Example
```csharp
CreateMatchTicketResponse createMatchTicketResponse = new CreateMatchTicketResponse();
createMatchTicketResponse.SetResponse(true, "Ticket123", new object(), "Success");
// Use createMatchTicketResponse in your logic
```

## References
- [ICreateMatchTicketResponse](https://github.com/gkhanC/flameborn-game/tree/dev/documents/ICreateMatchTicketResponse)
- [ResponseEntity](https://github.com/gkhanC/flameborn-game/tree/dev/documents/ResponseEntity)
