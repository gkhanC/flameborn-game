
# IAccountInfoResponse Interface Documentation

## Overview

The `IAccountInfoResponse` interface defines the structure for account info responses in the Flameborn SDK. This interface extends the `IApiResponse` interface.

### Namespace
```csharp
namespace flameborn.Sdk.Requests.Data.Abstract
```

## Properties

### `LaunchCount`

Gets the launch count of the account.

#### Syntax
```csharp
int LaunchCount { get; }
```

### `Rating`

Gets the rating of the account.

#### Syntax
```csharp
int Rating { get; }
```

### `UserName`

Gets the user name of the account.

#### Syntax
```csharp
string UserName { get; }
```

## References
- [IApiResponse](https://gkhanc.github.io/flameborn-game/IApiResponse)
