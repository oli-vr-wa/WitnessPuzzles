using WitnessPuzzles.Core.Interfaces.Validation;
using WitnessPuzzles.Core.Models.CellSymbols;
using WitnessPuzzles.Core.Models.Validation;

namespace WitnessPuzzles.Core.Services.Validation.Rules;

public class StarRuleValidator : IRegionRuleValidator
{
    /// <inheritdoc />
    public bool CanValidate(PuzzleRegion region) =>
        region.Cells.Any(c => c.Symbol is StarSymbol);

    /// <inheritdoc />
    public PuzzleValidationResult Validate(PuzzleRegion region)
    {
        var symbolColors = region.Cells
            .Where(c => c.Symbol != null)
            .GroupBy(c => c.Symbol!.ColorHex)
            .ToList();

        if (symbolColors.Any(sg => sg.Count() != 2))
        {
            return PuzzleValidationResult.Failure(new List<string>
            {
                $"Region {region.RegionId} has stars and color symbols are not in pairs."
            });
        }
           
         return PuzzleValidationResult.Success;        
    }
}
