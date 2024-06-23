# Flameborn

Flameborn is a multiplayer strategy game developed with Unity. In this project, players will develop their strategies to gain an advantage over their opponents. Strategic moves with different units and resource management will be crucial in the game.

## About the Project

Flameborn aims to offer a unique gaming experience by combining deep strategic elements and multiplayer interaction. The project is still in development, and new features are continuously being added.

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

### API

API keys are not included in the git repository. The API packages are located in the [packages](https://github.com/gkhanC/flameborn-game/tree/dev/packages) directory. You need to configure the APIs with your own keys. For details, please refer to the README.md file under the packages directory.


## Development Log and Game Design Document

You can follow our development process and design details from the links below:

- [My Development Diaries](https://github.com/gkhanC/flameborn-game/blob/master/documents/Docs/diaries/daily-log.md)
- [Game Design Document](https://github.com/gkhanC/flameborn-game/blob/master/documents/Docs/GDD/game-desing-doc-flameborn.md)
- [Architecture Diagram](https://github.com/gkhanC/flameborn-game/blob/dev/images/Architecture%20Diagram.png)

## Contributing

If you want to contribute to the project, please follow these steps:

1. **Fork the repository:** Fork this project on GitHub.
2. **Create a new branch:** Make your changes in a separate branch.
    ```bash
    git checkout -b new-branch-name
    ```
3. **Make your changes:** Develop and update your code.
4. **Commit and push your changes:** Commit your changes and push them to your forked repository.
    ```bash
    git commit -m "Brief description of your changes"
    git push origin new-branch-name
    ```
5. **Create a pull request:** Create a pull request for the original repository on GitHub.

## License

This project is licensed under the GNU General Public License. For more information, please refer to the `LICENSE` file.

## Authors and Acknowledgments

- **Developer:** [Gökhan Tutku Çay](https://github.com/gkhanC)
- Special thanks to all contributors.

---

Stay tuned for more information and updates!