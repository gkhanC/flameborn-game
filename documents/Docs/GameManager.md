
# GameManager.cs Documentation

## Overview
`GameManager` is a singleton class responsible for managing the game lifecycle and other managers within the system. It provides methods to set and get managers, as well as handling matchmaking events.

## Public Methods

| Method Name | Description | Parameters | Return Type |
|-------------|-------------|------------|-------------|
| `GameManager()` | Initializes a new instance of the `GameManager` class. | None | Constructor |
| `GetManager<T>()` | Retrieves a manager of the specified type. | None | `(bool IsContain, T? Instance)` |
| `SetManager<T>(T concrete)` | Sets a manager of the specified type. | `T concrete` | `void` |
| `LoadMainMenuScene()` | Loads the main menu scene. | None | `void` |

## Private Methods

| Method Name | Description | Parameters | Return Type |
|-------------|-------------|-------------|-------------|
| `EventListener_MatchMaking_OnMatchFound(GetMatchmakingTicketResult result)` | Handles the event when a matchmaking ticket result is received and a match is found. | `GetMatchmakingTicketResult result` | `void` |

## Example Usage

```csharp
public class ExampleUsage
{
    public void InitializeGameManager()
    {
        GameManager gameManager = new GameManager();
        gameManager.SetManager(new UiManager());
        
        var (isContain, uiManager) = gameManager.GetManager<UiManager>();
        if (isContain)
        {
            uiManager.mainMenu.Show();
        }

        gameManager.LoadMainMenuScene();
    }
}
```
