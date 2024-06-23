
# ILoginPanel.cs Documentation

## Overview
`ILoginPanel` is an interface that defines a contract for login panel classes. It includes properties to validate email and password, and references to associated panels.

## Properties

| Property Name | Description | Type |
|---------------|-------------|------|
| `IsEmailValid` | Indicates whether the email is valid. | `bool` |
| `IsPasswordValid` | Indicates whether the password is valid. | `bool` |
| `RecoveryPanel` | The recovery panel associated with the login panel. | `IPanel` |
| `RegisterPanel` | The register panel associated with the login panel. | `IPanel` |

## Example Usage

```csharp
public class MyLoginPanel : ILoginPanel
{
    public bool IsEmailValid { get; }
    public bool IsPasswordValid { get; }
    public IPanel RecoveryPanel { get; }
    public IPanel RegisterPanel { get; }

    // Implement methods from IPanel
    public void Show() { }
    public void Hide() { }
    public void Lock(object lockerObject) { }
    public void UnLock(object lockerObject) { }
}
```
