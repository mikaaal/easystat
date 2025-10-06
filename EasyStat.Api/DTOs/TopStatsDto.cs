// DTOs/TopStatsDto.cs
namespace EasyStat.Api.DTOs;

public class TopStatsDto
{
    public List<TopAverageDto> TopAverages { get; set; } = new();
    public List<TopCheckoutDto> TopCheckouts { get; set; } = new();
    public List<ShortestLegDto> ShortestLegs { get; set; } = new();
    public List<Most180sDto> Most180s { get; set; } = new();
}

public class TopAverageDto
{
    public string PlayerName { get; set; } = string.Empty;
    public decimal Average { get; set; }
    public string TeamName { get; set; } = string.Empty;
    public string OpponentName { get; set; } = string.Empty;
    public DateTime? MatchDate { get; set; }
    public int SubMatchId { get; set; }
}

public class TopCheckoutDto
{
    public string PlayerName { get; set; } = string.Empty;
    public int Checkout { get; set; }
    public string TeamName { get; set; } = string.Empty;
    public string OpponentName { get; set; } = string.Empty;
    public DateTime? MatchDate { get; set; }
    public int SubMatchId { get; set; }
}

public class ShortestLegDto
{
    public string PlayerName { get; set; } = string.Empty;
    public int Darts { get; set; }
    public string TeamName { get; set; } = string.Empty;
    public string OpponentName { get; set; } = string.Empty;
    public DateTime? MatchDate { get; set; }
    public int SubMatchId { get; set; }
}

public class Most180sDto
{
    public string PlayerName { get; set; } = string.Empty;
    public int Count180 { get; set; }
    public string TeamName { get; set; } = string.Empty;
    public string OpponentName { get; set; } = string.Empty;
    public DateTime? MatchDate { get; set; }
    public int SubMatchId { get; set; }
}
