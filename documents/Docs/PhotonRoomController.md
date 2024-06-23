
# PhotonRoomController Documentation

## Overview

`PhotonRoomController` is a class responsible for managing Photon rooms. It provides methods for creating and joining rooms, as well as checking the master client status.

## Table of Contents
- [PhotonRoomController](#photonroomcontroller)
  - [Methods](#methods)
    - [CreateOrJoinRoom](#createorjoinroom)
    - [IsMasterClient](#ismasterclient)
  - [Usage Example](#usage-example)
  - [Related Documentation](#related-documentation)

## Methods

### CreateOrJoinRoom
Creates or joins a Photon room with the specified parameters.

```csharp
public void CreateOrJoinRoom(string roomName, byte maxPlayers, string userName)
```
**Parameters:**
- `roomName`: The name of the room.
- `maxPlayers`: The maximum number of players allowed in the room.
- `userName`: The user name to set for the player.

### IsMasterClient
Determines whether the local player is the master client.

```csharp
public bool IsMasterClient()
```
**Returns:**
- `true` if the local player is the master client; otherwise, `false`.

## Usage Example

```csharp
PhotonRoomController roomController = new PhotonRoomController();
roomController.CreateOrJoinRoom("room123", 4, "Player1");

if (roomController.IsMasterClient())
{
    Debug.Log("You are the master client.");
}
```

## Related Documentation
- [PhotonManager](https://github.com/gkhanC/flameborn-game/tree/dev/documents/PhotonManager.md)
