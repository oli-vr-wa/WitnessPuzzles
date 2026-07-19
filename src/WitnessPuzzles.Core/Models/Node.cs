namespace WitnessPuzzles.Core.Models;

public class Node
{
    public required int Id { get; init; }
    public required int X { get; init; }
    public required int Y { get; init; }

    public bool IsStart { get; init; } = false;
    public bool IsEnd { get; init; } = false;
    public bool HasDot { get; init; } = false;

    public List<Edge> ConnectedEdges { get; } = new List<Edge>(); // A node knows which edges are connected to it.
}
