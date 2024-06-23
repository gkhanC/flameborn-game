
# IShowable Interface Documentation

## Overview
The `IShowable` interface defines a contract for objects that can be shown within the Flameborn SDK.

## Interface Definition

```csharp
namespace flameborn.Core.Contracts.Abilities
{
    /// <summary>
    /// Defines an interface for objects that can be shown.
    /// </summary>
    public interface IShowAble
    {
        /// <summary>
        /// Shows the object.
        /// </summary>
        void Show();
    }
}
```

## Methods
### Public Methods
- **Show()**: Shows the object.

## Usage Example
Below is an example of how to implement the `IShowable` interface in a concrete class.

```csharp
using flameborn.Core.Contracts.Abilities;

public class MyShowableObject : IShowAble
{
    public void Show()
    {
        // Implementation of showing the object
    }
}
```

## File Location
This interface is defined in the `IShowable.cs` file, located in the `flameborn.Core.Contracts.Abilities` namespace.
