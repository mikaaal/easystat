// DTOs/SubMatchThrowsDto.cs
namespace EasyStat.Api.DTOs;

public class SubMatchThrowsDto
{
    public SubMatchInfoDto SubMatchInfo { get; set; } = new();
    public string PlayerName { get; set; } = string.Empty;
    public string OpponentNames { get; set; } = string.Empty;
    public List<LegWithThrowsDto> Legs { get; set; } = new();
    public PlayerStatsInMatchDto PlayerStatistics { get; set; } = new();
    public PlayerStatsInMatchDto OpponentStatistics { get; set; } = new();
}

public class SubMatchInfoDto
{
    public int Id { get; set; }
    public string MatchType { get; set; } = string.Empty;
    public string MatchName { get; set; } = string.Empty;
    public int Team1Legs { get; set; }
    public int Team2Legs { get; set; }
    public decimal? PlayerAvg { get; set; }
    public DateTime? MatchDate { get; set; }
    public string Team1Name { get; set; } = string.Empty;
    public string Team2Name { get; set; } = string.Empty;
    public int TeamNumber { get; set; }
}

public class LegWithThrowsDto
{
    public int LegNumber { get; set; }
    public int WinnerTeam { get; set; }
    public int LegTotalRounds { get; set; }
    public bool PlayerWonLeg { get; set; }
    public List<ThrowInLegDto> PlayerThrows { get; set; } = new();
    public List<ThrowInLegDto> OpponentThrows { get; set; } = new();
}

public class ThrowInLegDto
{
    public int RoundNumber { get; set; }
    public int Score { get; set; }
    public int RemainingScore { get; set; }
    public int? DartsUsed { get; set; }
    public int LegNumber { get; set; }
}

public class PlayerStatsInMatchDto
{
    public int TotalThrows { get; set; }
    public int TotalScore { get; set; }
    public decimal AverageScore { get; set; }
    public int MaxScore { get; set; }
    public int LegsWon { get; set; }
    public int LegsTotal { get; set; }
    public int Throws100_139 { get; set; }
    public int Throws140_179 { get; set; }
    public int Throws180 { get; set; }
    public int Throws100PlusTotal { get; set; }
    public int ThrowsUnder20 { get; set; }
    public int Throws60Plus { get; set; }
    public int TotalCheckouts { get; set; }
    public int CheckoutDarts { get; set; }
    public int ShortLegs { get; set; }
    public int HighFinishes { get; set; }
    public decimal First9DartAvg { get; set; }
}
