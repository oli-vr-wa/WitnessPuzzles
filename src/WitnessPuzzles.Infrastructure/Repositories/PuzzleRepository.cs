using WitnessPuzzles.Application.Interfaces;
using WitnessPuzzles.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using WitnessPuzzles.Infrastructure.Mappers;

namespace WitnessPuzzles.Infrastructure.Repositories;

public class PuzzleRepository(PuzzleDbContext context) : IPuzzleRepository
{
    private readonly PuzzleDbContext _context = context;

    /// <inheritdoc />
    public async Task<PuzzleBlueprint?> GetBlueprintByIdAsync(int id)
    {
        var entity = await _context.Puzzles
            .Include(p => p.NodeModifiers)
            .Include(p => p.EdgeModifiers)
            .Include(p => p.CellModifiers)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (entity == null) return null;

        return entity.ToBlueprint();
    }
}
