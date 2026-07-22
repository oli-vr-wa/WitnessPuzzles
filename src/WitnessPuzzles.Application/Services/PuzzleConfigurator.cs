using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WitnessPuzzles.Application.Interfaces;
using WitnessPuzzles.Core.Models;

namespace WitnessPuzzles.Application.Services;

public class PuzzleConfigurator : IPuzzleConfigurator
{
    /// <inheritdoc />
    public void SetStartNode(PuzzleGrid grid, int x, int y, bool isStartNode)
    {
        grid.Nodes.Values.FirstOrDefault(n => n.X == x && n.Y == y)?.IsStart = isStartNode;
    }

    /// <inheritdoc />
    public void SetEndNode(PuzzleGrid grid, int x, int y, bool isEndNode)
    {
        grid.Nodes.Values.FirstOrDefault(n => n.X == x && n.Y == y)?.IsEnd = isEndNode;
    }

    /// <inheritdoc />
    public void SetCellSymbol(PuzzleGrid grid, int x, int y, CellSymbol symbol)
    {
        grid.Cells.Values.FirstOrDefault(c => c.X == x && c.Y == y)?.Symbol = symbol;
    }

    /// <inheritdoc />
    public void SetNodeDot(PuzzleGrid grid, int x, int y, bool hasDot)
    {
        grid.Nodes.Values.FirstOrDefault(n => n.X == x && n.Y == y)?.HasDot = hasDot;
    }

    /// <inheritdoc />
    public void SetEdgeDot(PuzzleGrid grid, int nodeAx, int nodeAy, int nodeBx, int nodeBy, bool hasDot)
    {
        grid.Edges.Values.FirstOrDefault(e => e.NodeA.X == nodeAx && e.NodeA.Y == nodeAy && e.NodeB.X == nodeBx && e.NodeB.Y == nodeBy)?.HasDot = hasDot;
    }
}
