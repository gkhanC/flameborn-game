
# IHideable Interface Documentation

## Overview
The `IHideable` interface defines a contract for objects that can be hidden within the Flameborn SDK.

## Interface Definition

```csharp
namespace flameborn.Core.Contracts.Abilities
{
    /// <summary>
    /// Defines an interface for objects that can be hidden.
    /// </summary>
    public interface IHideAble
    {
        /// <summary>
        /// Hides the object.
        /// </summary>
        void Hide();
    }
}
```

## Methods
### Public Methods
- **Hide()**: Hides the object.

## Usage Example
Below is an example of how to implement the `IHideable` interface in a concrete class.

```csharp
using flameborn.Core.Contracts.Abilities;

public class MyHideableObject : IHideAble
{
    public void Hide()
    {
        // Implementation of hiding the object
    }
}
```

## File Location
This interface is defined in the `IHideable.cs` file, located in the `flameborn.Core.Contracts.Abilities` namespace.
