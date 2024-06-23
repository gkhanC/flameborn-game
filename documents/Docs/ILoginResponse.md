
# ILoginResponse Interface Documentation

## Overview

The `ILoginResponse` interface defines the structure for login responses in the Flameborn SDK. This interface extends the `IApiResponse` interface.

### Namespace
```csharp
namespace flameborn.Sdk.Requests.Login.Abstract
```

## Properties

### `IsAccountLogged`

Gets a value indicating whether the account is logged in.

#### Syntax
```csharp
bool IsAccountLogged { get; }
```

### `NewlyCreated`

Gets a value indicating whether the account is newly created.

#### Syntax
```csharp
bool NewlyCreated { get; }
```

### `PlayFabId`

Gets the PlayFab ID associated with the account.

#### Syntax
```csharp
string PlayFabId { get; }
```

## References
- [IApiResponse](https://github.com/gkhanC/flameborn-game/tree/dev/documents/IApiResponse)
