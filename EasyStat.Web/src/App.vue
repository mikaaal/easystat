<template>
  <div id="app">
    <AppHeader />
    <main class="container mt-4">
      <RouterView />
    </main>
  </div>
</template>

<script setup lang="ts">
import AppHeader from '@/components/layout/AppHeader.vue'
import { RouterView } from 'vue-router'
</script>

<style>
#app {
  min-height: 100vh;
  background-color: #f8f9fa;
}

main {
  padding-bottom: 3rem;
}
</style>
