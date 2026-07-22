
namespace WitnessPuzzles.Infrastructure.Entities;

public class CellModifierEntity
{
    public int X { get; set; }
    public int Y { get; set; }
    public required string SymbolJson { get; set; }
}
