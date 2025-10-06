using EasyStat.Api.Data;
using EasyStat.Api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EasyStat.Api.Services;

public interface ITeamService
{
    Task<List<TeamDto>> GetAllTeamsAsync();
    Task<TeamLineupDto?> GetTeamLineupAsync(string teamName, string season, string division);
}

public class TeamService : ITeamService
{
    private readonly EasyStatDbContext _context;

    public TeamService(EasyStatDbContext context)
    {
        _context = context;
    }

    public async Task<List<TeamDto>> GetAllTeamsAsync()
    {
        var teams = await _context.Teams
            .Join(_context.Matches,
                t => t.Id,
                m => m.Team1Id,
                (t, m) => new { Team = t, Match = m })
            .Where(tm => tm.Match.Season != null && tm.Match.Division != null && tm.Match.Division != "Unknown")
            .Select(tm => new TeamDto
            {
                TeamName = tm.Team.Name,
                Season = tm.Match.Season ?? "",
                Division = tm.Match.Division ?? ""
            })
            .Union(
                _context.Teams
                    .Join(_context.Matches,
                        t => t.Id,
                        m => m.Team2Id,
                        (t, m) => new { Team = t, Match = m })
                    .Where(tm => tm.Match.Season != null && tm.Match.Division != null && tm.Match.Division != "Unknown")
                    .Select(tm => new TeamDto
                    {
                        TeamName = tm.Team.Name,
                        Season = tm.Match.Season ?? "",
                        Division = tm.Match.Division ?? ""
                    })
            )
            .Distinct()
            .OrderBy(t => t.TeamName)
            .ThenByDescending(t => t.Season)
            .ToListAsync();

        return teams;
    }

    public async Task<TeamLineupDto?> GetTeamLineupAsync(string teamName, string season, string division)
    {
        // Find the team
        var team = await _context.Teams
            .FirstOrDefaultAsync(t => t.Name == teamName);

        if (team == null)
            return null;

        // Get all matches for this team in the specified season and division
        var matches = await _context.Matches
            .Include(m => m.SubMatches)
                .ThenInclude(sm => sm.Participants)
                    .ThenInclude(p => p.Player)
            .Where(m => (m.Team1Id == team.Id || m.Team2Id == team.Id)
                && m.Season == season
                && m.Division == division)
            .ToListAsync();

        if (!matches.Any())
            return null;

        // Collect all players who played for this team
        var playerStats = new Dictionary<string, (int matches, int wins, int losses, List<decimal> avgs, int singles, int doubles)>();

        foreach (var match in matches)
        {
            var teamNumber = match.Team1Id == team.Id ? 1 : 2;

            foreach (var subMatch in match.SubMatches)
            {
                var teamParticipants = subMatch.Participants
                    .Where(p => p.TeamNumber == teamNumber)
                    .ToList();

                foreach (var participant in teamParticipants)
                {
                    var playerName = participant.Player.Name;

                    if (!playerStats.ContainsKey(playerName))
                    {
                        playerStats[playerName] = (0, 0, 0, new List<decimal>(), 0, 0);
                    }

                    var stats = playerStats[playerName];
                    var won = (teamNumber == 1 && subMatch.Team1Legs > subMatch.Team2Legs) ||
                              (teamNumber == 2 && subMatch.Team2Legs > subMatch.Team1Legs);

                    stats.matches++;
                    if (won) stats.wins++;
                    else stats.losses++;

                    if (participant.PlayerAvg.HasValue && participant.PlayerAvg.Value > 0)
                    {
                        stats.avgs.Add(participant.PlayerAvg.Value);
                    }

                    if (subMatch.MatchType == "Singles")
                        stats.singles++;
                    else
                        stats.doubles++;

                    playerStats[playerName] = stats;
                }
            }
        }

        var players = playerStats.Select(kvp =>
        {
            var (matches, wins, losses, avgs, singles, doubles) = kvp.Value;
            var winPercentage = matches > 0 ? (decimal)wins / matches * 100 : 0;
            var avgScore = avgs.Any() ? avgs.Average() : 0;

            return new TeamPlayerDto
            {
                PlayerName = kvp.Key,
                TotalMatches = matches,
                Wins = wins,
                Losses = losses,
                WinPercentage = Math.Round(winPercentage, 1),
                AverageScore = Math.Round(avgScore, 2),
                SinglesMatches = singles,
                DoublesMatches = doubles
            };
        })
        .OrderByDescending(p => p.TotalMatches)
        .ThenByDescending(p => p.AverageScore)
        .ToList();

        return new TeamLineupDto
        {
            TeamName = teamName,
            Season = season,
            Division = division,
            Players = players
        };
    }
}
