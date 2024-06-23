# Flameborn Project Overview

The Flameborn project consists of two main segments: Core and SDK.

## Core
The Core segment directly handles game and player-related features. It includes in-game functionalities such as:

- Navigating the game scene by touching the screen
- Selecting game characters by tapping
- Commanding game characters by tapping (e.g., walk, gather)

The Core segment ensures that player actions are accurately processed and reflected in the game environment.

## SDK
The SDK provides API support for the Core segment and is designed to work with various APIs through its abstraction structure. Currently, the SDK layer uses PlayFab for user identity registration and authentication, cloud data storage, and data synchronization via Azure tables using cloud scripts and function apps. The SDK ensures accurate processing and synchronization of player data between PlayFab and Azure, and then injects this processed data into the game.

The SDK layer also uses Photon Network API to distribute player information among players. Flameborn supports multiplayer functionality through Photon Network API. Match information created using match-making tickets from PlayFab is integrated with Photon to create rooms and start matches. Photon ensures real-time synchronization of essential character attributes, such as position and animation, among players.

## Game Features

- **Navigation**: Players can navigate the game scene by touching the screen.
- **Character Selection**: Players can select game characters by tapping on them.
- **Commanding Characters**: Players can command their characters to perform actions like walking or gathering resources by tapping on them.
- **Score Update**: Scores obtained by players in a match are updated in the cloud using function apps.
- **Resource Management**: Resources collected in-game are converted to a virtual currency called Rating, which can be spent in-game.
- **Matchmaking**: Rating is also used to match players during matchmaking.

## Multiplayer Functionality

Flameborn uses the Photon Network API to support multiplayer features. Match information generated from PlayFab match-making tickets is used to create rooms and start matches through Photon. Photon handles the real-time synchronization of player data, animations, and other critical character attributes among players.

## Data Synchronization

The SDK ensures seamless data synchronization between PlayFab and Azure, maintaining accurate player data across platforms. This synchronization is crucial for injecting accurate player information into the game.

---

For more detailed information about the project, please refer to the [README](https://github.com/gkhanC/flameborn-game/blob/dev/README.md).

For the development log and daily updates, check out the [Daily-Log](https://github.com/gkhanC/flameborn-game/blob/dev/documents/Docs/diaries/daily-log.md).

- [My Development Diaries](https://github.com/gkhanC/flameborn-game/blob/master/documents/Docs/diaries/daily-log.md)
- [Game Design Document](https://github.com/gkhanC/flameborn-game/blob/master/documents/Docs/GDD/game-desing-doc-flameborn.md)
- [Architecture Diagram](https://github.com/gkhanC/flameborn-game/blob/dev/images/Architecture%20Diagram.png)