# TrackLock Codebase Information

## Repository Information
- **Repository Link**: https://github.com/ShreyashRajBamrara/TrackLock.git
- **Primary Branch**: Development is primarily happening on the `main` branch
- **Project Type**: Unity-based C# game project

## Branch Structure
- `main`: Primary development branch
- `feature/grid-system`: Branch for grid implementation and management
- `feature/pathfinding`: Branch for pathfinding algorithms
- `feature/level-design`: Branch for level creation and management

## Important Commits

### Core Systems Implementation
- **Grid System Implementation** (Initial Commit)
  - Implemented the core `GridManager` class
  - Created the node-based grid system
  - Established basic track placement mechanics
  - Set up the foundation for the game's core mechanics

### Pathfinding System
- **Pathfinding Algorithms Integration**
  - Implemented Dijkstra's algorithm for basic pathfinding
  - Added A* algorithm for optimized path calculation
  - Created the `Pathfinder` class with dynamic path recalculation
  - Integrated path visualization system

### Level System
- **Level Design Framework**
  - Created the level management system
  - Implemented level progression mechanics
  - Added level completion conditions
  - Integrated scoring system

### Game Mechanics
- **Track Lever System**
  - Implemented track switching mechanics
  - Added lever interaction system
  - Created dynamic track connection management
  - Integrated collision detection

### Documentation
- **Project Documentation Update**
  - Added comprehensive README.md
  - Created system architecture documentation
  - Added visual flowcharts and diagrams
  - Documented core systems and their interactions

## Development Timeline
1. **Phase 1**: Core Systems (Completed)
   - Grid system implementation
   - Basic pathfinding
   - Node system

2. **Phase 2**: Game Mechanics (In Progress)
   - Track lever system
   - Train movement
   - Level design

3. **Phase 3**: Polish and Features (Planned)
   - UI/UX improvements
   - Audio system
   - Advanced level features

## Technical Stack
- **Game Engine**: Unity
- **Programming Language**: C#
- **Version Control**: Git
- **Documentation**: Markdown
- **Visualization**: Mermaid Diagrams

## Project Structure
```
TrackLock/
├── Assets/
│   ├── My Things/
│   │   ├── Scripts/
│   │   │   ├── GridManager.cs
│   │   │   ├── Node.cs
│   │   │   ├── Pathfinder.cs
│   │   │   └── LevelChanger.cs
│   │   ├── Scenes/
│   │   └── Prefabs/
│   └── Downloaded Assets/
├── Documentation/
│   ├── README.md
│   ├── SYSTEM_ARCHITECTURE.md
│   └── SYSTEM_FLOWCHART.md
└── ProjectSettings/
```

## Current Status
- Core systems are fully implemented and functional
- Game mechanics are in active development
- Documentation is being continuously updated
- Level design and testing are ongoing

## Future Development
- Implementation of advanced level features
- Integration of audio system
- UI/UX improvements
- Performance optimization
- Additional game mechanics 