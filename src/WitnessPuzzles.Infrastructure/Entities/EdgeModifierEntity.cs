using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WitnessPuzzles.Infrastructure.Entities;

public class EdgeModifierEntity
{
    public int NodeAX { get; set; }
    public int NodeAY { get; set; }
    public int NodeBX { get; set; }
    public int NodeBY { get; set; }
    public bool HasDot { get; set; }
}
