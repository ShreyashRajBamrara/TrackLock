# TrackLock System Flowchart

## System Architecture Flow

```mermaid
graph TD
    A[Game Initialization] --> B[GridManager]
    B --> C[Node System]
    B --> D[Pathfinder]
    
    C --> E[Track Placement]
    C --> F[Obstacle Management]
    
    D --> G[Path Calculation]
    G --> H[Dijkstra's Algorithm]
    G --> I[A* Algorithm]
    
    E --> J[Track Levers]
    F --> K[Dynamic Obstacles]
    
    J --> L[Track Switching]
    K --> M[Collision Detection]
    
    L --> N[Train Movement]
    M --> N
    
    N --> O[Level Completion]
    O --> P[Score Calculation]
    P --> Q[Next Level]
    
    subgraph Core Systems
        B
        C
        D
    end
    
    subgraph Game Mechanics
        E
        F
        J
        K
    end
    
    subgraph Pathfinding
        G
        H
        I
    end
    
    subgraph Game Flow
        L
        M
        N
        O
        P
        Q
    end
```

## Data Flow Diagram

```mermaid
flowchart LR
    A[Player Input] --> B[GridManager]
    B --> C[Node System]
    C --> D[Pathfinder]
    D --> E[Train Controller]
    E --> F[Game State]
    F --> G[UI Update]
    G --> H[Player Feedback]
    
    subgraph Input Layer
        A
    end
    
    subgraph Processing Layer
        B
        C
        D
    end
    
    subgraph Output Layer
        E
        F
        G
        H
    end
```

## Component Interaction

```mermaid
sequenceDiagram
    participant P as Player
    participant GM as GridManager
    participant NS as NodeSystem
    participant PF as Pathfinder
    participant TC as TrainController
    
    P->>GM: Place Track
    GM->>NS: Update Grid
    NS->>PF: Request Path
    PF->>TC: Calculate Route
    TC->>P: Update Display
    
    loop Game Loop
        P->>GM: Interact with Levers
        GM->>NS: Update Connections
        NS->>PF: Recalculate Path
        PF->>TC: Update Train Path
        TC->>P: Show Movement
    end
```

## State Management

```mermaid
stateDiagram-v2
    [*] --> MainMenu
    MainMenu --> LevelSelect
    LevelSelect --> Gameplay
    Gameplay --> Paused
    Paused --> Gameplay
    Gameplay --> LevelComplete
    LevelComplete --> LevelSelect
    LevelComplete --> MainMenu
    Gameplay --> GameOver
    GameOver --> MainMenu
```

These diagrams provide a visual representation of:
1. The overall system architecture and component relationships
2. Data flow through the system
3. Component interactions and sequences
4. Game state management

The diagrams are created using Mermaid, which is supported by GitHub and many other markdown viewers. You can view these diagrams directly on GitHub or any other platform that supports Mermaid syntax. 