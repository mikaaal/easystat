// Models/Throw.cs
namespace EasyStat.Api.Models;

public class Throw
{
    public int Id { get; set; }
    public int LegId { get; set; }
    public int TeamNumber { get; set; }
    public int RoundNumber { get; set; }
    public int Score { get; set; }
    public int RemainingScore { get; set; }
    public int? DartsUsed { get; set; }

    public Leg Leg { get; set; } = null!;
}
