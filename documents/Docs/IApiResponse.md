
# IApiResponse Interface Documentation

## Overview

The `IApiResponse` interface defines the structure for API responses in the Flameborn SDK. This interface is used to encapsulate the details of a response from an API request.

### Namespace
```csharp
namespace flameborn.Sdk.Requests.Abstract
```

## Properties

### `IsRequestSuccess`

Indicates whether the request was successful.

#### Syntax
```csharp
bool IsRequestSuccess { get; }
```

### `Message`

Gets the response message.

#### Syntax
```csharp
string Message { get; }
```

### `Response`

Gets the response object.

#### Syntax
```csharp
object Response { get; }
```

### `ResponseType`

Gets the type of the response.

#### Syntax
```csharp
Type ResponseType { get; }
```

## Methods

### `SetResponse`

Sets the response details.

#### Syntax
```csharp
void SetResponse<T>(bool isSuccess, T response, string message = "");
```

#### Parameters
- **isSuccess**: `bool` - Indicates if the response is successful.
- **response**: `T` - The response object.
- **message**: `string` - The message associated with the response.

#### Example
```csharp
public class MyResponse : IApiResponse
{
    public bool IsRequestSuccess { get; private set; }
    public string Message { get; private set; }
    public object Response { get; private set; }
    public Type ResponseType { get; private set; }

    public void SetResponse<T>(bool isSuccess, T response, string message = "")
    {
        IsRequestSuccess = isSuccess;
        ResponseType = typeof(T);
        Response = response;
        Message = message;
    }
}
```

## References
- [IApiRequest](https://github.com/gkhanC/flameborn-game/tree/dev/documents/IApiRequest)
- [Request](https://github.com/gkhanC/flameborn-game/tree/dev/documents/Request)
