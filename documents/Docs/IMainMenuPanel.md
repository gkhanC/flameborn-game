
# IMainMenuPanel.cs Documentation

## Overview
`IMainMenuPanel` is an interface that defines a contract for main menu panel classes. It includes properties to reference associated panels and user data.

## Properties

| Property Name | Description | Type |
|---------------|-------------|------|
| `LoginMenu` | The login menu associated with the main menu panel. | `LoginPanel` |
| `ProfileMenu` | The profile menu associated with the main menu panel. | `ProfilePanel` |
| `RecoveryMenu` | The recovery menu associated with the main menu panel. | `RecoveryPanel` |
| `RegisterMenu` | The register menu associated with the main menu panel. | `RegisterPanel` |
| `UserData` | The user data associated with the main menu panel. | `UserData` |

## Example Usage

```csharp
public class MyMainMenuPanel : IMainMenuPanel
{
    public LoginPanel LoginMenu { get; }
    public ProfilePanel ProfileMenu { get; }
    public RecoveryPanel RecoveryMenu { get; }
    public RegisterPanel RegisterMenu { get; }
    public UserData UserData { get; }

    // Implement methods from IPanel
    public void Show() { }
    public void Hide() { }
    public void Lock(object lockerObject) { }
    public void UnLock(object lockerObject) { }
}
```
