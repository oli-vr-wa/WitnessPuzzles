using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WitnessPuzzles.Core.Models;

namespace WitnessPuzzles.Application.Interfaces;

public interface IPuzzleConfigurator
{
    /// <summary>
    /// Sets the start node for the puzzle grid. If isStartNode is true, it sets the specified cell as the start node; if false, 
    /// it removes the start node designation from that cell.
    /// </summary>
    /// <param name="grid">The puzzle grid.</param>
    /// <param name="x">The x-coordinate of the cell.</param>
    /// <param name="y">The y-coordinate of the cell.</param>
    /// <param name="isStartNode">True to set the start node; false to remove it.</param>
    void SetStartNode(PuzzleGrid grid, int x, int y, bool isStartNode);

    /// <summary>
    /// Sets the end node for the puzzle grid. If isEndNode is true, it sets the specified cell as the end node; if false, 
    /// it removes the end node designation from that cell.
    /// </summary>
    /// <param name="grid">The puzzle grid.</param>
    /// <param name="x">The x-coordinate of the cell.</param>
    /// <param name="y">The y-coordinate of the cell.</param>
    /// <param name="isEndNode">True to set the end node; false to remove it.</param>
    void SetEndNode(PuzzleGrid grid, int x, int y, bool isEndNode);

    /// <summary>
    /// Sets the symbol for a specific cell in the puzzle grid. This method allows you to assign a symbol to a cell at the specified coordinates (x, y) within the grid. 
    /// The symbol can represent various elements or features of the puzzle, squares, stars and tetris shapes for now.
    /// </summary>
    /// <param name="grid">The puzzle grid.</param>
    /// <param name="x">The x-coordinate of the cell.</param>
    /// <param name="y">The y-coordinate of the cell.</param>
    /// <param name="symbol">The symbol to set for the cell.</param>
    void SetCellSymbol(PuzzleGrid grid, int x, int y, CellSymbol symbol);

    /// <summary>
    /// Sets or removes a dot on a specific node in the puzzle grid. 
    /// If hasDot is true, it adds a dot to the node at the specified coordinates (x, y); if false, it removes the dot from that node.
    /// </summary>
    /// <param name="grid">The puzzle grid.</param>
    /// <param name="x">The x-coordinate of the node.</param>
    /// <param name="y">The y-coordinate of the node.</param>
    /// <param name="hasDot">True to add a dot; false to remove it.</param>
    void SetNodeDot(PuzzleGrid grid, int x, int y, bool hasDot);

    /// <summary>
    /// Sets or removes a dot on the edge between two nodes in the puzzle grid.
    /// If hasDot is true, it adds a dot to the edge connecting the nodes at (nodeAx, nodeAy) and (nodeBx, nodeBy); if false, it removes the dot from that edge.
    /// </summary>
    /// <param name="grid">The puzzle grid.</param>
    /// <param name="nodeAx">The x-coordinate of the first node.</param>
    /// <param name="nodeAy">The y-coordinate of the first node.</param>
    /// <param name="nodeBx">The x-coordinate of the second node.</param>
    /// <param name="nodeBy">The y-coordinate of the second node.</param>
    /// <param name="hasDot">True to add a dot; false to remove it
    void SetEdgeDot(PuzzleGrid grid, int nodeAx, int nodeAy, int nodeBx, int nodeBy, bool hasDot);
}
