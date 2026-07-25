
namespace WitnessPuzzles.Core.Models.Validation;

public class PuzzleRegion
{
    public int RegionId { get; set; }
    public List<Cell> Cells { get; set; } = new();

    public IEnumerable<Cell> SymbolCells => Cells.Where(c => c.Symbol != null);
}
