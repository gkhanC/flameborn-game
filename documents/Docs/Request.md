
# Request Class Documentation

## Overview

The `Request<T>` class represents a base class for requests in the Flameborn SDK. This class implements the `IApiRequest<T>` interface.

### Namespace
```csharp
namespace flameborn.Sdk.Requests
```

## Properties

### `Controller`

Gets or sets the API controller for the request.

#### Syntax
```csharp
protected IApiController<T> Controller { get; set; }
```

## Constructors

### `Request(IApiController<T> controller)`

Initializes a new instance of the `Request<T>` class.

#### Syntax
```csharp
protected Request(IApiController<T> controller)
```

#### Parameters
- **controller**: `IApiController<T>` - The API controller for the request.

#### Example
```csharp
public class MyRequest : Request<MyResponse>
{
    public MyRequest(IApiController<MyResponse> controller) : base(controller) { }

    public override void SendRequest(out string errorLog, params Action<MyResponse>[] listeners)
    {
        // Implementation
    }
}
```

## Methods

### `SendRequest`

Sends the request.

#### Syntax
```csharp
public abstract void SendRequest(out string errorLog, params Action<T>[] listeners);
```

#### Parameters
- **errorLog**: `out string` - The error log to be populated in case of an error.
- **listeners**: `params Action<T>[]` - The listeners to process the response.

## References
- [IApiController](https://gkhanc.github.io/flameborn-game/IApiController)
- [IApiRequest](https://gkhanc.github.io/flameborn-game/IApiRequest)
