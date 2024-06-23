
# GetMatchInfoResponse Class Documentation

## Overview

The `GetMatchInfoResponse` class represents a response for getting match information in the Flameborn SDK. This class extends `ResponseEntity` and implements the `IGetMatchInfoResponse` interface.

### Namespace
```csharp
namespace flameborn.Sdk.Requests.MatchMaking.Entity
```

## Inheritance
- `ResponseEntity`
- Implements: `IGetMatchInfoResponse`

## Attributes
- `[Serializable]`

## Properties

### `Players`

Gets or sets the list of player data in the match.

#### Syntax
```csharp
public List<PlayerData> Players { get; set; }
```

## Methods

### `SetResponse`

Sets the response details.

#### Syntax
```csharp
public void SetResponse<T>(bool isSuccess, List<PlayerData> players, T response, string message = "");
```

#### Parameters
- **isSuccess**: `bool` - Indicates if the response is successful.
- **players**: `List<PlayerData>` - The list of player data.
- **response**: `T` - The response object.
- **message**: `string` - The message associated with the response.

#### Example
```csharp
GetMatchInfoResponse getMatchInfoResponse = new GetMatchInfoResponse();
getMatchInfoResponse.SetResponse(true, new List<PlayerData>(), new object(), "Success");
// Use getMatchInfoResponse in your logic
```

## References
- [IGetMatchInfoResponse](https://github.com/gkhanC/flameborn-game/tree/dev/documents/IGetMatchInfoResponse)
- [ResponseEntity](https://github.com/gkhanC/flameborn-game/tree/dev/documents/ResponseEntity)
