using WitnessPuzzles.Core.Enums;

namespace WitnessPuzzles.Core.Models.CellSymbols;

public class TetrisSymbol : CellSymbol
{
    public required TetrisShape Shape { get; init; }
    public bool CanRotate { get; init; } = false;
    public SymbolRotation Rotation { get; init; } = SymbolRotation.Degrees0;   
}
