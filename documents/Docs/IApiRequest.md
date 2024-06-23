
# IApiRequest Interface Documentation

## Overview

The `IApiRequest<T>` interface defines the structure for API requests in the Flameborn SDK. This interface is generic and works with a response type that implements `IApiResponse`.

### Namespace
```csharp
namespace flameborn.Sdk.Requests.Abstract
```

## Methods

### `SendRequest`

Sends a request and processes it with the specified listeners.

#### Syntax
```csharp
void SendRequest(out string errorLog, params Action<T>[] listeners);
```

#### Parameters
- **errorLog**: `out string` - The error log to be populated in case of an error.
- **listeners**: `params Action<T>[]` - The listeners to process the response.

#### Example
```csharp
public class MyRequest : IApiRequest<MyResponse>
{
    public void SendRequest(out string errorLog, params Action<MyResponse>[] listeners)
    {
        // Implementation
    }
}
```

## References
- [IApiResponse](https://github.com/gkhanC/flameborn-game/tree/dev/documents/IApiResponse)
- [IApiController](https://github.com/gkhanC/flameborn-game/tree/dev/documents/IApiController)
