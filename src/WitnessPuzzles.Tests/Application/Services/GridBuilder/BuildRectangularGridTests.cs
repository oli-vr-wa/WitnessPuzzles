using WitnessPuzzles.Application.Interfaces;
using WitnessPuzzles.Application.Services;

namespace WitnessPuzzles.Tests.Application.Services.GridBuilder;

public class BuildRectangularGridTests
{
    private readonly GridBuilderService _gridBuilderService;

    public BuildRectangularGridTests()
    {
        _gridBuilderService = new GridBuilderService();
    }

    [Fact]
    public void ThrowsArgumentException_WhenWidthIsZero()
    {
        // Arrange
        int width = 0;
        int height = 5;

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _gridBuilderService.BuildRectangularGrid(width, height));        
    }

    [Fact]
    public void ThrowsArgumentException_WhenHeightIsZero()
    {
        // Arrange
        int width = 5;
        int height = 0;

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _gridBuilderService.BuildRectangularGrid(width, height));        
    }

    [Fact]
    public void ReturnsCorrectGrid_WhenWidthAndHeightArePositive()
    {
        // Arrange
        int width = 3;
        int height = 2;

        // Act
        var grid = _gridBuilderService.BuildRectangularGrid(width, height);

        // Assert
        Assert.NotNull(grid);
        Assert.Equal((width + 1) * (height + 1), grid.Nodes.Count);
        Assert.Equal(width * (height + 1) + (width + 1) * height, grid.Edges.Count);
        Assert.Equal(width * height, grid.Cells.Count);
    }
}
