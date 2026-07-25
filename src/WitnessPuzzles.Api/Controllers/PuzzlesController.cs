using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WitnessPuzzles.Api.Models;
using WitnessPuzzles.Application.DTOs;
using WitnessPuzzles.Application.Interfaces;
using WitnessPuzzles.Core.Interfaces.Validation;
using WitnessPuzzles.Core.Models;
using WitnessPuzzles.Core.Models.Validation;

namespace WitnessPuzzles.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PuzzlesController(
    IPuzzleLoaderService puzzleLoaderService, IPuzzleSaverService puzzleSaverService) : ControllerBase
{
    private readonly IPuzzleLoaderService _puzzleLoaderService = puzzleLoaderService;
    private readonly IPuzzleSaverService _puzzleSaverService = puzzleSaverService;

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PuzzleGrid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPuzzle(Guid id)
    {
        var puzzleGrid = await _puzzleLoaderService.LoadPlayablePuzzleAsync(id);
        if (puzzleGrid == null)
        {
            return NotFound(new { Message = $"Puzzle with ID {id} not found." });
        }

        return Ok(puzzleGrid);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SavePuzzle([FromBody] CreatePuzzleRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Name) || request.Grid == null)
        {
            return BadRequest(new { Message = "Invalid puzzle data." });
        }

        var puzzleId = await _puzzleSaverService.SavePuzzleGridAsync(request.Grid, request.Name);
        return CreatedAtAction(nameof(GetPuzzle), new { id = puzzleId }, new { Id = puzzleId });
    }

    [HttpPost("{id:guid}/validate")]
    public async Task<ActionResult<PuzzleValidationResult>> ValidateSolution(
        Guid id, 
        [FromBody] ValidateSolutionDto request,
        [FromServices] IPuzzleValidationEngine validationEngine)
    {
        var playablePuzzle = await _puzzleLoaderService.LoadPlayablePuzzleAsync(id);
        if (playablePuzzle == null)
        {
            return NotFound(new { Message = $"Puzzle with ID {id} not found." });
        }

        var result = validationEngine.ValidateSolution(playablePuzzle, request.DrawnNodeIds);

        return Ok(result);
    }
}

