using WitnessPuzzles.Application.Interfaces;
using WitnessPuzzles.Core.Models;

namespace WitnessPuzzles.Application.Services;

public class GridBuilderService : IGridBuilderService
{
    /// <inheritdoc />
    public PuzzleGrid BuildRectangularGrid(int width, int height)
    {
        var nodes = new Dictionary<int, Node>();
        var edges = new Dictionary<int, Edge>();
        var cells = new Dictionary<int, Cell>();

        int nodeIdCounter = 1;
        int edgeIdCounter = 1;
        int cellIdCounter = 1;

        // Create nodes
        var nodeMatrix = new Node[width + 1, height + 1];

        for (int y = 0; y <= height; y++)
        {
            for (int x = 0; x <= width; x++)
            {
                var node = new Node { Id = nodeIdCounter++, X = x, Y = y };
                nodes.Add(node.Id, node);
                nodeMatrix[x, y] = node;
            }
        }

        // Create edges and link them to nodes
        var horizontalEdges = new Edge[width, height + 1];
        var verticalEdges = new Edge[width + 1, height];

        for (int y = 0; y <= height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var edge = new Edge
                {
                    Id = edgeIdCounter++,
                    NodeA = nodeMatrix[x, y],
                    NodeB = nodeMatrix[x + 1, y]
                };

                edge.NodeA.ConnectedEdges.Add(edge);
                edge.NodeB.ConnectedEdges.Add(edge);

                edges.Add(edge.Id, edge);
                horizontalEdges[x, y] = edge;
            }
        }

        // Create vertical edges and link them to nodes
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x <= width; x++)
            {
                var edge = new Edge
                {
                    Id = edgeIdCounter++,
                    NodeA = nodeMatrix[x, y],
                    NodeB = nodeMatrix[x, y + 1]
                };

                edge.NodeA.ConnectedEdges.Add(edge);
                edge.NodeB.ConnectedEdges.Add(edge);

                edges.Add(edge.Id, edge);
                verticalEdges[x, y] = edge;
            }
        }

        // Create cells and link them to edges boundries
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var cell = new Cell
                {
                    Id = cellIdCounter++,
                    X = x,
                    Y = y,
                    TopEdge = horizontalEdges[x, y],
                    BottomEdge = horizontalEdges[x, y + 1],
                    LeftEdge = verticalEdges[x, y],
                    RightEdge = verticalEdges[x + 1, y]
                };

                cells.Add(cell.Id, cell);
            }
        }

        return new PuzzleGrid
        {
            Nodes = nodes,
            Edges = edges,
            Cells = cells
        };
    }
}
