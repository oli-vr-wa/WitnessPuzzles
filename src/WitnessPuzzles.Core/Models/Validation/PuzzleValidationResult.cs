
namespace WitnessPuzzles.Core.Models.Validation;

public record PuzzleValidationResult(bool IsValid, List<string> Errors)
{
    public static PuzzleValidationResult Success => new (true, new());
    public static PuzzleValidationResult Failure(List<string> errors) => new (false, errors);
}
