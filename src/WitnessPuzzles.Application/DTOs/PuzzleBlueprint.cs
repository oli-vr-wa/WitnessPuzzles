using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WitnessPuzzles.Core.Models;

namespace WitnessPuzzles.Application.DTOs;

public class PuzzleBlueprint
{
    public required int Id { get; set; }
    public required string Name { get; set; }

    public required int Width { get; set; }
    public required int Height { get; set; }

    public string BackgroundColor { get; init; } = PuzzleColors.White;
    public string EdgesColor { get; init; } = PuzzleColors.Blue;
    public string CellsColor { get; init; } = PuzzleColors.LightBlue;
    public string LineInputColor { get; init; } = PuzzleColors.White;
    public string LineInputSolvedColor { get; init; } = PuzzleColors.LightBlue;

    public List<NodeModifierDto> Nodes { get; set; } = new();
    public List<EdgeModifierDto> Edges { get; set; } = new();
    public List<CellModifierDto> Cells { get; set; } = new();
}
