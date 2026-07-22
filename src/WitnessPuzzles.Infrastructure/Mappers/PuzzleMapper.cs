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
}
