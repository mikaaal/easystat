import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '@/views/HomeView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/players',
      name: 'players',
      component: () => import('@/views/PlayersView.vue')
    },
    {
      path: '/teams',
      name: 'teams',
      component: () => import('@/views/TeamsView.vue')
    },
    {
      path: '/stats',
      name: 'stats',
      component: () => import('@/views/StatsView.vue')
    }
  ]
})

export default router
