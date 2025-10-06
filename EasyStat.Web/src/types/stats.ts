export interface TopStatsDto {
  topAverages: TopAverageDto[]
  topCheckouts: TopCheckoutDto[]
  shortestLegs: ShortestLegDto[]
  most180s: Most180sDto[]
}

export interface TopAverageDto {
  playerName: string
  average: number
  teamName: string
  opponentName: string
  matchDate: string | null
  subMatchId: number
}

export interface TopCheckoutDto {
  playerName: string
  checkout: number
  teamName: string
  opponentName: string
  matchDate: string | null
  subMatchId: number
}

export interface ShortestLegDto {
  playerName: string
  darts: number
  teamName: string
  opponentName: string
  matchDate: string | null
  subMatchId: number
}

export interface Most180sDto {
  playerName: string
  count180: number
  teamName: string
  opponentName: string
  matchDate: string | null
  subMatchId: number
}

export interface OverviewDto {
  totalPlayers: number
  totalMatches: number
  totalSubMatches: number
  totalLegs: number
  totalThrows: number
  recentMatches: RecentMatchDto[]
}

export interface RecentMatchDto {
  team1: string
  team2: string
  team1Score: number
  team2Score: number
  scrapedAt: string
}
