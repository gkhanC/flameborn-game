
# IGetStatisticsResponse Interface Documentation

## Overview

The `IGetStatisticsResponse` interface defines the structure for responses that contain statistics data in the Flameborn SDK. This interface extends the `IApiResponse` interface.

### Namespace
```csharp
namespace flameborn.Sdk.Requests.Data.Abstract
```

## Properties

### `Statistics`

Gets the dictionary containing statistics data.

#### Syntax
```csharp
Dictionary<string, int> Statistics { get; }
```

## References
- [IApiResponse](https://gkhanc.github.io/flameborn-game/IApiResponse)
