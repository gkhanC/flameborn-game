
# IApiController Interface Documentation

## Overview
The `IApiController` interface defines the structure for API controllers that manage sending requests and handling responses within the Flameborn SDK. This interface allows for the implementation of controllers that can send requests and process responses of a specified type.

## Interface Definition

```csharp
using System;
using flameborn.Sdk.Requests.Abstract;

namespace flameborn.Sdk.Controllers.Abstract
{
    /// <summary>
    /// Defines an interface for API controllers that manage sending requests and handling responses.
    /// </summary>
    /// <typeparam name="T">The type of response managed by the controller.</typeparam>
    public interface IApiController<T> where T : IApiResponse
    {
        #region Methods

        /// <summary>
        /// Sends a request and processes it with the specified listeners.
        /// </summary>
        /// <param name="errorLog">The error log to be populated in case of an error.</param>
        /// <param name="listeners">The listeners to process the response.</param>
        void SendRequest(out string errorLog, params Action<T>[] listeners);

        #endregion
    }
}
```

## Methods
### Public Methods
- **SendRequest(out string errorLog, params Action<T>[] listeners)**: Sends a request and processes it with the specified listeners.
  - **Parameters**:
    - **errorLog**: The error log to be populated in case of an error.
    - **listeners**: The listeners to process the response.

## Usage Example
Below is an example of how to implement the `IApiController` interface in a concrete class.

```csharp
using System;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.Abstract;

public class MyApiController : IApiController<MyApiResponse>
{
    public void SendRequest(out string errorLog, params Action<MyApiResponse>[] listeners)
    {
        errorLog = string.Empty;
        // Implementation of sending the request and processing the response
    }
}

public class MyApiResponse : IApiResponse
{
    // Implementation of the response
}
```

## See Also
For more information on the `IApiResponse` interface, refer to the [IApiResponse documentation](https://github.com/gkhanC/flameborn-game/tree/dev/documents/IApiResponse).

## File Location
This interface is defined in the `IApiController.cs` file, located in the `flameborn.Sdk.Controllers.Abstract` namespace.
