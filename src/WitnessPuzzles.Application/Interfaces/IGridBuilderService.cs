using WitnessPuzzles.Core.Models;

namespace WitnessPuzzles.Application.Interfaces;

public interface IGridBuilderService
{
    /// <summary>
    /// Builds a rectangular grid of nodes and edges with the specified width and height.
    /// The grid will have (width + 1) * (height + 1) nodes and width * (height + 1) horizontal edges and (width + 1) * height vertical edges.
    /// The number of cells will be width * height.
    /// </summary>
    /// <param name="width">The number of cells horizontally.</param>
    /// <param name="height">The number of cells vertically.</param>
    /// <returns>A PuzzleGrid representing the rectangular grid.</returns>
    PuzzleGrid BuildRectangularGrid(int width, int height);    
}
