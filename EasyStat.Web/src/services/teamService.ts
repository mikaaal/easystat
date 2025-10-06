import { api } from './api'
import type { TeamDto, TeamLineupDto } from '@/types/team'

export const teamService = {
  async getAllTeams(): Promise<TeamDto[]> {
    const response = await api.get<TeamDto[]>('/teams')
    return response.data
  },

  async getTeamLineup(teamName: string, season: string, division: string): Promise<TeamLineupDto> {
    const response = await api.get<TeamLineupDto>('/teams/lineup', {
      params: { teamName, season, division }
    })
    return response.data
  }
}
