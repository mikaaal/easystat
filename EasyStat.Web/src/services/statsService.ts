import { api } from './api'
import type { TopStatsDto, OverviewDto } from '@/types/stats'

export const statsService = {
  async getOverview(): Promise<OverviewDto> {
    const response = await api.get<OverviewDto>('/overview')
    return response.data
  },

  async getTopStats(season?: string): Promise<TopStatsDto> {
    const params = season ? { season } : {}
    const response = await api.get<TopStatsDto>('/top-stats', { params })
    return response.data
  }
}
