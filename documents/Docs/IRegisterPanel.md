
# IRegisterPanel.cs Documentation

## Overview
`IRegisterPanel` is an interface that defines a contract for register panel classes. It includes properties to validate email, password, and username, and references to associated panels.

## Properties

| Property Name | Description | Type |
|---------------|-------------|------|
| `IsEmailValid` | Indicates whether the email is valid. | `bool` |
| `IsPasswordValid` | Indicates whether the password is valid. | `bool` |
| `IsUserNameValid` | Indicates whether the username is valid. | `bool` |
| `LoginPanel` | The login panel associated with the register panel. | `IPanel` |

## Example Usage

```csharp
public class MyRegisterPanel : IRegisterPanel
{
    public bool IsEmailValid { get; }
    public bool IsPasswordValid { get; }
    public bool IsUserNameValid { get; }
    public IPanel LoginPanel { get; }

    // Implement methods from IPanel
    public void Show() { }
    public void Hide() { }
    public void Lock(object lockerObject) { }
    public void UnLock(object lockerObject) { }
}
```
