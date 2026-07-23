using WitnessPuzzles.Application.Interfaces;
using WitnessPuzzles.Application.Mappers;
using WitnessPuzzles.Core.Models;

namespace WitnessPuzzles.Application.Services;

public class PuzzleSaverService(IPuzzleRepository puzzleRepository) : IPuzzleSaverService
{
    private readonly IPuzzleRepository _puzzleRepository = puzzleRepository;

    /// <inheritdoc />
    public async Task<Guid> SavePuzzleGridAsync(PuzzleGrid grid, string puzzleName)
    {
        if (grid == null)        
            throw new ArgumentNullException(nameof(grid), "PuzzleGrid cannot be null.");
        var id = Guid.NewGuid();
        var blueprint = grid.ToBlueprint(id, puzzleName);

        await _puzzleRepository.SaveBlueprintAsync(blueprint);

        return blueprint.Id;
    }
}
