using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WitnessPuzzles.Core.Interfaces;
using WitnessPuzzles.Core.Interfaces.Validation;
using WitnessPuzzles.Core.Models;
using WitnessPuzzles.Core.Models.Validation;

namespace WitnessPuzzles.Core.Services.Validation;

public class PuzzleValidationEngine(
    IPathRuleValidator pathRuleValidator, 
    IRegionPartitioner regionPartitioner, 
    IEnumerable<IRegionRuleValidator> regionValidators) : IPuzzleValidationEngine
{
    private readonly IPathRuleValidator _pathRuleValidator = pathRuleValidator;
    private readonly IRegionPartitioner _regionPartitioner = regionPartitioner;
    private readonly IEnumerable<IRegionRuleValidator> _regionValidators = regionValidators;


    /// <inheritdoc />
    public PuzzleValidationResult ValidateSolution(PuzzleGrid grid, List<int> drawnNodeIds)
    {
        var drawnEdgeIds = ConvertNodesToEdges(grid, drawnNodeIds);

        var pathResult = _pathRuleValidator.ValidatePath(grid, drawnNodeIds, drawnEdgeIds);
        if (!pathResult.IsValid) return pathResult;

        var regions = _regionPartitioner.PartitionIntoRegions(grid, drawnEdgeIds);

        var errors = new List<string>();

        foreach (var region in regions)
        {
            foreach (var validator in _regionValidators.Where(v => v.CanValidate(region)))
            {
                var validationResult = validator.Validate(region);
                if (!validationResult.IsValid)
                {
                    errors.AddRange(validationResult.Errors);
                }
            }
        }

        return errors.Count > 0 ?
            PuzzleValidationResult.Failure(errors) :
            PuzzleValidationResult.Success;
    }

    /// <inheritdoc />
    public HashSet<int> ConvertNodesToEdges(PuzzleGrid grid, List<int> drawnNodeIds)
    {
        var drawnEdges = new HashSet<int>();
        for (int i = 0; i < drawnNodeIds.Count - 1; i++)
        {
            var nodeAId = drawnNodeIds[i];
            var nodeBId = drawnNodeIds[i + 1];

            var edge = grid.Edges.Values.FirstOrDefault(e => e.IsEdge(nodeAId, nodeBId));
            if (edge != null)
            {
                drawnEdges.Add(edge.Id);
            }
        }
        return drawnEdges;
    }
}

