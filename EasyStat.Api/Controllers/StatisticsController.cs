using EasyStat.Api.DTOs;
using EasyStat.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasyStat.Api.Controllers;

[ApiController]
[Route("api")]
public class StatisticsController : ControllerBase
{
    private readonly IStatisticsService _statisticsService;

    public StatisticsController(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    [HttpGet("overview")]
    public async Task<ActionResult<OverviewDto>> GetOverview()
    {
        var overview = await _statisticsService.GetOverviewAsync();
        return Ok(overview);
    }

    [HttpGet("sub_match/{subMatchId}/throws/{playerName}")]
    public async Task<ActionResult<SubMatchThrowsDto>> GetSubMatchThrows(
        int subMatchId,
        string playerName)
    {
        var result = await _statisticsService.GetSubMatchThrowsAsync(subMatchId, playerName);
        if (result == null)
            return NotFound(new { error = "Player did not participate in this sub-match" });

        return Ok(result);
    }

    [HttpGet("top-stats")]
    public async Task<ActionResult<TopStatsDto>> GetTopStats([FromQuery] string? season = null)
    {
        var stats = await _statisticsService.GetTopStatsAsync(season);
        return Ok(stats);
    }
}