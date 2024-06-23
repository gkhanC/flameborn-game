
# GameButton.cs Documentation

## Overview
`GameButton` is a serializable class that represents a button in the game UI. It includes properties for the button's name, image, and click event.

## Public Fields

| Field Name | Description | Type |
|------------|-------------|------|
| `btnName` | The name of the button. | `string` |
| `btnImage` | The image of the button. | `Sprite` |
| `ClickEvent` | The event triggered when the button is clicked. | `Action` |

## Constructors

| Constructor | Description | Parameters |
|-------------|-------------|------------|
| `GameButton(string btnName, Sprite buttonImage, params Action[] actions)` | Initializes a new instance of the `GameButton` class. | `btnName`, `buttonImage`, `actions` |

## Example Usage

```csharp
public class ExampleUsage
{
    public void CreateButton()
    {
        Sprite mySprite = // Load your sprite here
        GameButton myButton = new GameButton("Click Me", mySprite, () => Debug.Log("Button Clicked"));
    }
}
```
