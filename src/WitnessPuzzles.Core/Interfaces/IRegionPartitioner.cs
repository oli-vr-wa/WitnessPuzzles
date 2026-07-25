using WitnessPuzzles.Core.Models;
using WitnessPuzzles.Core.Models.Validation;

namespace WitnessPuzzles.Core.Interfaces;

public interface IRegionPartitioner
{
    /// <summary>
    /// Partitions the puzzle grid into regions based on the drawn edges.
    /// Each region is represented by a PuzzleRegion object containing the cells that belong to that region
    /// </summary>
    /// <param name="grid">The puzzle grid to partition.</param>
    /// <param name="drawnEdgeIds">The set of drawn edge IDs defining the regions.</param>
    /// <returns>A list of PuzzleRegion objects representing the distinct regions of the puzzle grid.</returns>
    List<PuzzleRegion> PartitionIntoRegions(PuzzleGrid grid, HashSet<int> drawnEdgeIds);
}
