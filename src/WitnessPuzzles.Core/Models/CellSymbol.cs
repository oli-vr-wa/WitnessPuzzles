using System.Text.Json.Serialization;
using WitnessPuzzles.Core.Models.CellSymbols;


namespace WitnessPuzzles.Core.Models;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(SquareSymbol), typeDiscriminator: "square")]
[JsonDerivedType(typeof(StarSymbol), typeDiscriminator: "star")]
[JsonDerivedType(typeof(TetrisSymbol), typeDiscriminator: "tetris")]
public abstract class CellSymbol
{
    public required string ColorHex { get; init; }
}
