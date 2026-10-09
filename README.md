# Witness Puzzles

A web-based engine for rendering and validating logic puzzles inspired by the game *The Witness*.

## 🏗️ Architecture

This project is split into two distinct layers to separate the visual rendering from the complex validation logic:

- **Backend (`/src`):** A rules engine built with .NET 10 Web API. It serves pre-created puzzle boards from a PostgreSQL database, receives grid states and paths, evaluates them against puzzle rules (e.g., symmetry, color separation, tetris blocks), and returns validation results.
- **Frontend (`/web`):** A React application that retrieves boards from the API and renders the puzzles using SVG. It provides an interactive UI utilizing native DOM events for precise hit-detection on grid nodes and paths, allowing users to actively solve the boards.

## ✨ Current Features

- **Playable Puzzles:** Successfully retrieves and displays pre-created puzzle boards directly from the database.
- **Interactive Solver:** Users can draw paths and solve the puzzles through the web UI, with the backend API handling the strict validation rules.

## 🚀 Getting Started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/)
- PostgreSQL

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

### Running the Frontend

1. Navigate to the web directory:
   ```bash
   cd web
   ```

2. Install dependencies and start the Vite development server:
   ```bash
   npm install
   npm run dev
   ```

## 🗺️ Roadmap & Pending Tasks

### Level Editor & Custom Puzzles
- [ ] **Puzzle Builder Tool:** Implement an interactive tool to design and build custom puzzles directly in the UI.
- [ ] **Save Functionality:** Allow users to save their custom-built puzzles to the database.
- [ ] **Play Saved Puzzles:** Create a browser or library view to load and play user-generated custom puzzles.

### Planned Puzzle Mechanics
- [x] Basic pathfinding (Start node to End node).
- [x] Black/White square separation.
- [x] Essential waypoints (Hexagon dots).
- [x] Symmetry lines.
- [ ] Tetris blocks.