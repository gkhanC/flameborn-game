
# GenericResponseEntity Class Documentation

## Overview

The `GenericResponseEntity<T>` class represents a generic response entity with custom data in the Flameborn SDK. This class implements the `IApiResponse` interface.

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

### `CustomData`

Gets or sets the custom data associated with the response.

#### Syntax
```csharp
public T CustomData { get; set; }
```

### `Message`

Gets or sets the response message.

#### Syntax
```csharp
public string Message { get; set; }
```

## Constructors

### `GenericResponseEntity(T customData)`

Initializes a new instance of the `GenericResponseEntity<T>` class.

#### Syntax
```csharp
public GenericResponseEntity(T customData)
```

#### Parameters
- **customData**: `T` - The custom data associated with the response.

#### Example
```csharp
public class MyGenericResponseEntity : GenericResponseEntity<MyData>
{
    public MyGenericResponseEntity(MyData customData) : base(customData) { }

    public override void SetResponse<V>(bool isSuccess, V response, string message = "")
    {
        IsRequestSuccess = isSuccess;
        ResponseType = typeof(V);
        Response = response;
        Message = message;
    }
}
```

## Methods

### `SetResponse`

Sets the response details.

#### Syntax
```csharp
public abstract void SetResponse<V>(bool isSuccess, V response, string message = "");
```

#### Parameters
- **isSuccess**: `bool` - Indicates if the response is successful.
- **response**: `V` - The response object.
- **message**: `string` - The message associated with the response.

## References
- [ResponseEntity](https://gkhanc.github.io/flameborn-game/ResponseEntity)
- [IApiResponse](https://gkhanc.github.io/flameborn-game/IApiResponse)
