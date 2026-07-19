# Witness Puzzles

A web-based engine for rendering and validating logic puzzles inspired by the game *The Witness*.

## 🏗️ Architecture

This project is split into two distinct layers to separate the visual rendering from the complex validation logic:

* **Backend (`/src`):** A headless rules engine built with .NET 10 Web API. It receives grid states and paths, evaluates them against puzzle rules (e.g., symmetry, color separation, tetris blocks), and returns validation results.
* **Frontend (`/web` - *Planned*):** A React application that renders the puzzles using SVG. SVG provides native DOM events for precise hit-detection on grid nodes and paths.

## 🚀 Getting Started

### Prerequisites
* [.NET 10 SDK](https://dotnet.microsoft.com/download)
* [Node.js](https://nodejs.org/) (For future frontend development)

### Running the Backend Validation API

1. Navigate to the API directory:
   ```bash
   cd src/WitnessPuzzles.Api
   ```

2. Run the application:
    ```bash
    dotnet run
    ```

3. The API will start on `http://localhost:[PORT]`.

### Planned Puzzle Mechanics
* [ ] Basic pathfinding (Start node to End node).
* [ ] Black/White square separation.
* [ ] Essentail waypoints (Hexagon dots).
* [ ] Symmetry lines.
* [ ] Tetris blocks. 
   


