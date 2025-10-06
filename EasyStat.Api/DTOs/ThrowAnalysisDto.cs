// DTOs/ThrowAnalysisDto.cs
namespace EasyStat.Api.DTOs;

public class ThrowAnalysisDto
{
    public string PlayerName { get; set; } = string.Empty;
    public List<ThrowDetailDto> Throws { get; set; } = new();
    public ThrowStatisticsDto Statistics { get; set; } = new();
}

public class ThrowDetailDto
{
    public int Score { get; set; }
    public int RemainingScore { get; set; }
    public int? DartsUsed { get; set; }
    public int RoundNumber { get; set; }
    public int LegNumber { get; set; }
    public string MatchType { get; set; } = string.Empty;
    public string MatchName { get; set; } = string.Empty;
    public DateTime? MatchDate { get; set; }
    public bool WonMatch { get; set; }
}

public class ThrowStatisticsDto
{
    public int TotalThrows { get; set; }
    public decimal AverageScore { get; set; }
    public int MaxScore { get; set; }
    public Dictionary<string, int> ScoreRanges { get; set; } = new();
    public int TotalCheckouts { get; set; }
    public List<int> CheckoutScores { get; set; } = new();
    public int ThrowsOver100 { get; set; }
    public int Throws180 { get; set; }
    public int ThrowsOver140 { get; set; }
    public int Throws26 { get; set; }
    public int ThrowsUnder20 { get; set; }
    public List<MostCommonScoreDto> MostCommonScores { get; set; } = new();
}

public class MostCommonScoreDto
{
    public int Score { get; set; }
    public int Count { get; set; }
}
