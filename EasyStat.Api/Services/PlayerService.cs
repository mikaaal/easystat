using EasyStat.Api.Data;
using EasyStat.Api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EasyStat.Api.Services;

public interface IPlayerService
{
    Task<List<string>> GetAllPlayerNamesAsync();
    Task<PlayerStatsDto?> GetPlayerStatsAsync(string playerName, int? limit = null);
    Task<ThrowAnalysisDto?> GetPlayerThrowAnalysisAsync(string playerName);
}

public class PlayerService : IPlayerService
{
    private readonly EasyStatDbContext _context;

    public PlayerService(EasyStatDbContext context)
    {
        _context = context;
    }

    public async Task<List<string>> GetAllPlayerNamesAsync()
    {
        return await _context.Players
            .OrderBy(p => p.Name)
            .Select(p => p.Name)
            .Distinct()
            .ToListAsync();
    }

    public async Task<PlayerStatsDto?> GetPlayerStatsAsync(string playerName, int? limit = null)
    {
        var player = await _context.Players
            .FirstOrDefaultAsync(p => p.Name == playerName);

        if (player == null)
            return null;

        var participations = await _context.SubMatchParticipants
            .Include(smp => smp.SubMatch)
                .ThenInclude(sm => sm.Match)
                    .ThenInclude(m => m.Team1)
            .Include(smp => smp.SubMatch)
                .ThenInclude(sm => sm.Match)
                    .ThenInclude(m => m.Team2)
            .Where(smp => smp.PlayerId == player.Id)
            .OrderByDescending(smp => smp.SubMatch.Match.MatchDate)
            .ToListAsync();

        var matches = new List<MatchHistoryDto>();

        foreach (var participation in participations)
        {
            var subMatch = participation.SubMatch;
            var match = subMatch.Match;

            // Get opponents
            var opposingTeamNumber = participation.TeamNumber == 1 ? 2 : 1;
            var opponents = await _context.SubMatchParticipants
                .Include(smp => smp.Player)
                .Where(smp => smp.SubMatchId == subMatch.Id && smp.TeamNumber == opposingTeamNumber)
                .Select(smp => smp.Player.Name)
                .ToListAsync();

            var opponent = subMatch.MatchType == "Singles"
                ? (opponents.FirstOrDefault() ?? "Unknown")
                : string.Join(" + ", opponents);

            var won = (participation.TeamNumber == 1 && subMatch.Team1Legs > subMatch.Team2Legs) ||
                      (participation.TeamNumber == 2 && subMatch.Team2Legs > subMatch.Team1Legs);

            matches.Add(new MatchHistoryDto
            {
                MatchDate = match.MatchDate,
                Team1Name = match.Team1.Name,
                Team2Name = match.Team2.Name,
                MatchType = subMatch.MatchType,
                MatchName = subMatch.MatchName ?? "",
                Team1Legs = subMatch.Team1Legs,
                Team2Legs = subMatch.Team2Legs,
                TeamNumber = participation.TeamNumber,
                PlayerAvg = participation.PlayerAvg,
                Opponent = opponent,
                Won = won,
                SubMatchId = subMatch.Id
            });
        }

        if (limit.HasValue)
        {
            matches = matches.Take(limit.Value).ToList();
        }

        var totalMatches = matches.Count;
        var wins = matches.Count(m => m.Won);
        var losses = totalMatches - wins;
        var winPercentage = totalMatches > 0 ? (decimal)wins / totalMatches * 100 : 0;

        var singlesMatches = matches.Where(m => m.MatchType == "Singles" && m.PlayerAvg.HasValue).ToList();
        var avgScore = singlesMatches.Any() 
            ? singlesMatches.Average(m => m.PlayerAvg!.Value) 
            : 0;

        return new PlayerStatsDto
        {
            PlayerName = playerName,
            TotalMatches = totalMatches,
            Wins = wins,
            Losses = losses,
            WinPercentage = Math.Round(winPercentage, 1),
            AverageScore = Math.Round(avgScore, 2),
            RecentMatches = matches
        };
    }

    public async Task<ThrowAnalysisDto?> GetPlayerThrowAnalysisAsync(string playerName)
    {
        var player = await _context.Players
            .FirstOrDefaultAsync(p => p.Name == playerName);

        if (player == null)
            return null;

        var throws = await _context.Throws
            .Include(t => t.Leg)
                .ThenInclude(l => l.SubMatch)
                    .ThenInclude(sm => sm.Match)
            .Include(t => t.Leg.SubMatch.Participants)
            .Where(t => t.Leg.SubMatch.Participants.Any(p => p.PlayerId == player.Id) 
                && t.Leg.SubMatch.MatchType == "Singles")
            .OrderByDescending(t => t.Leg.SubMatch.Match.MatchDate)
            .Take(1000)
            .ToListAsync();

        if (!throws.Any())
        {
            return new ThrowAnalysisDto
            {
                PlayerName = playerName,
                Throws = new List<ThrowDetailDto>(),
                Statistics = new ThrowStatisticsDto()
            };
        }

        var throwDetails = throws.Select(t => new ThrowDetailDto
        {
            Score = t.Score,
            RemainingScore = t.RemainingScore,
            DartsUsed = t.DartsUsed,
            RoundNumber = t.RoundNumber,
            LegNumber = t.Leg.LegNumber,
            MatchType = t.Leg.SubMatch.MatchType,
            MatchName = t.Leg.SubMatch.MatchName ?? "",
            MatchDate = t.Leg.SubMatch.Match.MatchDate,
            WonMatch = false // Calculate this properly if needed
        }).ToList();

        var scores = throws.Where(t => t.Score > 0).Select(t => t.Score).ToList();
        var avgScore = scores.Any() ? scores.Average() : 0;
        var maxScore = scores.Any() ? scores.Max() : 0;

        var scoreRanges = new Dictionary<string, int>
        {
            ["0-20"] = scores.Count(s => s >= 0 && s <= 20),
            ["21-40"] = scores.Count(s => s >= 21 && s <= 40),
            ["41-60"] = scores.Count(s => s >= 41 && s <= 60),
            ["61-80"] = scores.Count(s => s >= 61 && s <= 80),
            ["81-100"] = scores.Count(s => s >= 81 && s <= 100),
            ["100+"] = scores.Count(s => s > 100)
        };

        var checkouts = throws.Where(t => t.RemainingScore == 0 && t.Score > 0).Select(t => t.Score).ToList();

        var mostCommonScores = scores
            .GroupBy(s => s)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => new MostCommonScoreDto { Score = g.Key, Count = g.Count() })
            .ToList();

        return new ThrowAnalysisDto
        {
            PlayerName = playerName,
            Throws = throwDetails.Take(100).ToList(),
            Statistics = new ThrowStatisticsDto
            {
                TotalThrows = throws.Count,
                AverageScore = Math.Round((decimal)avgScore, 2),
                MaxScore = maxScore,
                ScoreRanges = scoreRanges,
                TotalCheckouts = checkouts.Count,
                CheckoutScores = checkouts,
                ThrowsOver100 = scores.Count(s => s > 100),
                Throws180 = scores.Count(s => s == 180),
                ThrowsOver140 = scores.Count(s => s > 140),
                Throws26 = scores.Count(s => s == 26),
                ThrowsUnder20 = scores.Count(s => s < 20),
                MostCommonScores = mostCommonScores
            }
        };
    }
}