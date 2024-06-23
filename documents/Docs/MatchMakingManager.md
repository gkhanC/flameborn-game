
# MatchMakingManager.cs Documentation

## Overview
`MatchMakingManager` is a singleton class that manages matchmaking operations, including creating match tickets, finding matches, and handling match info responses.

## Public Methods

| Method Name | Description | Parameters | Return Type |
|-------------|-------------|------------|-------------|
| `MatchMakingManager()` | Initializes a new instance of the `MatchMakingManager` class. | None | Constructor |
| `NewMatch()` | Initiates a new match if no ticket response is present. | None | `void` |

## Private Methods

| Method Name | Description | Parameters | Return Type |
|-------------|-------------|-------------|-------------|
| `CreateTicket()` | Creates a new matchmaking ticket. | None | `void` |
| `FindMatch()` | Starts the process of finding a match. | None | `void` |
| `GetMatchInfo()` | Retrieves match information. | None | `void` |
| `MatchSearchingCoroutine()` | Coroutine that repeatedly attempts to find a match. | None | `IEnumerator` |
| `OnGetMatchInfoResponse_EventListener(IGetMatchInfoResponse response)` | Event listener for match info response. | `IGetMatchInfoResponse response` | `void` |
| `OnGetFindMatchResponse_EventListener(IFindMatchResponse response)` | Event listener for find match response. | `IFindMatchResponse response` | `void` |
| `OnGetMatchTicketResponse_EventListener(ICreateMatchTicketResponse response)` | Event listener for match ticket response. | `ICreateMatchTicketResponse response` | `void` |

## Example Usage

```csharp
public class ExampleUsage
{
    public void InitializeMatchMakingManager()
    {
        MatchMakingManager matchMakingManager = new MatchMakingManager();
        matchMakingManager.NewMatch();
    }
}
```
