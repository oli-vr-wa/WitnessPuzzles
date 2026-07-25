using WitnessPuzzles.Core.Models.Validation;

namespace WitnessPuzzles.Core.Interfaces.Validation;

public interface IRegionRuleValidator
{
    /// <summary>
    /// A quick check to see if the region can be validated by this validator. 
    /// This is used to avoid running expensive validation logic on regions that don't have the relevant rules.
    /// </summary>
    /// <param name="region">The puzzle region to check.</param>
    /// <returns>True if the region can be validated by this validator; otherwise, false.</returns>
    bool CanValidate(PuzzleRegion region);

    /// <summary>
    /// Validates the rules of a puzzle region based on the cells and their contents.
    /// Apply rules based on the cell symbols and their colors.
    /// </summary>
    /// <param name="region">The puzzle region to validate.</param>
    /// <returns>The result of the puzzle validation.</returns>
    PuzzleValidationResult Validate(PuzzleRegion region);
}
