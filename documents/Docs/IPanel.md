
# IPanel.cs Documentation

## Overview
`IPanel` is a base interface for all UI panels. It combines the capabilities of showing, hiding, and locking UI panels.

## Example Usage

```csharp
public class MyPanel : IPanel
{
    public void Show()
    {
        // Show panel logic
    }

    public void Hide()
    {
        // Hide panel logic
    }

    public void Lock(object lockerObject)
    {
        // Lock panel logic
    }

    public void UnLock(object lockerObject)
    {
        // Unlock panel logic
    }
}
```
