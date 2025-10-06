// Models/Team.cs
namespace EasyStat.Api.Models;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Division { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Match> HomeMatches { get; set; } = new List<Match>();
    public ICollection<Match> AwayMatches { get; set; } = new List<Match>();
}
