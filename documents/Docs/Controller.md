
# Controller Class Documentation

## Overview
The `Controller` class is an abstract base class for API controllers within the Flameborn SDK. This class defines the structure for sending API requests and handling responses of a specified type. 

## Class Definition

```csharp
using System;
using flameborn.Sdk.Controllers.Abstract;
using flameborn.Sdk.Requests.Abstract;

namespace flameborn.Sdk.Controllers
{
    /// <summary>
    /// Abstract base class for API controllers.
    /// </summary>
    /// <typeparam name="T">The type of response managed by the controller.</typeparam>
    public abstract class Controller<T> : IApiController<T> where T : IApiResponse
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Controller{T}"/> class.
        /// </summary>
        protected Controller()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sends the API request.
        /// </summary>
        /// <param name="errorLog">The error log to be populated in case of an error.</param>
        /// <param name="listeners">The listeners to process the response.</param>
        public abstract void SendRequest(out string errorLog, params Action<T>[] listeners);

        #endregion
    }
}
```

## Methods
### Public Methods
- **SendRequest(out string errorLog, params Action<T>[] listeners)**: Sends the API request.
  - **Parameters**:
    - **errorLog**: The error log to be populated in case of an error.
    - **listeners**: The listeners to process the response.

## Usage Example
Below is an example of how to extend the `Controller` class in a concrete implementation.

```csharp
using System;
using flameborn.Sdk.Controllers;
using flameborn.Sdk.Requests.Abstract;

public class MyApiController : Controller<MyApiResponse>
{
    public override void SendRequest(out string errorLog, params Action<MyApiResponse>[] listeners)
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
For more information on the `IApiController` interface, refer to the [IApiController documentation](https://github.com/gkhanC/flameborn-game/tree/dev/documents/IApiController).

## File Location
This class is defined in the `Controller.cs` file, located in the `flameborn.Sdk.Controllers` namespace.
