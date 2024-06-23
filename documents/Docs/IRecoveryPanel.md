
# IRecoveryPanel.cs Documentation

## Overview
`IRecoveryPanel` is an interface that defines a contract for recovery panel classes. It includes properties to validate email and references to associated panels.

## Properties

| Property Name | Description | Type |
|---------------|-------------|------|
| `IsEmailValid` | Indicates whether the email is valid. | `bool` |
| `LoginPanel` | The login panel associated with the recovery panel. | `IPanel` |
| `RegisterPanel` | The register panel associated with the recovery panel. | `IPanel` |

## Example Usage

```csharp
public class MyRecoveryPanel : IRecoveryPanel
{
    public bool IsEmailValid { get; }
    public IPanel LoginPanel { get; }
    public IPanel RegisterPanel { get; }

    // Implement methods from IPanel
    public void Show() { }
    public void Hide() { }
    public void Lock(object lockerObject) { }
    public void UnLock(object lockerObject) { }
}
```
