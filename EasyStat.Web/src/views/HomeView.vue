<template>
  <div class="home">
    <div class="row justify-content-center">
      <div class="col-lg-8">
        <div class="card shadow-lg">
          <div class="card-body text-center p-5">
            <h1 class="display-4 mb-4">🎯 Välkommen till EasyStat</h1>
            <p class="lead text-muted mb-4">
              Statistik för dart-spelare och lag från nakka.com
            </p>

            <div class="row g-3 mt-4">
              <div class="col-md-4">
                <div class="card h-100 border-primary">
                  <div class="card-body">
                    <h3>👤</h3>
                    <h5 class="card-title">Spelare</h5>
                    <p class="card-text text-muted">Sök efter spelare och se detaljerad statistik</p>
                    <RouterLink to="/players" class="btn btn-primary">
                      Sök spelare
                    </RouterLink>
                  </div>
                </div>
              </div>

              <div class="col-md-4">
                <div class="card h-100 border-success">
                  <div class="card-body">
                    <h3>👥</h3>
                    <h5 class="card-title">Lag</h5>
                    <p class="card-text text-muted">Se laguppställningar och statistik per lag</p>
                    <RouterLink to="/teams" class="btn btn-success">
                      Sök lag
                    </RouterLink>
                  </div>
                </div>
              </div>

              <div class="col-md-4">
                <div class="card h-100 border-info">
                  <div class="card-body">
                    <h3>📊</h3>
                    <h5 class="card-title">Topplistor</h5>
                    <p class="card-text text-muted">Se top 10 listor för olika kategorier</p>
                    <RouterLink to="/stats" class="btn btn-info">
                      Visa topplistor
                    </RouterLink>
                  </div>
                </div>
              </div>
            </div>

            <div class="alert alert-warning mt-5" role="alert">
              <strong>Betaversion:</strong> Statistik baseras på data från nakka.com.
              Det kan förekomma fel eller saknade uppgifter.
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { RouterLink } from 'vue-router'
</script>

<style scoped>
.home {
  padding: 2rem 0;
}
</style>
