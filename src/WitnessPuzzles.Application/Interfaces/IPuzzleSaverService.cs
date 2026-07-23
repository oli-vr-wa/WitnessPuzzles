using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WitnessPuzzles.Core.Models;

namespace WitnessPuzzles.Application.Interfaces;

public interface IPuzzleSaverService
{
    /// <summary>
    /// Saves a PuzzleGrid object to the data source, converting it into a PuzzleBlueprint for storage.
    /// This method takes a PuzzleGrid and a puzzle name, converts the grid into a corresponding PuzzleBlueprint, and saves it to the data source. 
    /// It returns the unique identifier of the saved puzzle blueprint.
    /// </summary>
    /// <param name="grid">The PuzzleGrid object to save.</param>
    /// <param name="puzzleName">The name of the puzzle.</param>
    /// <returns>The unique identifier of the saved puzzle blueprint.</returns>
    Task<Guid> SavePuzzleGridAsync(PuzzleGrid grid, string puzzleName);
}
