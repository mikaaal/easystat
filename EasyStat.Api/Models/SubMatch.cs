// Models/SubMatch.cs
namespace EasyStat.Api.Models;

public class SubMatch
{
    public int Id { get; set; }
    public int MatchId { get; set; }
    public int MatchNumber { get; set; }
    public string MatchType { get; set; } = string.Empty;
    public string? MatchName { get; set; }
    public int Team1Legs { get; set; }
    public int Team2Legs { get; set; }
    public decimal? Team1Avg { get; set; }
    public decimal? Team2Avg { get; set; }
    public string? Mid { get; set; }
    public DateTime ScrapedAt { get; set; } = DateTime.UtcNow;

    public Match Match { get; set; } = null!;
    public ICollection<SubMatchParticipant> Participants { get; set; } = new List<SubMatchParticipant>();
    public ICollection<Leg> Legs { get; set; } = new List<Leg>();
}
