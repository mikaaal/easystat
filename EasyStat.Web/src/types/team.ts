export interface TeamDto {
  teamName: string
  season: string
  division: string
}

export interface TeamLineupDto {
  teamName: string
  season: string
  division: string
  players: TeamPlayerDto[]
}

export interface TeamPlayerDto {
  playerName: string
  totalMatches: number
  wins: number
  losses: number
  winPercentage: number
  averageScore: number
  singlesMatches: number
  doublesMatches: number
}
