using WitnessPuzzles.Application.DTOs;

namespace WitnessPuzzles.Application.Interfaces;

public interface IPuzzleRepository
{
    /// <summary>
    /// Retrieves a puzzle blueprint by its unique identifier. 
    /// This method fetches the puzzle blueprint from the data source based on the provided ID. 
    /// If a blueprint with the specified ID exists, it returns the corresponding PuzzleBlueprint object; otherwise, it returns null.
    /// </summary>
    /// <param name="id">The unique identifier of the puzzle blueprint.</param>
    /// <returns>The puzzle blueprint with the specified ID, or null if not found.</returns>
    Task<PuzzleBlueprint?> GetBlueprintByIdAsync(int id);
}
