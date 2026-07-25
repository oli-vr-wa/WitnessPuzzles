namespace WitnessPuzzles.Core.Models;

public class Edge
{
    public required int Id { get; init; }
    public required Node NodeA { get; init; }
    public required Node NodeB { get; init; }
    public bool HasDot { get; set; } = false;  

    public bool IsEdge(int nodeAId, int nodeBId) => (NodeA.Id == nodeAId && NodeB.Id == nodeBId) || (NodeA.Id == nodeBId && NodeB.Id == nodeAId);
}
