<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-primary">
    <div class="container">
      <RouterLink to="/" class="navbar-brand">
        🎯 EasyStat
      </RouterLink>
      <button
        class="navbar-toggler"
        type="button"
        data-bs-toggle="collapse"
        data-bs-target="#navbarNav"
      >
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarNav">
        <ul class="navbar-nav ms-auto">
          <li class="nav-item">
            <RouterLink to="/players" class="nav-link" active-class="active">
              👤 Spelare
            </RouterLink>
          </li>
          <li class="nav-item">
            <RouterLink to="/teams" class="nav-link" active-class="active">
              👥 Lag
            </RouterLink>
          </li>
          <li class="nav-item">
            <RouterLink to="/stats" class="nav-link" active-class="active">
              📊 Topplistor
            </RouterLink>
          </li>
        </ul>
      </div>
    </div>
  </nav>
</template>

<script setup lang="ts">
import { RouterLink } from 'vue-router'
</script>

<style scoped>
.navbar-brand {
  font-weight: bold;
  font-size: 1.5rem;
}
</style>
