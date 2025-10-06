// DTOs/PlayerStatsDto.cs
namespace EasyStat.Api.DTOs;

public class PlayerStatsDto
{
    public string PlayerName { get; set; } = string.Empty;
    public int TotalMatches { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }
    public decimal WinPercentage { get; set; }
    public decimal AverageScore { get; set; }
    public List<MatchHistoryDto> RecentMatches { get; set; } = new();
}

public class MatchHistoryDto
{
    public DateTime? MatchDate { get; set; }
    public string Team1Name { get; set; } = string.Empty;
    public string Team2Name { get; set; } = string.Empty;
    public string MatchType { get; set; } = string.Empty;
    public string MatchName { get; set; } = string.Empty;
    public int Team1Legs { get; set; }
    public int Team2Legs { get; set; }
    public int TeamNumber { get; set; }
    public decimal? PlayerAvg { get; set; }
    public string Opponent { get; set; } = string.Empty;
    public bool Won { get; set; }
    public int SubMatchId { get; set; }
}
