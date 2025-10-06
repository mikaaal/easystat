// Models/Player.cs
namespace EasyStat.Api.Models;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<SubMatchParticipant> Participations { get; set; } = new List<SubMatchParticipant>();
}
