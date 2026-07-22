using WitnessPuzzles.Core.Enums;

namespace WitnessPuzzles.Core.Models;

public class Cell
{
    public required int Id { get; init; }
    public required int X { get; init; }
    public required int Y { get; init; }

    public CellSymbol? Symbol { get; set; }

    public Edge? TopEdge { get; init; }
    public Edge? RightEdge { get; init; }
    public Edge? BottomEdge { get; init; }
    public Edge? LeftEdge { get; init; }
}
