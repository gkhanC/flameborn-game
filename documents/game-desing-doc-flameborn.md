## Game Design Document: Flameborn

**Platform:** Mobile (iOS, Android)

**Genre:** Real-Time Strategy (RTS), Multiplayer

## Contents {#contents}

1. [Game Overview](#Game-Overview)
2. [Gameplay Mechanics](#Gameplay-Mechanics)
   - [Player Movement](#Player-Movement)
   - [Character Selection](#Character-Selection)
   - [Command System](#Command-System)
3. [Character Roles and Abilities](#Character-Roles-and-Abilities)
   - [Workers](#Workers)
   - [Soldiers](#Woldiers)
4. [Multiplayer Dynamics](#Multiplayer-Dynamics)
5. [Resource Management](#Resource-Management)
6. [User Interface (UI)](#User-Interface-ui)
7. [Monetization Strategy](#Monetization-Strategy)
8. [Technical Specifications](#Technical-Specifications)
9. [Art and Sound Design](#Art-and-Sound-Design)

---

## Game Overview
Flameborn is a real-time multiplayer strategy game where players control a team of characters to gather resources and defend or attack campfires. The goal is to collect wood and use strategic commands to make your campfire bigger than your opponents'.

## Gameplay Mechanics

### Player Movement
- Players can drag the screen to move their view around the map.
- Tap on a character to select it and activate command options.

### Character Selection
- Players can select characters by tapping on them.
- When a character is selected, an action menu with various commands appears.

### Command System 
- **Idle:** The character stands still.
- **Gather Resources:** The character gathers nearby resources (wood).
- **Defense Mode:** (Soldiers only) The character defends the campfire.
- **Attack Mode:** (Soldiers only) The character attacks designated targets.

## Character Roles and Abilities 

### Workers
- **Move:** Walk to a designated location.
- **Gather Resources:** Collect wood from the nearest resource point.
- **Auto-Gather:** Automatically gather resources within a certain range.
- **Call:** Summon all workers to the player's current screen location.

### Soldiers
- **Defend Campfire:** Protect the campfire from enemy attacks.
- **Attack Enemy Character:** Fight with a selected enemy character.
- **Attack Enemy Campfire:** Target and damage the enemy campfire.
- **Move:** Walk to a designated location.
- **Guard Area:** Protect a specific area from enemy entry.

## Multiplayer Dynamics
- **Real-Time Strategy:** Players compete in real-time to grow their campfires by gathering resources and attacking opponents.
- **Resource Competition:** Players must strategically gather wood from shared resource points.
- **Campfire Battle:** The size of the campfire reflects the player's progress and strength. Attacks on enemy campfires reduce their size.

## Resource Management
- **Wood:** The primary resource used to grow the campfire and upgrade units.

### Balance
- When a player attacks the opponent's campfire, their soldier dies, and the opponent's campfire shrinks.
- When a player attacks the opponent's soldier character, both soldiers die.
- When a player attacks the opponent's worker character, the worker dies, and the soldier's health reduces to half of its total health.

### Upgrades
- **Create Worker:** Increase the number of workers to gather resources faster.
- **Create Soldier:** Increase the number of soldiers for defense and attack.

## 6. User Interface (UI) {#user-interface-ui}

### Main Screen
- Drag to move the view around the map.
- Tap to select characters and resources.

### Character Menu
- Appears when a character is selected.
- Includes commands like move, gather, attack, defend, and call.

### Campfire UI
- Shows the size and health of the campfire.
- Displays available upgrades and current resource amount.

## Monetization Strategy

### In-App Purchases
- Cosmetic skins for characters.
- Resource packs to accelerate progress.
- Special abilities or boosts providing temporary advantages.

### Advertisements
- Optional video ads for rewards.
- Banner ads on the main menu screen.

## Technical Specifications
- **Engine:** Unity
- **Supported Devices:** iOS 10.0+ and Android 5.0+
- **Network:** Real-time multiplayer via P2P or dedicated servers
- **Performance:** Optimized for smooth performance on mid-range mobile devices

## Art and Sound Design

### Art Style
- **Visuals:** Stylized, colorful graphics suitable for mobile screens.
- **Characters:** Distinct designs for workers and soldiers.
- **Environment:** Vibrant landscapes filled with resource points and campfires.

### Sound Design
- **Background Music:** Dynamic music that changes based on game state (calm during resource gathering, intense during battles).
- **Sound Effects:** Feedback sounds for commands, resource gathering, and combat.

---

This document serves as a foundational guide for the development of Flameborn, outlining core gameplay mechanics, character functions, multiplayer dynamics, resource management, and other critical elements. More details and iterations will be added as development progresses.
