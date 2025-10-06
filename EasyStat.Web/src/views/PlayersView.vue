<template>
  <div class="players-view">
    <h2 class="mb-4">👤 Spelarsökning</h2>

    <!-- Search Section -->
    <div class="row justify-content-center mb-4">
      <div class="col-lg-8">
        <div class="card shadow">
          <div class="card-body p-4">
            <h5 class="text-center mb-3">Sök efter spelare</h5>
            <div class="input-group input-group-lg">
              <input
                v-model="searchQuery"
                type="text"
                class="form-control"
                placeholder="Namn..."
                @input="handleSearch"
                @keyup.enter="selectFirstMatch"
              />
              <button
                v-if="searchQuery"
                class="btn btn-outline-secondary"
                type="button"
                @click="clearSearch"
              >
                ✕
              </button>
            </div>

            <!-- Autocomplete Suggestions -->
            <div v-if="filteredPlayers.length > 0 && searchQuery" class="list-group mt-2">
              <button
                v-for="(player, index) in filteredPlayers.slice(0, 10)"
                :key="index"
                class="list-group-item list-group-item-action"
                @click="selectPlayer(player)"
              >
                {{ player }}
              </button>
            </div>

            <div v-if="searchQuery && filteredPlayers.length === 0" class="alert alert-info mt-3">
              Inga spelare hittades
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Player Stats Section -->
    <div v-if="selectedPlayer && playerStats" class="row">
      <div class="col-12">
        <div class="card shadow">
          <div class="card-header d-flex justify-content-between align-items-center">
            <h5 class="mb-0">Statistik för {{ playerStats.playerName }}</h5>
            <button class="btn btn-outline-secondary btn-sm" @click="clearSelection">
              ✕ Stäng
            </button>
          </div>
          <div class="card-body">
            <!-- Overview Stats -->
            <div class="row g-3 mb-4">
              <div class="col-md-3">
                <div class="card bg-primary text-white">
                  <div class="card-body text-center">
                    <h2>{{ playerStats.totalMatches }}</h2>
                    <p class="mb-0">Totalt matcher</p>
                  </div>
                </div>
              </div>
              <div class="col-md-3">
                <div class="card bg-success text-white">
                  <div class="card-body text-center">
                    <h2>{{ playerStats.wins }}</h2>
                    <p class="mb-0">Vinster</p>
                  </div>
                </div>
              </div>
              <div class="col-md-3">
                <div class="card bg-danger text-white">
                  <div class="card-body text-center">
                    <h2>{{ playerStats.losses }}</h2>
                    <p class="mb-0">Förluster</p>
                  </div>
                </div>
              </div>
              <div class="col-md-3">
                <div class="card bg-info text-white">
                  <div class="card-body text-center">
                    <h2>{{ playerStats.averageScore.toFixed(2) }}</h2>
                    <p class="mb-0">Snitt (singlar)</p>
                  </div>
                </div>
              </div>
            </div>

            <!-- Match History -->
            <h6 class="mt-4 mb-3">Matchhistorik</h6>
            <div class="table-responsive">
              <table class="table table-striped table-hover">
                <thead>
                  <tr>
                    <th>Datum</th>
                    <th>Typ</th>
                    <th>Motståndare</th>
                    <th>Resultat</th>
                    <th>Snitt</th>
                    <th>Utfall</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="match in playerStats.recentMatches.slice(0, 20)" :key="match.subMatchId">
                    <td>{{ formatDate(match.matchDate) }}</td>
                    <td>{{ match.matchType }}</td>
                    <td>{{ match.opponent }}</td>
                    <td>{{ match.team1Legs }}-{{ match.team2Legs }}</td>
                    <td>{{ match.playerAvg?.toFixed(2) || '-' }}</td>
                    <td>
                      <span v-if="match.won" class="badge bg-success">Vinst</span>
                      <span v-else class="badge bg-danger">Förlust</span>
                    </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Loading State -->
    <div v-if="isLoadingPlayers" class="text-center">
      <div class="spinner-border text-primary" role="status">
        <span class="visually-hidden">Laddar...</span>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'
import { useQuery } from '@tanstack/vue-query'
import { playerService } from '@/services/playerService'
import type { PlayerStatsDto } from '@/types/player'

const searchQuery = ref('')
const selectedPlayer = ref<string | null>(null)
const playerStats = ref<PlayerStatsDto | null>(null)

// Fetch all players for autocomplete
const { data: allPlayers, isLoading: isLoadingPlayers } = useQuery({
  queryKey: ['players'],
  queryFn: () => playerService.getAllPlayers()
})

// Filter players based on search query
const filteredPlayers = computed(() => {
  if (!searchQuery.value || !allPlayers.value) return []
  const query = searchQuery.value.toLowerCase()
  return allPlayers.value.filter(player =>
    player.toLowerCase().includes(query)
  )
})

const handleSearch = () => {
  playerStats.value = null
  selectedPlayer.value = null
}

const selectPlayer = async (playerName: string) => {
  selectedPlayer.value = playerName
  searchQuery.value = playerName

  try {
    playerStats.value = await playerService.getPlayerStats(playerName)
  } catch (error) {
    console.error('Failed to fetch player stats:', error)
  }
}

const selectFirstMatch = () => {
  if (filteredPlayers.value.length > 0) {
    selectPlayer(filteredPlayers.value[0])
  }
}

const clearSearch = () => {
  searchQuery.value = ''
  selectedPlayer.value = null
  playerStats.value = null
}

const clearSelection = () => {
  selectedPlayer.value = null
  playerStats.value = null
}

const formatDate = (dateString: string | null) => {
  if (!dateString) return '-'
  return new Date(dateString).toLocaleDateString('sv-SE')
}
</script>

<style scoped>
.players-view {
  padding: 1rem 0;
}
</style>
