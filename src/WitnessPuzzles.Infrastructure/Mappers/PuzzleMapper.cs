using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WitnessPuzzles.Application.DTOs;
using WitnessPuzzles.Core.Models;
using WitnessPuzzles.Infrastructure.Entities;

namespace WitnessPuzzles.Infrastructure.Mappers;

public static class PuzzleMapper
{
    /// <summary>
    /// Converts a PuzzleEntity object into a PuzzleBlueprint object, preserving the entity's properties and structure.
    /// This method extracts the relevant information from the PuzzleEntity, including its dimensions, colors, nodes, edges, and cells, 
    /// and constructs a corresponding PuzzleBlueprint that can be used to build a playable puzzle grid or for further processing.
    /// </summary>
    /// <param name="entity">The PuzzleEntity object to convert.</param>
    /// <returns>The corresponding PuzzleBlueprint object.</returns>
    public static PuzzleBlueprint ToBlueprint(this PuzzleEntity entity)
    {
        var blueprint = new PuzzleBlueprint
        {
            Id = entity.Id,
            Name = entity.Name,
            Width = entity.Width,
            Height = entity.Height,
            BackgroundColor = entity.BackgroundColor,
            EdgesColor = entity.EdgesColor,
            CellsColor = entity.CellsColor,
            LineInputColor = entity.LineInputColor,
            LineInputSolvedColor = entity.LineInputSolvedColor,
        };

        blueprint.Nodes.AddRange(entity.NodeModifiers.Select(n => new NodeModifierDto(n.X, n.Y, n.IsStart, n.IsEnd, n.HasDot)));
        blueprint.Edges.AddRange(entity.EdgeModifiers.Select(e => new EdgeModifierDto(e.NodeAX, e.NodeAY, e.NodeBX, e.NodeBY, e.HasDot)));
        blueprint.Cells.AddRange(entity.CellModifiers
            .Select(c => new { c.X, c.Y, Symbol = JsonSerializer.Deserialize<CellSymbol>(c.SymbolJson) })
            .Where(c => c.Symbol != null)
            .Select(c => new CellModifierDto(c.X, c.Y, c.Symbol!)));

        return blueprint;
    }

    /// <summary>
    /// Converts a PuzzleBlueprint object into a PuzzleEntity object, preserving the blueprint's properties and structure.
    /// This method extracts the relevant information from the PuzzleBlueprint, including its dimensions, colors, nodes, edges, and cells.
    /// </summary>
    /// <param name="blueprint">The PuzzleBlueprint object to convert.</param>
    /// <returns>The corresponding PuzzleEntity object.</returns>
    public static PuzzleEntity ToEntity(this PuzzleBlueprint blueprint)
    {
        var entity = new PuzzleEntity
        {
            Id = blueprint.Id,
            Name = blueprint.Name,
            Width = blueprint.Width,
            Height = blueprint.Height,
            BackgroundColor = blueprint.BackgroundColor,
            EdgesColor = blueprint.EdgesColor,
            CellsColor = blueprint.CellsColor,
            LineInputColor = blueprint.LineInputColor,
            LineInputSolvedColor = blueprint.LineInputSolvedColor,
        };

        entity.NodeModifiers.AddRange(blueprint.Nodes.Select(n => new NodeModifierEntity { X = n.X, Y = n.Y, IsStart = n.IsStart, IsEnd = n.IsEnd, HasDot = n.HasDot }));
        entity.EdgeModifiers.AddRange(blueprint.Edges.Select(e => new EdgeModifierEntity { NodeAX = e.NodeAX, NodeAY = e.NodeAY, NodeBX = e.NodeBX, NodeBY = e.NodeBY, HasDot = e.HasDot }));
        entity.CellModifiers.AddRange(blueprint.Cells.Select(c => new CellModifierEntity { X = c.X, Y = c.Y, SymbolJson = JsonSerializer.Serialize(c.Symbol) }));

        return entity;
    }
}
