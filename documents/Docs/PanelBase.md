
# PanelBase.cs Documentation

## Overview
`PanelBase` is an abstract class that serves as a base for all UI panels. It provides methods to show, hide, lock, and unlock panels.

## Public Methods

| Method Name | Description | Parameters | Return Type |
|-------------|-------------|------------|-------------|
| `Show()` | Shows the panel if it is not locked. | None | `void` |
| `Hide()` | Hides the panel if it is not locked. | None | `void` |
| `Lock(object lockerObject)` | Locks the panel with the specified object. | `object lockerObject` | `void` |
| `UnLock(object lockerObject)` | Unlocks the panel if the specified object matches the lock object. | `object lockerObject` | `void` |

## Example Usage

```csharp
public class MyPanel : PanelBase
{
    public override void Show()
    {
        // Show panel logic
    }

    public override void Hide()
    {
        // Hide panel logic
    }

    public override void Lock(object lockerObject)
    {
        // Lock panel logic
    }

    public override void UnLock(object lockerObject)
    {
        // Unlock panel logic
    }
}
```
