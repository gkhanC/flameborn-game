
# ResponseEntity Class Documentation

## Overview

The `ResponseEntity` class represents a base response entity in the Flameborn SDK. This class implements the `IApiResponse` interface.

### Namespace
```csharp
namespace flameborn.Sdk.Requests.Entity
```

## Properties

### `IsRequestSuccess`

Gets or sets a value indicating whether the request was successful.

#### Syntax
```csharp
public bool IsRequestSuccess { get; set; }
```

### `ResponseType`

Gets or sets the type of the response.

#### Syntax
```csharp
public Type ResponseType { get; set; }
```

### `Response`

Gets or sets the response object.

#### Syntax
```csharp
public object Response { get; set; }
```

### `Message`

Gets or sets the response message.

#### Syntax
```csharp
public string Message { get; set; }
```

## Constructors

### `ResponseEntity()`

Initializes a new instance of the `ResponseEntity` class.

#### Syntax
```csharp
protected ResponseEntity()
```

## Methods

### `SetResponse`

Sets the response details.

#### Syntax
```csharp
public virtual void SetResponse<T>(bool isSuccess, T response, string message = "");
```

#### Parameters
- **isSuccess**: `bool` - Indicates if the response is successful.
- **response**: `T` - The response object.
- **message**: `string` - The message associated with the response.

#### Example
```csharp
public class MyResponseEntity : ResponseEntity
{
    public override void SetResponse<T>(bool isSuccess, T response, string message = "")
    {
        IsRequestSuccess = isSuccess;
        ResponseType = typeof(T);
        Response = response;
        Message = message;
    }
}
```

## References
- [IApiResponse](https://gkhanc.github.io/flameborn-game/IApiResponse)
- [GenericResponseEntity](https://gkhanc.github.io/flameborn-game/GenericResponseEntity)
