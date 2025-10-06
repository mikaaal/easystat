<template>
  <div class="stats-view">
    <h2 class="mb-4">📊 Topplistor</h2>

    <!-- Season Filter -->
    <div class="row mb-4">
      <div class="col-md-4">
        <select v-model="selectedSeason" class="form-select" @change="refetchStats">
          <option value="">Alla säsonger</option>
          <option value="2025/2026">2025/2026</option>
          <option value="2024/2025">2024/2025</option>
        </select>
      </div>
    </div>

    <div v-if="isLoading" class="text-center">
      <div class="spinner-border text-primary" role="status">
        <span class="visually-hidden">Laddar...</span>
      </div>
    </div>

    <div v-else-if="topStats" class="row g-4">
      <!-- Top Averages -->
      <div class="col-lg-6">
        <div class="card shadow">
          <div class="card-header bg-primary text-white">
            <h5 class="mb-0">🎯 Top 10 Högsta Snitt (Singlar)</h5>
          </div>
          <div class="card-body">
            <div class="table-responsive">
              <table class="table table-sm">
                <thead>
                  <tr>
                    <th>#</th>
                    <th>Spelare</th>
                    <th>Snitt</th>
                    <th>Datum</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="(item, index) in topStats.topAverages" :key="index">
                    <td>{{ index + 1 }}</td>
                    <td>{{ item.playerName }}</td>
                    <td><strong>{{ item.average.toFixed(2) }}</strong></td>
                    <td>{{ formatDate(item.matchDate) }}</td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>

      <!-- Top Checkouts -->
      <div class="col-lg-6">
        <div class="card shadow">
          <div class="card-header bg-success text-white">
            <h5 class="mb-0">✅ Top 10 Högsta Checkouts</h5>
          </div>
          <div class="card-body">
            <div class="table-responsive">
              <table class="table table-sm">
                <thead>
                  <tr>
                    <th>#</th>
                    <th>Spelare</th>
                    <th>Checkout</th>
                    <th>Datum</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="(item, index) in topStats.topCheckouts" :key="index">
                    <td>{{ index + 1 }}</td>
                    <td>{{ item.playerName }}</td>
                    <td><strong>{{ item.checkout }}</strong></td>
                    <td>{{ formatDate(item.matchDate) }}</td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>

      <!-- Shortest Legs -->
      <div class="col-lg-6">
        <div class="card shadow">
          <div class="card-header bg-warning">
            <h5 class="mb-0">⚡ Top 10 Kortaste Legs</h5>
          </div>
          <div class="card-body">
            <div class="table-responsive">
              <table class="table table-sm">
                <thead>
                  <tr>
                    <th>#</th>
                    <th>Spelare</th>
                    <th>Darts</th>
                    <th>Datum</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="(item, index) in topStats.shortestLegs" :key="index">
                    <td>{{ index + 1 }}</td>
                    <td>{{ item.playerName }}</td>
                    <td><strong>{{ item.darts }}</strong></td>
                    <td>{{ formatDate(item.matchDate) }}</td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>

      <!-- Most 180s -->
      <div class="col-lg-6">
        <div class="card shadow">
          <div class="card-header bg-info text-white">
            <h5 class="mb-0">💯 Top 10 Flest 180:or i en Match</h5>
          </div>
          <div class="card-body">
            <div class="table-responsive">
              <table class="table table-sm">
                <thead>
                  <tr>
                    <th>#</th>
                    <th>Spelare</th>
                    <th>Antal</th>
                    <th>Datum</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="(item, index) in topStats.most180s" :key="index">
                    <td>{{ index + 1 }}</td>
                    <td>{{ item.playerName }}</td>
                    <td><strong>{{ item.count180 }}</strong></td>
                    <td>{{ formatDate(item.matchDate) }}</td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useQuery } from '@tanstack/vue-query'
import { statsService } from '@/services/statsService'

const selectedSeason = ref('')

const { data: topStats, isLoading, refetch } = useQuery({
  queryKey: ['topStats', selectedSeason],
  queryFn: () => statsService.getTopStats(selectedSeason.value || undefined)
})

const refetchStats = () => {
  refetch()
}

const formatDate = (dateString: string | null) => {
  if (!dateString) return '-'
  return new Date(dateString).toLocaleDateString('sv-SE')
}
</script>

<style scoped>
.stats-view {
  padding: 1rem 0;
}
</style>
