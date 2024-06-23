
# LoginResponse Class Documentation

## Overview

The `LoginResponse` class represents the response for a login request in the Flameborn SDK. This class extends `ResponseEntity` and implements the `ILoginResponse` interface.

### Namespace
```csharp
namespace flameborn.Sdk.Requests.Login.Entity
```

## Inheritance
- `ResponseEntity`
- Implements: `ILoginResponse`

## Attributes
- `[Serializable]`

## Properties

### `IsAccountLogged`

Gets or sets a value indicating whether the account is logged in.

#### Syntax
```csharp
public bool IsAccountLogged { get; set; }
```

### `NewlyCreated`

Gets or sets a value indicating whether the account is newly created.

#### Syntax
```csharp
public bool NewlyCreated { get; set; }
```

### `PlayFabId`

Gets or sets the PlayFab ID associated with the account.

#### Syntax
```csharp
public string PlayFabId { get; set; }
```

## Constructors

### `LoginResponse()`

Initializes a new instance of the `LoginResponse` class.

#### Syntax
```csharp
public LoginResponse()
```

## Methods

### `SetResponse`

Sets the response details.

#### Syntax
```csharp
public void SetResponse<T>(bool isSuccess, bool isAccountLogged, bool isNewly, string fabId, T response, string message = "");
```

#### Parameters
- **isSuccess**: `bool` - Indicates if the response is successful.
- **isAccountLogged**: `bool` - Indicates if the account is logged in.
- **isNewly**: `bool` - Indicates if the account is newly created.
- **fabId**: `string` - The PlayFab ID associated with the account.
- **response**: `T` - The response object.
- **message**: `string` - The message associated with the response.

#### Example
```csharp
LoginResponse loginResponse = new LoginResponse();
loginResponse.SetResponse(true, true, false, "PlayFab123", new object(), "Success");
// Use loginResponse in your logic
```

## References
- [ILoginResponse](https://github.com/gkhanC/flameborn-game/tree/dev/documents/ILoginResponse)
- [ResponseEntity](https://github.com/gkhanC/flameborn-game/tree/dev/documents/ResponseEntity)
