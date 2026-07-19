using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WitnessPuzzles.Core.Models;

public abstract class CellSymbol
{
    public required string ColorHex { get; init; }
}
