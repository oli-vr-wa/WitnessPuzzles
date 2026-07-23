using NSubstitute;
using WitnessPuzzles.Application.DTOs;
using WitnessPuzzles.Application.Interfaces;
using WitnessPuzzles.Application.Services;
using WitnessPuzzles.Core.Models;
using WitnessPuzzles.Core.Models.CellSymbols;

namespace WitnessPuzzles.Tests.Application.Services.PuzzleLoaderServiceTests;

public class LoadPlayablePuzzleAsyncTests
{
    private readonly PuzzleLoaderService _puzzleLoaderService;
    private readonly IPuzzleRepository _puzzleRepository;
    private readonly IPuzzleConfigurator _puzzleConfigurator;
    private readonly IGridBuilderService _gridBuilderService;

    public LoadPlayablePuzzleAsyncTests()
    {
        _puzzleRepository = Substitute.For<IPuzzleRepository>();
        _puzzleConfigurator = new PuzzleConfigurator();
        _gridBuilderService = new GridBuilderService();

        _puzzleLoaderService = new PuzzleLoaderService(_puzzleRepository, _puzzleConfigurator, _gridBuilderService);
    }

    [Fact]
    public async Task ReturnsNull_WhenPuzzleBlueprintIsNull()
    {
        // Arrange
        int puzzleId = 1;
        _puzzleRepository.GetBlueprintByIdAsync(puzzleId).Returns(Task.FromResult<PuzzleBlueprint?>(null));

        // Act
        var result = await _puzzleLoaderService.LoadPlayablePuzzleAsync(puzzleId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task TaskReturnsGridWithProperties_WhenBlueprintIsValid()
    {
        // Arrange
        int puzzleId = 1;
        var blueprint = new PuzzleBlueprint
        {
            Id = 1,
            Name = "Test Puzzle",
            Width = 2,
            Height = 2,
            Nodes = new List<NodeModifierDto>
            {
                new NodeModifierDto(0, 0, true, false, false),
                new NodeModifierDto(1, 1, false, true, false)
            },
            Edges = new List<EdgeModifierDto>
            {
                new EdgeModifierDto(0, 0, 1, 0, true)
            },
            Cells = new List<CellModifierDto>
            {
                new CellModifierDto(0, 0, new SquareSymbol { ColorHex = PuzzleColors.Blue })
            }
        };

        _puzzleRepository.GetBlueprintByIdAsync(puzzleId).Returns(Task.FromResult<PuzzleBlueprint?>(blueprint));    

        // Act
        var gridResult = await _puzzleLoaderService.LoadPlayablePuzzleAsync(puzzleId);

        // Assert
        Assert.NotNull(gridResult);
        Assert.Contains(gridResult.Nodes.Values, n => n.X == 0 && n.Y == 0 && n.IsStart);
        Assert.Contains(gridResult.Nodes.Values, n => n.X == 1 && n.Y == 1 && n.IsEnd);
        Assert.Contains(gridResult.Edges.Values, e => 
            e.HasDot && 
            ((e.NodeA.X == 0 && e.NodeA.Y == 0 && e.NodeB.X == 1 && e.NodeB.Y == 0) ||
            (e.NodeA.X == 1 && e.NodeA.Y == 0 && e.NodeB.X == 0 && e.NodeB.Y == 0)));
        Assert.Contains(gridResult.Cells.Values, c => c.X == 0 && c.Y == 0 && c.Symbol is SquareSymbol squareSymbol && squareSymbol.ColorHex == PuzzleColors.Blue);
    }
}
