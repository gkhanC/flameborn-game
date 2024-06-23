
# FindMatchResponse Class Documentation

## Overview

The `FindMatchResponse` class represents a response for finding a match in the Flameborn SDK. This class extends `ResponseEntity` and implements the `IFindMatchResponse` interface.

### Namespace
```csharp
namespace flameborn.Sdk.Requests.MatchMaking.Entity
```

## Inheritance
- `ResponseEntity`
- Implements: `IFindMatchResponse`

## Attributes
- `[Serializable]`

## Properties

### `MatchId`

Gets or sets the match ID of the found match.

#### Syntax
```csharp
public string MatchId { get; set; }
```

## Methods

### `SetResponse`

Sets the response details.

#### Syntax
```csharp
public void SetResponse<T>(bool isSuccess, string matchId, T response, string message = "");
```

#### Parameters
- **isSuccess**: `bool` - Indicates if the response is successful.
- **matchId**: `string` - The match ID of the found match.
- **response**: `T` - The response object.
- **message**: `string` - The message associated with the response.

#### Example
```csharp
FindMatchResponse findMatchResponse = new FindMatchResponse();
findMatchResponse.SetResponse(true, "Match123", new object(), "Success");
// Use findMatchResponse in your logic
```

## References
- [IFindMatchResponse](https://gkhanc.github.io/flameborn-game/IFindMatchResponse)
- [ResponseEntity](https://gkhanc.github.io/flameborn-game/ResponseEntity)
