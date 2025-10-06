using EasyStat.Api.DTOs;
using EasyStat.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasyStat.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    private readonly ITeamService _teamService;

    public TeamsController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TeamDto>>> GetAllTeams()
    {
        var teams = await _teamService.GetAllTeamsAsync();
        return Ok(teams);
    }

    [HttpGet("lineup")]
    public async Task<ActionResult<TeamLineupDto>> GetTeamLineup(
        [FromQuery] string teamName,
        [FromQuery] string season,
        [FromQuery] string division)
    {
        if (string.IsNullOrEmpty(teamName) || string.IsNullOrEmpty(season) || string.IsNullOrEmpty(division))
        {
            return BadRequest(new { error = "teamName, season, and division are required" });
        }

        var lineup = await _teamService.GetTeamLineupAsync(teamName, season, division);
        if (lineup == null)
            return NotFound(new { error = "Team not found or no data for specified season/division" });

        return Ok(lineup);
    }
}
