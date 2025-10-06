// Models/Leg.cs
namespace EasyStat.Api.Models;

public class Leg
{
    public int Id { get; set; }
    public int SubMatchId { get; set; }
    public int LegNumber { get; set; }
    public int WinnerTeam { get; set; }
    public int FirstPlayerTeam { get; set; }
    public int TotalRounds { get; set; }
    public DateTime ScrapedAt { get; set; } = DateTime.UtcNow;

    public SubMatch SubMatch { get; set; } = null!;
    public ICollection<Throw> Throws { get; set; } = new List<Throw>();
}
