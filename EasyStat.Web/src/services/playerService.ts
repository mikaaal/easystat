import { api } from './api'
import type { PlayerStatsDto, ThrowAnalysisDto } from '@/types/player'

export const playerService = {
  async getAllPlayers(): Promise<string[]> {
    const response = await api.get<string[]>('/players')
    return response.data
  },

  async getPlayerStats(playerName: string, limit?: number): Promise<PlayerStatsDto> {
    const params = limit ? { limit } : {}
    const response = await api.get<PlayerStatsDto>(`/players/${encodeURIComponent(playerName)}`, { params })
    return response.data
  },

  async getPlayerThrows(playerName: string): Promise<ThrowAnalysisDto> {
    const response = await api.get<ThrowAnalysisDto>(`/players/${encodeURIComponent(playerName)}/throws`)
    return response.data
  }
}
