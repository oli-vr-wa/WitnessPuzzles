
namespace WitnessPuzzles.Infrastructure.Entities;

public class NodeModifierEntity
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsStart { get; set; }
    public bool IsEnd { get; set; }
    public bool HasDot { get; set; }
}
