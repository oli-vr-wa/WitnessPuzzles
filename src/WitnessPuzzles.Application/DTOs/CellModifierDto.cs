using WitnessPuzzles.Core.Models;

namespace WitnessPuzzles.Application.DTOs;

public record CellModifierDto(int X, int Y, CellSymbol Symbol);
