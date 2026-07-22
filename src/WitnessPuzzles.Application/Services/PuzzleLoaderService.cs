using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WitnessPuzzles.Application.Interfaces;
using WitnessPuzzles.Core.Models;

namespace WitnessPuzzles.Application.Services;

public class PuzzleLoaderService(
    IPuzzleRepository puzzleRepository,
    IPuzzleConfigurator puzzleConfigurator,
    IGridBuilderService gridBuilder) : IPuzzleLoaderService
{
    private readonly IPuzzleRepository _puzzleRepository = puzzleRepository;
    private readonly IPuzzleConfigurator _puzzleConfigurator = puzzleConfigurator;
    private readonly IGridBuilderService _gridBuilder = gridBuilder;

    /// <inheritdoc />
    public async Task<PuzzleGrid?> LoadPlayablePuzzleAsync(int puzzleId)
    {
        var blueprint = await _puzzleRepository.GetBlueprintByIdAsync(puzzleId);
        if (blueprint == null) return null;

        var grid = _gridBuilder.BuildRectangularGrid(blueprint.Width, blueprint.Height);

        foreach (var node in blueprint.Nodes)
        {
            if (node.IsStart) _puzzleConfigurator.SetStartNode(grid, node.X, node.Y, true);
            if (node.IsEnd) _puzzleConfigurator.SetEndNode(grid, node.X, node.Y, true);
            if (node.HasDot) _puzzleConfigurator.SetNodeDot(grid, node.X, node.Y, true);
        }

        foreach (var edge in blueprint.Edges)
        {
            if (edge.HasDot) _puzzleConfigurator.SetEdgeDot(grid, edge.NodeAX, edge.NodeAY, edge.NodeBX, edge.NodeBY, true);
        }

        foreach (var cell in blueprint.Cells)
        {
            _puzzleConfigurator.SetCellSymbol(grid, cell.X, cell.Y, cell.Symbol);
        }
        
        return grid;
    }
}
