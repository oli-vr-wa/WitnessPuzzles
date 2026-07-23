using WitnessPuzzles.Core.Models;

namespace WitnessPuzzles.Infrastructure.Entities;

public class PuzzleEntity
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }

    public required int Width { get; set; }
    public required int Height { get; set; }

    public string BackgroundColor { get; init; } = PuzzleColors.White;
    public string EdgesColor { get; init; } = PuzzleColors.Blue;
    public string CellsColor { get; init; } = PuzzleColors.LightBlue;
    public string LineInputColor { get; init; } = PuzzleColors.White;
    public string LineInputSolvedColor { get; init; } = PuzzleColors.LightBlue;

    // Modifiers
    public List<NodeModifierEntity> NodeModifiers { get; set; } = new();
    public List<EdgeModifierEntity> EdgeModifiers { get; set; } = new();
    public List<CellModifierEntity> CellModifiers { get; set; } = new();
}
