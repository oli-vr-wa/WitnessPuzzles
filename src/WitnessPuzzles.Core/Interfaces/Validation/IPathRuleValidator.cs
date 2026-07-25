using WitnessPuzzles.Core.Models;
using WitnessPuzzles.Core.Models.Validation;

namespace WitnessPuzzles.Core.Interfaces.Validation;

public interface IPathRuleValidator
{
    /// <summary>
    /// Validates the path rules of a puzzle grid based on the drawn nodes and edges.
    /// The path must start on a start node and finish on an end node, and must cross all the dots in nodes and edges if any are present.
    /// </summary>
    /// <param name="grid">The puzzle grid to validate.</param>
    /// <param name="drawnNodeIds">The list of drawn node IDs representing the solution.</param>
    /// <param name="drawnEdgeIds">The set of drawn edge IDs representing the solution.</param>
    /// <returns>The result of the puzzle validation.</returns>
    PuzzleValidationResult ValidatePath(PuzzleGrid grid, List<int> drawnNodeIds, HashSet<int> drawnEdgeIds);
}
