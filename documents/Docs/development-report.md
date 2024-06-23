# Development Report

This document is a report on the Flameborn multiplayer game project.

## Skills Demonstrated

The project aims to showcase the following skill sets:

- Working with multiplayer game APIs
- Using Azure Functions and cloud scripts
- Working with PlayFab
- Programming skills
- Ability to design programming architecture
- Proficiency in Unity, game development, and game mechanics
- Competence in working with datasets and tables
- Coding skills
- Ability to document code

## Features of Flameborn

Flameborn includes:

- Login, register, password recovery, and basic CRUD operations on Azure tables via APIs
- API management
- Matchmaking
- Event management
- Player management
- Game management
- Basic in-game functions

## How It Works

1. **Loader Scene**:
    - The application starts with the Loader scene, which communicates with the API via the `IApiRequest` interface injected from the SDK layer. It authenticates the user's account information and player data for use.
    - The `IApiRequest` type uses an injected `IApiController` type, providing an abstraction that supports working with different types of APIs and promotes polymorphism.

2. **Main Menu**:
    - Once data processing in the Loader scene is complete, the UI is set up, and the MainMenu scene is loaded.
    - The MainMenu scene allows players to log in, register, and start a new match.
    - If a player has previously logged in, the application automatically loads the player's account on the next login.

3. **Matchmaking**:
    - When a player wants to start a new match, a ticket is created via the API, and players are matched based on their rating information.
    - Upon successful matching, a match is created, and the match ID is distributed to the players. Players create a room using the match ID through the network API.
    - The network API distributes information such as player readiness and nicknames.
    - Once the information is processed on the network API, the game scene is loaded.

4. **Game Scene**:
    - In the game scene, each player has a campfire and workers.
    - Players start with resources equivalent to their rating points.
    - Players can spend their rating points at the campfire to spawn new workers.
    - Players can select their workers and move them by clicking on empty spots on the screen.
    - Players can move the camera by dragging their fingers on the screen.
    - Players can collect resources with their workers to increase their resources and rating points.

## Planned Features

- Allowing players to attack another player's workers with their own workers
- Allowing players to attack another player's campfire with their workers
- Time limit for matches
- Different types of characters (e.g., Soldier, Priest)
- Adding win/lose events

## Additional Resources

For more detailed information about the project, please refer to the [README](https://github.com/gkhanC/flameborn-game/blob/dev/README.md).

For the development log and daily updates, check out the [Daily-Log](https://github.com/gkhanC/flameborn-game/blob/dev/documents/Docs/diaries/daily-log.md).
