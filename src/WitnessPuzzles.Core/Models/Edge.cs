namespace WitnessPuzzles.Core.Models;

public class Edge
{
    public required int Id { get; init; }
    public required Node NodeA { get; init; }
    public required Node NodeB { get; init; }
}
