using WitnessPuzzles.Application.DTOs;
using WitnessPuzzles.Core.Models;

namespace WitnessPuzzles.Application.Mappers;

public static class GridMapper
{
    /// <summary>
    /// Converts a PuzzleGrid object into a PuzzleBlueprint object, preserving the grid's properties and structure.
    /// This method extracts the relevant information from the PuzzleGrid, including its dimensions, colors, nodes, edges, 
    /// and cells, and constructs a corresponding PuzzleBlueprint that can be used for storage or further processing.
    /// </summary>
    /// <param name="grid">The PuzzleGrid object to convert.</param>
    /// <param name="name">The name for the resulting PuzzleBlueprint.</param>
    /// <returns>The corresponding PuzzleBlueprint object.</returns>
    public static PuzzleBlueprint ToBlueprint(this PuzzleGrid grid, Guid id, string name)
    {
        int width = grid.Cells.Values.Max(c => c.X) + 1;
        int height = grid.Cells.Values.Max(c => c.Y) + 1;

        var blueprint = new PuzzleBlueprint
        {
            Id = id,
            Name = name,
            Width = width,
            Height = height,
            BackgroundColor = grid.BackgroundColor,
            CellsColor = grid.CellsColor,
            EdgesColor = grid.EdgesColor,
            LineInputColor = grid.LineInputColor,
            LineInputSolvedColor = grid.LineInputSolvedColor
        };

        var nodesWithProperties = grid.Nodes.Values.Where(n => n.IsStart || n.IsEnd || n.HasDot);
        foreach (var node in nodesWithProperties)
        {
            blueprint.Nodes.Add(new NodeModifierDto(node.X, node.Y, node.IsStart, node.IsEnd, node.HasDot));
        }

        var edgesWithProperties = grid.Edges.Values.Where(e => e.HasDot);
        foreach (var edge in edgesWithProperties)
        {
            blueprint.Edges.Add(new EdgeModifierDto(edge.NodeA.X, edge.NodeA.Y, edge.NodeB.X, edge.NodeB.Y, edge.HasDot));
        }

        var cellsWithSymbols = grid.Cells.Values.Where(c => c.Symbol != null);
        foreach (var cell in cellsWithSymbols)
        {
            blueprint.Cells.Add(new CellModifierDto(cell.X, cell.Y, cell.Symbol!));
        }

        return blueprint;
    }
}
