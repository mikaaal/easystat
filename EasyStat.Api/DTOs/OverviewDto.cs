// DTOs/OverviewDto.cs
namespace EasyStat.Api.DTOs;

public class OverviewDto
{
    public int TotalPlayers { get; set; }
    public int TotalMatches { get; set; }
    public int TotalSubMatches { get; set; }
    public int TotalLegs { get; set; }
    public int TotalThrows { get; set; }
    public List<RecentMatchDto> RecentMatches { get; set; } = new();
}

public class RecentMatchDto
{
    public string Team1 { get; set; } = string.Empty;
    public string Team2 { get; set; } = string.Empty;
    public int Team1Score { get; set; }
    public int Team2Score { get; set; }
    public DateTime ScrapedAt { get; set; }
}
