export interface PlayerStatsDto {
  playerName: string
  totalMatches: number
  wins: number
  losses: number
  winPercentage: number
  averageScore: number
  recentMatches: MatchHistoryDto[]
}

export interface MatchHistoryDto {
  matchDate: string | null
  team1Name: string
  team2Name: string
  matchType: string
  matchName: string
  team1Legs: number
  team2Legs: number
  teamNumber: number
  playerAvg: number | null
  opponent: string
  won: boolean
  subMatchId: number
}

export interface ThrowAnalysisDto {
  playerName: string
  throws: ThrowDetailDto[]
  statistics: ThrowStatisticsDto
}

export interface ThrowDetailDto {
  score: number
  remainingScore: number
  dartsUsed: number
  roundNumber: number
  legNumber: number
  matchType: string
  matchName: string
  matchDate: string | null
  wonMatch: boolean
}

export interface ThrowStatisticsDto {
  totalThrows: number
  averageScore: number
  maxScore: number
  scoreRanges: Record<string, number>
  totalCheckouts: number
  checkoutScores: number[]
  throwsOver100: number
  throws180: number
  throwsOver140: number
  throws26: number
  throwsUnder20: number
  mostCommonScores: MostCommonScoreDto[]
}

export interface MostCommonScoreDto {
  score: number
  count: number
}
