
# GetStatisticsResponse Class Documentation

## Overview

The `GetStatisticsResponse` class represents the response for getting statistics in the Flameborn SDK. This class extends `ResponseEntity` and implements the `IGetStatisticsResponse` interface.

### Namespace
```csharp
namespace flameborn.Sdk.Requests.Data.Entity
```

## Inheritance
- `ResponseEntity`
- Implements: `IGetStatisticsResponse`

## Properties

### `Statistics`

Gets or sets the dictionary containing statistics data.

#### Syntax
```csharp
public Dictionary<string, int> Statistics { get; set; }
```

## Methods

### `SetResponse`

Sets the response details.

#### Syntax
```csharp
public void SetResponse<T>(bool isSuccess, Dictionary<string, int> statistics, T response, string message = "");
```

#### Parameters
- **isSuccess**: `bool` - Indicates if the response is successful.
- **statistics**: `Dictionary<string, int>` - The dictionary containing statistics data.
- **response**: `T` - The response object.
- **message**: `string` - The message associated with the response.

#### Example
```csharp
GetStatisticsResponse getStatisticsResponse = new GetStatisticsResponse();
getStatisticsResponse.SetResponse(true, new Dictionary<string, int>(), new object(), "Success");
// Use getStatisticsResponse in your logic
```

## References
- [IGetStatisticsResponse](https://github.com/gkhanC/flameborn-game/tree/dev/documents/IGetStatisticsResponse)
- [ResponseEntity](https://github.com/gkhanC/flameborn-game/tree/dev/documents/ResponseEntity)
