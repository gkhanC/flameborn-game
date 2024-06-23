
# IAccount Interface Documentation

## Overview
The `IAccount` interface represents an account with user data management functionalities within the Flameborn SDK. This interface allows for getting and setting user data associated with the account.

## Interface Definition

```csharp
using flameborn.Core.User;

namespace flameborn.Core.Accounts
{
    /// <summary>
    /// Represents an account interface with user data management functionalities.
    /// </summary>
    public interface IAccount
    {
        /// <summary>
        /// Gets the user data associated with the account.
        /// </summary>
        UserData UserData { get; }

        /// <summary>
        /// Sets the user data for the account.
        /// </summary>
        /// <param name="data">The user data to be set.</param>
        void SetUserData(UserData data);
    }
}
```

## Properties
- **UserData**: Gets the user data associated with the account.

## Methods
### Public Methods
- **SetUserData(UserData data)**: Sets the user data for the account.
  - **Parameters**:
    - **data**: The user data to be set.

## Usage Example
Below is an example of how to implement the `IAccount` interface in a concrete class.

```csharp
using flameborn.Core.User;

public class MyAccount : IAccount
{
    public UserData UserData { get; private set; }

    public void SetUserData(UserData data)
    {
        UserData = data;
    }
}
```

## See Also
For more information on the `UserData` class, refer to the [UserData documentation](https://gkhanc.github.io/flameborn-game/UserData).

## File Location
This interface is defined in the `IAccount.cs` file, located in the `flameborn.Core.Accounts` namespace.
