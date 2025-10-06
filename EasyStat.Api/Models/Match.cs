// Models/Match.cs
namespace EasyStat.Api.Models;

public class Match
{
    public int Id { get; set; }
    public string MatchUrl { get; set; } = string.Empty;
    public int Team1Id { get; set; }
    public int Team2Id { get; set; }
    public int Team1Score { get; set; }
    public int Team2Score { get; set; }
    public decimal? Team1Avg { get; set; }
    public decimal? Team2Avg { get; set; }
    public string? Division { get; set; }
    public string? Season { get; set; }
    public DateTime? MatchDate { get; set; }
    public DateTime ScrapedAt { get; set; } = DateTime.UtcNow;

    public Team Team1 { get; set; } = null!;
    public Team Team2 { get; set; } = null!;
    public ICollection<SubMatch> SubMatches { get; set; } = new List<SubMatch>();
}
