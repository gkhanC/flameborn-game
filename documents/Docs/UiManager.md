
# UiManager.cs Documentation

## Overview
`UiManager` is a singleton class responsible for managing the UI components of the game. It initializes and controls various UI panels and responds to scene load events.

## Public Methods

| Method Name | Description | Parameters | Return Type |
|-------------|-------------|------------|-------------|
| `UiManager()` | Initializes a new instance of the `UiManager` class. | None | Constructor |
| `CloseAll()` | Closes all UI panels. | None | `void` |

## Unity Functions

| Method Name | Description | Parameters | Return Type |
|-------------|-------------|-------------|-------------|
| `Start()` | Called when the script instance is being loaded. | None | `void` |
| `OnSceneLoad(Scene scene, LoadSceneMode mode)` | Called when a scene is loaded. | `Scene scene, LoadSceneMode mode` | `void` |

## Example Usage

```csharp
public class ExampleUsage
{
    public void InitializeUiManager()
    {
        UiManager uiManager = new UiManager();
        uiManager.CloseAll();
        
        SceneManager.LoadScene(1);
    }
}
```
