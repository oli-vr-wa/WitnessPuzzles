using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WitnessPuzzles.Core.Models;

public class PuzzleGrid
{
    public required Dictionary<int, Node> Nodes { get; init; }
    public required Dictionary<int, Edge> Edges { get; init; }
    public required Dictionary<int, Cell> Cells { get; init; }

    public string BackgroundColor { get; init; } = PuzzleColors.White;
    public string EdgesColor { get; init; } = PuzzleColors.Blue;
    public string CellsColor { get; init; } = PuzzleColors.LightBlue;
}
