using WitnessPuzzles.Core.Interfaces.Validation;
using WitnessPuzzles.Core.Models.CellSymbols;
using WitnessPuzzles.Core.Models.Validation;

namespace WitnessPuzzles.Core.Services.Validation.Rules;

public class SquareRuleValidator : IRegionRuleValidator
{
    /// <inheritdoc />
    public bool CanValidate(PuzzleRegion region) =>
        region.Cells.Any(c => c.Symbol is SquareSymbol);
    
    /// <inheritdoc />
    public PuzzleValidationResult Validate(PuzzleRegion region)
    {
        var squareColors = region.Cells
            .Where(c => c.Symbol is SquareSymbol)
            .Select(c => ((SquareSymbol)c.Symbol!).ColorHex)
            .Distinct()
            .ToList();
            
        if (squareColors.Count <= 1)
        {
            return PuzzleValidationResult.Success;
        }

        return PuzzleValidationResult.Failure(new List<string>
        {
            $"Region {region.RegionId} has squares of multiple colors."
        });
    }
}
