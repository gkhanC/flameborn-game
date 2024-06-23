
# ILobbyPanel.cs Documentation

## Overview
`ILobbyPanel` is an interface that defines a contract for lobby panel classes. It inherits from the `IPanel` interface, combining the capabilities of showing, hiding, and locking UI panels.

## Example Usage

```csharp
public class MyLobbyPanel : ILobbyPanel
{
    // Implement methods from IPanel
    public void Show() { }
    public void Hide() { }
    public void Lock(object lockerObject) { }
    public void UnLock(object lockerObject) { }
}
```
