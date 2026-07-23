using WitnessPuzzles.Core.Models;

namespace WitnessPuzzles.Api.Models;

public record CreatePuzzleRequest(string Name, PuzzleGrid Grid);
