## Objectives
-  To develop a functional 2-player multiplayer demo prototype demonstrating network 
synchronization in a 2D interaction arena. 
- To implement client-server architecture using Netcode for GameObjects (NGO) with 
server authority for player actions. 
- To synchronize player movement and interactions across the network in real-time 
with minimal latency and jitter. 
- To demonstrate bidirectional network communication through Server RPCs and 
Client RPCs for game state management. 
- To create a spawn system that assigns unique spawn points to players upon joining the 
game session. (Host Left, Client Right). 
- To ensure efficient state synchronization of player transforms (position, rotation, and 
scale) using NetworkTransform components. 
- To implement networked shooting mechanics where bullet spawning and movement 
are synchronized across all connected clients. (Additional) 
- To handle player ownership correctly, ensuring only the owning client can control 
their respective player character. 
- To document the networking architecture including data flow between clients and 
server for all game interactions. 
- To demonstrate understanding of common networking challenges such as bandwidth 
optimization, ownership management, and synchronization lag mitigation.


## Tools & Technologies: 
- Game Engine: Unity Engine 6.3 
- Networking Framework: Netcode for GameObjects (NGO) 
- Programming Language: C# 
- Input System: Unity Input System (New Input System) 
- Physics: Unity 2D Physics (Rigidbody2D, Network Rigidbody2D)

## Key Features Implemented
- Network-synchronized player movement with smooth horizontal controls 
- 2D Character flipping based on movement direction, synchronized across all clients 
- Synchronized Server authoritative shooting mechanics allowing players to spawn 
and fire bullets 
- Dual spawn point system ensuring players start at opposite sides of the arena (Host = 
Left, Client = Right) 
- Server-authoritative architecture prevents cheating and ensuring consistent game 
state 
- Host/Join connection system with UI-based network initialization 
