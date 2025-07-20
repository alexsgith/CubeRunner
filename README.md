# CubeRunner

This project is a simple multiplayer runner prototype built with Unity.  
It features:
- A **local player (PlayerManager)** controlled by input.
- A **remote player (RemotePlayer)** that syncs position, score, and state via `SyncState`.
- Jump and forward movement synchronization using `RemoteDataReceive`.

## Key Scripts
- **PlayerManager.cs** Controls the local player, handles input and state sync.
- **RemotePlayer.cs** Receives and applies state updates for the remote player.
- **BasePlayer.cs** Common player logic like movement, jumping, and collision.
- **RemoteDataReceive.cs** Buffers and applies network state updates.

## Getting Started
1. Open the project in Unity (2021+ recommended).
2. Run the scene and control the local player (mouse click or touch).
3. Observe remote player sync for position, jump, and game states.


**Author:** Alex Savi  
