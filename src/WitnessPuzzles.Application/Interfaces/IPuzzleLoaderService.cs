using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WitnessPuzzles.Core.Models;

namespace WitnessPuzzles.Application.Interfaces;

public interface IPuzzleLoaderService
{
    /// <summary>
    /// Loads a playable puzzle grid based on the provided puzzle ID.
    /// This method retrieves the puzzle blueprint associated with the specified ID and constructs a playable puzzle grid from it.
    /// If a playable puzzle grid can be created, it returns the PuzzleGrid object; otherwise, it returns null.
    /// </summary>
    /// <param name="puzzleId">The unique identifier of the puzzle.</param>
    /// <returns>The playable puzzle grid with the specified ID, or null if it cannot be created.</returns>
    Task<PuzzleGrid?> LoadPlayablePuzzleAsync(int puzzleId);
}
