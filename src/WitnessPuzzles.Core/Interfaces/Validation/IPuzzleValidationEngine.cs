using WitnessPuzzles.Core.Models;
using WitnessPuzzles.Core.Models.Validation;

namespace WitnessPuzzles.Core.Interfaces.Validation;

public interface IPuzzleValidationEngine
{
    /// <summary>
    /// Validates the solution of a puzzle grid based on the drawn nodes and edges.
    /// The validation process includes checking the global path rules, partitioning the grid into regions, and validating the rules of each region.
    /// </summary>
    /// <param name="grid">The puzzle grid to validate.</param>
    /// <param name="drawnNodeIds">The list of drawn node IDs representing the solution.</param>
    /// <returns>The result of the puzzle validation.</returns>
    PuzzleValidationResult ValidateSolution(PuzzleGrid grid, List<int> drawnNodeIds);

    /// <summary>
    /// Validates the global path rules of a puzzle grid based on the drawn nodes and edges.
    /// This method checks if the drawn edges adhere to the puzzle's global path constraints.
    /// </summary>
    /// <param name="grid">The puzzle grid to validate.</param>
    /// <param name="drawnNodeIds">The list of drawn node IDs representing the solution.</param>
    /// <returns>The set of drawn edge IDs corresponding to the drawn nodes.</returns>
    HashSet<int> ConvertNodesToEdges(PuzzleGrid grid, List<int> drawnNodeIds);
}
