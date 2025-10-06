// Models/SubMatchParticipant.cs
namespace EasyStat.Api.Models;

public class SubMatchParticipant
{
    public int Id { get; set; }
    public int SubMatchId { get; set; }
    public int PlayerId { get; set; }
    public int TeamNumber { get; set; }
    public decimal? PlayerAvg { get; set; }

    public SubMatch SubMatch { get; set; } = null!;
    public Player Player { get; set; } = null!;
}
