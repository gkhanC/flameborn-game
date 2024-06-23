
# AccountInfoResponse Class Documentation

## Overview

The `AccountInfoResponse` class represents the response for account information in the Flameborn SDK. This class extends `ResponseEntity` and implements the `IAccountInfoResponse` interface.

### Namespace
```csharp
namespace flameborn.Sdk.Requests.Data.Entity
```

## Inheritance
- `ResponseEntity`
- Implements: `IAccountInfoResponse`

## Attributes
- `[Serializable]`

## Properties

### `LaunchCount`

Gets or sets the launch count.

#### Syntax
```csharp
public int LaunchCount { get; set; }
```

### `Rating`

Gets or sets the rating.

#### Syntax
```csharp
public int Rating { get; set; }
```

### `UserName`

Gets or sets the user name.

#### Syntax
```csharp
public string UserName { get; set; }
```

## Methods

### `SetResponse`

Sets the response details.

#### Syntax
```csharp
public void SetResponse<T>(bool isSuccess, string userName, int rating, int launchCount, T response, string message = "");
```

#### Parameters
- **isSuccess**: `bool` - Indicates if the response is successful.
- **userName**: `string` - The user name.
- **rating**: `int` - The rating.
- **launchCount**: `int` - The launch count.
- **response**: `T` - The response object.
- **message**: `string` - The message associated with the response.

#### Example
```csharp
AccountInfoResponse accountInfoResponse = new AccountInfoResponse();
accountInfoResponse.SetResponse(true, "User123", 1500, 10, new object(), "Success");
// Use accountInfoResponse in your logic
```

## References
- [IAccountInfoResponse](https://github.com/gkhanC/flameborn-game/tree/dev/documents/IAccountInfoResponse)
- [ResponseEntity](https://github.com/gkhanC/flameborn-game/tree/dev/documents/ResponseEntity)
