// DTOs/TeamDto.cs
namespace EasyStat.Api.DTOs;

public class TeamDto
{
    public string TeamName { get; set; } = string.Empty;
    public string Season { get; set; } = string.Empty;
    public string Division { get; set; } = string.Empty;
}

public class TeamLineupDto
{
    public string TeamName { get; set; } = string.Empty;
    public string Season { get; set; } = string.Empty;
    public string Division { get; set; } = string.Empty;
    public List<TeamPlayerDto> Players { get; set; } = new();
}

public class TeamPlayerDto
{
    public string PlayerName { get; set; } = string.Empty;
    public int TotalMatches { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }
    public decimal WinPercentage { get; set; }
    public decimal AverageScore { get; set; }
    public int SinglesMatches { get; set; }
    public int DoublesMatches { get; set; }
}
