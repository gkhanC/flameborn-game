
# GamePanel.cs Documentation

## Overview
`GamePanel` is a serializable class that represents the main game panel UI. It provides methods to set the campfire object, show and close buttons, and override show and hide behaviors.

## Public Methods

| Method Name | Description | Parameters | Return Type |
|-------------|-------------|------------|-------------|
| `SetCampFire(GameObject camp)` | Sets the campfire object and configures its behavior. | `GameObject camp` | `void` |
| `ShowButtons(params GameButton[] buttonsData)` | Displays the provided buttons on the panel. | `GameButton[] buttonsData` | `void` |
| `CloseButtons()` | Closes all buttons on the panel. | None | `void` |
| `Show()` | Shows the panel and initializes button animations. | None | `void` |
| `Hide()` | Hides the panel and closes buttons. | None | `void` |

## Example Usage

```csharp
public class ExampleUsage
{
    public void SetupGamePanel(GamePanel gamePanel, GameObject campFire)
    {
        gamePanel.SetCampFire(campFire);

        GameButton[] buttons = {
            new GameButton("Action 1", action1Sprite, () => Debug.Log("Action 1")),
            new GameButton("Action 2", action2Sprite, () => Debug.Log("Action 2"))
        };

        gamePanel.ShowButtons(buttons);
    }
}
```
