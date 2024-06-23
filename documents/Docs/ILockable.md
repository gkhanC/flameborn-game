
# ILockable Interface Documentation

## Overview
The `ILockable` interface defines a contract for objects that can be locked and unlocked within the Flameborn SDK.

## Interface Definition

```csharp
namespace flameborn.Core.Contracts.Abilities
{
    /// <summary>
    /// Defines an interface for objects that can be locked and unlocked.
    /// </summary>
    public interface ILockAble
    {
        /// <summary>
        /// Locks the object with a specified locker.
        /// </summary>
        /// <param name="lockerObject">The object used to lock.</param>
        void Lock(object lockerObject);

        /// <summary>
        /// Unlocks the object with a specified locker.
        /// </summary>
        /// <param name="lockerObject">The object used to unlock.</param>
        void UnLock(object lockerObject);
    }
}
```

## Methods
### Public Methods
- **Lock(object lockerObject)**: Locks the object with a specified locker.
  - **Parameters**:
    - **lockerObject**: The object used to lock.
- **UnLock(object lockerObject)**: Unlocks the object with a specified locker.
  - **Parameters**:
    - **lockerObject**: The object used to unlock.

## Usage Example
Below is an example of how to implement the `ILockable` interface in a concrete class.

```csharp
using flameborn.Core.Contracts.Abilities;

public class MyLockableObject : ILockAble
{
    private bool isLocked = false;
    private object locker;

    public void Lock(object lockerObject)
    {
        if (!isLocked)
        {
            isLocked = true;
            locker = lockerObject;
        }
    }

    public void UnLock(object lockerObject)
    {
        if (isLocked && locker == lockerObject)
        {
            isLocked = false;
            locker = null;
        }
    }
}
```

## File Location
This interface is defined in the `ILockable.cs` file, located in the `flameborn.Core.Contracts.Abilities` namespace.
