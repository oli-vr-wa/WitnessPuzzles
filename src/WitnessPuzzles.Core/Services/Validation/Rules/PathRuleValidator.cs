using WitnessPuzzles.Core.Interfaces.Validation;
using WitnessPuzzles.Core.Models;
using WitnessPuzzles.Core.Models.Validation;

namespace WitnessPuzzles.Core.Services.Validation.Rules;

public class PathRuleValidator : IPathRuleValidator
{
    /// <inheritdoc />
    public PuzzleValidationResult ValidatePath(PuzzleGrid grid, List<int> drawnNodeIds, HashSet<int> drawnEdgeIds)
    {
        var errorsList = new List<string>();

        errorsList.AddRange(ValidateStartEndNodes(grid, drawnNodeIds));

        if (!IsPathCrossingAllDots(grid, drawnNodeIds, drawnEdgeIds))
        {
            errorsList.Add("The path must cross all nodes and edges with dots.");
        }

        return errorsList.Count > 0 ?
            PuzzleValidationResult.Failure(errorsList) :
            PuzzleValidationResult.Success;
    }

    /// <summary>
    /// Validates that the path starts on a start node and ends on an end node.
    /// </summary>
    /// <param name="grid">The puzzle grid to validate.</param>
    /// <param name="drawnNodeIds">The list of drawn node IDs representing the solution.</param>
    /// <returns>A list of error messages if the start or end nodes are invalid; otherwise, an empty list.</returns>
    private List<string> ValidateStartEndNodes(PuzzleGrid grid, List<int> drawnNodeIds)
    {
        var errors = new List<string>();

        var firstNode = grid.Nodes.Values.FirstOrDefault(n => n.Id == drawnNodeIds.First());
        var lastNode = grid.Nodes.Values.FirstOrDefault(n => n.Id == drawnNodeIds.Last());

        if (firstNode == null || lastNode == null)
        {
            errors.Add("The path must start and end on valid nodes.");
            return errors;
        }

        if (!firstNode.IsStart)
        {
            errors.Add($"The path must start on a start node. Node {firstNode.Id} is not a start node.");
        }

        if (!lastNode.IsEnd)
        {
            errors.Add($"The path must end on an end node. Node {lastNode.Id} is not an end node.");
        }

        return errors;
    }

    /// <summary>
    /// Checks if the path crosses all nodes and edges that have dots.
    /// This method ensures that all nodes and edges with dots are included in the drawn path.
    /// </summary>
    /// <param name="grid">The puzzle grid to validate.</param>
    /// <param name="drawnNodeIds">The list of drawn node IDs representing the solution.</param>
    /// <param name="drawnEdgeIds">The set of drawn edge IDs representing the solution.</param>
    /// <returns>True if the path crosses all nodes and edges with dots; otherwise, false.</returns>
    private bool IsPathCrossingAllDots(PuzzleGrid grid, List<int> drawnNodeIds, HashSet<int> drawnEdgeIds)
    {
        // Check if all nodes with dots are included in the drawnNodeIds
        var nodesWithDots = grid.Nodes.Values.Where(n => n.HasDot).Select(n => n.Id);
        if (!nodesWithDots.All(drawnNodeIds.Contains))
        {
            return false;
        }

        // Check if all edges with dots are included in the drawnEdgeIds
        var edgesWithDots = grid.Edges.Values.Where(e => e.HasDot).Select(e => e.Id);
        if (!edgesWithDots.All(drawnEdgeIds.Contains))
        {
            return false;
        }

        return true;
    }
}
