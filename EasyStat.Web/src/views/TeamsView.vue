<template>
  <div class="teams-view">
    <h2 class="mb-4">👥 Lagsökning</h2>

    <div class="row justify-content-center mb-4">
      <div class="col-lg-8">
        <div class="card shadow">
          <div class="card-body p-4">
            <h5 class="text-center mb-3">Sök efter lag</h5>
            <input
              v-model="searchQuery"
              type="text"
              class="form-control form-control-lg"
              placeholder="Lagnamn, division eller säsong..."
              @input="handleSearch"
            />

            <!-- Team Suggestions -->
            <div v-if="filteredTeams.length > 0 && searchQuery" class="list-group mt-2">
              <button
                v-for="(team, index) in filteredTeams.slice(0, 10)"
                :key="index"
                class="list-group-item list-group-item-action"
                @click="selectTeam(team)"
              >
                {{ team.teamName }} ({{ team.division }}) - {{ team.season }}
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Team Lineup -->
    <div v-if="teamLineup" class="row">
      <div class="col-12">
        <div class="card shadow">
          <div class="card-header">
            <h5 class="mb-0">
              {{ teamLineup.teamName }} - {{ teamLineup.division }} ({{ teamLineup.season }})
            </h5>
          </div>
          <div class="card-body">
            <div class="table-responsive">
              <table class="table table-striped">
                <thead>
                  <tr>
                    <th>Spelare</th>
                    <th>Matcher</th>
                    <th>Vinster</th>
                    <th>Förluster</th>
                    <th>Vinstprocent</th>
                    <th>Snitt</th>
                    <th>Singlar</th>
                    <th>Dubblar</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="player in teamLineup.players" :key="player.playerName">
                    <td><strong>{{ player.playerName }}</strong></td>
                    <td>{{ player.totalMatches }}</td>
                    <td>{{ player.wins }}</td>
                    <td>{{ player.losses }}</td>
                    <td>{{ player.winPercentage.toFixed(1) }}%</td>
                    <td>{{ player.averageScore.toFixed(2) }}</td>
                    <td>{{ player.singlesMatches }}</td>
                    <td>{{ player.doublesMatches }}</td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>
    </div>

    <div v-if="isLoadingTeams" class="text-center">
      <div class="spinner-border text-primary" role="status">
        <span class="visually-hidden">Laddar...</span>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'
import { useQuery } from '@tanstack/vue-query'
import { teamService } from '@/services/teamService'
import type { TeamDto, TeamLineupDto } from '@/types/team'

const searchQuery = ref('')
const teamLineup = ref<TeamLineupDto | null>(null)

const { data: allTeams, isLoading: isLoadingTeams } = useQuery({
  queryKey: ['teams'],
  queryFn: () => teamService.getAllTeams()
})

const filteredTeams = computed(() => {
  if (!searchQuery.value || !allTeams.value) return []
  const query = searchQuery.value.toLowerCase()
  return allTeams.value.filter(team =>
    team.teamName.toLowerCase().includes(query) ||
    team.division.toLowerCase().includes(query) ||
    team.season.toLowerCase().includes(query)
  )
})

const handleSearch = () => {
  teamLineup.value = null
}

const selectTeam = async (team: TeamDto) => {
  try {
    teamLineup.value = await teamService.getTeamLineup(team.teamName, team.season, team.division)
    searchQuery.value = `${team.teamName} (${team.division}) - ${team.season}`
  } catch (error) {
    console.error('Failed to fetch team lineup:', error)
  }
}
</script>

<style scoped>
.teams-view {
  padding: 1rem 0;
}
</style>
