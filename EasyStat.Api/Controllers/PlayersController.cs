using EasyStat.Api.DTOs;
using EasyStat.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasyStat.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private readonly IPlayerService _playerService;

    public PlayersController(IPlayerService playerService)
    {
        _playerService = playerService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<string>>> GetAllPlayers()
    {
        var players = await _playerService.GetAllPlayerNamesAsync();
        return Ok(players);
    }

    [HttpGet("{playerName}")]
    public async Task<ActionResult<PlayerStatsDto>> GetPlayerStats(
        string playerName,
        [FromQuery] int? limit = null)
    {
        var stats = await _playerService.GetPlayerStatsAsync(playerName, limit);
        if (stats == null)
            return NotFound(new { error = "Player not found" });

        return Ok(stats);
    }

    [HttpGet("{playerName}/throws")]
    public async Task<ActionResult<ThrowAnalysisDto>> GetPlayerThrows(string playerName)
    {
        var analysis = await _playerService.GetPlayerThrowAnalysisAsync(playerName);
        if (analysis == null)
            return NotFound(new { error = "Player not found" });

        return Ok(analysis);
    }
}