
# PhotonManager Documentation

## Overview

`PhotonManager` is a class responsible for managing Photon networking operations in the game. It handles the connection to Photon servers, room creation and management, and player synchronization. This class ensures seamless multiplayer gameplay by managing player connections and game states.

## Table of Contents
- [PhotonManager](#photonmanager)
  - [Fields](#fields)
  - [Methods](#methods)
    - [Awake](#awake)
    - [Start](#start)
    - [Init](#init)
    - [InitializePlayerList](#initializeplayerlist)
    - [ShowLobby](#showlobby)
    - [ConnectCheck](#connectcheck)
    - [PlayerLeft](#playerleft)
    - [JoinOrCreateRoom](#joinorcreateroom)
    - [ConnectToPhoton](#connecttophoton)
    - [MatchCanceled](#matchcanceled)
    - [LoadMainMenu](#loadmainmenu)
    - [OnConnectedToMaster](#onconnectedtomaster)
    - [OnLeftRoom](#onleftroom)
    - [OnLeftLobby](#onleftlobby)
    - [OnCreatedRoom](#oncreatedroom)
    - [OnCreateRoomFailed](#oncreateroomfailed)
    - [OnJoinedRoom](#onjoinedroom)
    - [OnJoinRoomFailed](#onjoinroomfailed)
    - [OnDisconnected](#ondisconnected)
    - [OnPlayerEnteredRoom](#onplayerenteredroom)
    - [OnPlayerLeftRoom](#onplayerleftroom)
    - [RPC_StartMatch](#rpc_startmatch)
    - [StartMatch](#startmatch)
  - [Usage Example](#usage-example)
  - [Related Documentation](#related-documentation)

## Fields

| Field             | Type                  | Description                                                      |
|-------------------|-----------------------|------------------------------------------------------------------|
| `instance`        | `PhotonManager`       | Singleton instance of `PhotonManager`.                           |
| `isReady`         | `bool`                | Indicates if the manager is ready.                               |
| `isConnecting`    | `bool`                | Indicates if a connection attempt is in progress.                |
| `isMatchStop`     | `bool`                | Indicates if the match has been stopped.                         |
| `isMatchStart`    | `bool`                | Indicates if the match has started.                              |
| `isConnectSuccess`| `bool`                | Indicates if the connection was successful.                      |
| `matchId`         | `string`              | ID of the current match.                                         |
| `userName`        | `string`              | User name of the local player.                                   |
| `roomController`  | `PhotonRoomController`| Controller for managing Photon rooms.                            |
| `uiManager`       | `UiManager`           | Manager for handling UI operations.                              |
| `playersInRoom`   | `List<Player>`        | List of players currently in the room.                           |

## Methods

### Awake
Called when the script instance is being loaded.

```csharp
private void Awake()
```

### Start
Called on the first frame the script is active.

```csharp
private void Start()
```

### Init
Initializes the `PhotonManager` with match ID and user name.

```csharp
public void Init(string matchID, string userName)
```
**Parameters:**
- `matchID`: The match ID.
- `userName`: The user name.

### InitializePlayerList
Initializes the player list.

```csharp
private void InitializePlayerList()
```

### ShowLobby
Shows the lobby if enough players are present.

```csharp
public void ShowLobby()
```

### ConnectCheck
Checks if the connection is successful.

```csharp
public void ConnectCheck()
```

### PlayerLeft
Handles the event when a player leaves the room.

```csharp
private void PlayerLeft(Player otherPlayer)
```
**Parameters:**
- `otherPlayer`: The player who left the room.

### JoinOrCreateRoom
Joins or creates a room.

```csharp
private void JoinOrCreateRoom()
```

### ConnectToPhoton
Connects to Photon.

```csharp
private void ConnectToPhoton()
```

### MatchCanceled
Cancels the match.

```csharp
public void MatchCanceled(string message)
```
**Parameters:**
- `message`: The message to display.

### LoadMainMenu
Loads the main menu scene.

```csharp
public void LoadMainMenu()
```

### OnConnectedToMaster
Called when the connection to the master server is established.

```csharp
public override void OnConnectedToMaster()
```

### OnLeftRoom
Called when the local player leaves the room.

```csharp
public override void OnLeftRoom()
```

### OnLeftLobby
Called when the local player leaves the lobby.

```csharp
public override void OnLeftLobby()
```

### OnCreatedRoom
Called when a room is successfully created.

```csharp
public override void OnCreatedRoom()
```

### OnCreateRoomFailed
Called when a room creation attempt fails.

```csharp
public override void OnCreateRoomFailed(short returnCode, string message)
```

### OnJoinedRoom
Called when the local player successfully joins a room.

```csharp
public override void OnJoinedRoom()
```

### OnJoinRoomFailed
Called when a room join attempt fails.

```csharp
public override void OnJoinRoomFailed(short returnCode, string message)
```

### OnDisconnected
Called when the local player is disconnected from the Photon server.

```csharp
public override void OnDisconnected(DisconnectCause cause)
```

### OnPlayerEnteredRoom
Called when a new player enters the room.

```csharp
public override void OnPlayerEnteredRoom(Player newPlayer)
```

### OnPlayerLeftRoom
Called when a player leaves the room.

```csharp
public override void OnPlayerLeftRoom(Player otherPlayer)
```

### RPC_StartMatch
Remote procedure call to start the match.

```csharp
[PunRPC]
private void RPC_StartMatch()
```

### StartMatch
Starts the match if all players are ready.

```csharp
public void StartMatch()
```

## Usage Example

```csharp
// Initialize PhotonManager
PhotonManager.Instance.Init("match123", "Player1");

// Check if the local player is the master client
if (PhotonManager.Instance.IsMasterClient())
{
    Debug.Log("You are the master client.");
}

// Connect to Photon
PhotonManager.Instance.ConnectToPhoton();
```

## Related Documentation
- [PhotonRoomController](https://github.com/gkhanC/flameborn-game/tree/dev/documents/PhotonRoomController.md)
