using WitnessPuzzles.Core.Interfaces;
using WitnessPuzzles.Core.Models;
using WitnessPuzzles.Core.Models.Validation;

namespace WitnessPuzzles.Core.Services.Validation;

public class RegionPartitioner : IRegionPartitioner
{
    /// <inheritdoc />
    public List<PuzzleRegion> PartitionIntoRegions(PuzzleGrid grid, HashSet<int> drawnEdgeIds)
    {
        var regions = new List<PuzzleRegion>();
        var visitedCellIds = new HashSet<int>();

        var cellLookup = grid.Cells.Values.ToDictionary(c => (c.X, c.Y));
        var activeWalls = BuildWallLookup(grid, drawnEdgeIds);

        var regionCounter = 1;

        foreach (var cell in grid.Cells.Values)
        {
            if (visitedCellIds.Contains(cell.Id))
                continue;
            
            var region = new PuzzleRegion { RegionId = regionCounter++ };
            var queue = new Queue<Cell>();

            queue.Enqueue(cell);
            visitedCellIds.Add(cell.Id);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                region.Cells.Add(current);

                // Check all orthogonal directions
                var neighborCoords = new[]
                {
                    (X: current.X, Y: current.Y - 1), // Up
                    (X: current.X, Y: current.Y + 1), // Down
                    (X: current.X - 1, Y: current.Y), // Left
                    (X: current.X + 1, Y: current.Y)  // Right
                };

                foreach (var coord in neighborCoords)
                {
                    // Ignore out-of-bounds neighbors
                    if (!cellLookup.TryGetValue(coord, out var neighbor))
                        continue;

                    // Ignore cells already claimed by a region
                    if (visitedCellIds.Contains(neighbor.Id))
                        continue;

                    // Check if there's a wall between the current cell and the neighbor
                    if (IsWallBetween(current, neighbor, activeWalls))
                        continue;

                    visitedCellIds.Add(neighbor.Id);
                    queue.Enqueue(neighbor);
                }
            }

            regions.Add(region);
        }

        return regions;
    }

    /// <summary>
    /// Builds a lookup of walls based on the drawn edges in the puzzle grid.
    /// </summary>
    /// <param name="grid">The puzzle grid containing the cells and edges.</param>
    /// <param name="drawnEdgeIds">The set of drawn edge IDs representing walls.</param>
    /// <returns>A set of wall coordinates for quick lookup.</returns>
    private HashSet<((int x, int y) n1, (int x, int y) n2)> BuildWallLookup(PuzzleGrid grid, HashSet<int> drawnEdgeIds)
    {
        var walls = new HashSet<((int x, int y) n1, (int x, int y) n2)>();

        foreach (var edge in grid.Edges.Values.Where(e => drawnEdgeIds.Contains(e.Id)))
        {
            var coordA = (edge.NodeA.X, edge.NodeA.Y);
            var coordB = (edge.NodeB.X, edge.NodeB.Y);

            // Store both directions to make it easier to check for walls in either direction
            walls.Add((coordA, coordB));
            walls.Add((coordB, coordA));
        }

        return walls;
    }

    /// <summary>
    /// Determines if there is a wall between two adjacent cells based on the active walls in the puzzle grid.
    /// </summary>
    /// <param name="from">The starting cell.</param>
    /// <param name="to">The adjacent cell to check for a wall.</param>
    /// <param name="activeWalls">The set of active walls in the puzzle grid.</param>
    /// <returns>True if there is a wall between the two cells; otherwise, false.</returns>
    private bool IsWallBetween(Cell from, Cell to, HashSet<((int x, int y), (int x, int y))> activeWalls)
    {
        ((int x, int y) nodeA, (int x, int y) nodeB) boundary;

        if (to.X > from.X)
            boundary = ((from.X + 1, from.Y), (from.X + 1, from.Y + 1)); // Right wall
        else if (to.X < from.X)
            boundary = ((from.X, from.Y), (from.X, from.Y + 1)); // Left wall
        else if (to.Y > from.Y)
            boundary = ((from.X, from.Y + 1), (from.X + 1, from.Y + 1)); // Bottom wall
        else if (to.Y < from.Y)
            boundary = ((from.X, from.Y), (from.X + 1, from.Y)); // Top wall
        else
            return false; // Same cell, no wall

        return activeWalls.Contains(boundary);
    }
}
