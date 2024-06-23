
# LobbyPanel.cs Documentation

## Overview
`LobbyPanel` is a serializable class that represents the lobby panel UI. It provides methods to set player data, initialize animations, and override show and hide behaviors.

## Public Methods

| Method Name | Description | Parameters | Return Type |
|-------------|-------------|------------|-------------|
| `SetPlayerData(List<Player> data)` | Sets the player data in the lobby panel. | `List<Player> data` | `void` |
| `Init()` | Initializes the lobby panel animations. | None | `void` |
| `Show()` | Shows the lobby panel and plays animations. | None | `void` |
| `Hide()` | Hides the lobby panel and stops animations. | None | `void` |

## Example Usage

```csharp
public class ExampleUsage
{
    private LobbyPanel lobbyPanel;

    public void SetupLobbyPanel(List<Player> players)
    {
        lobbyPanel.SetPlayerData(players);
        lobbyPanel.Show();
    }
}
```
