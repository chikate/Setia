import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('../views/HomeView.vue'),
      children: [
        {
          path: 'account',
          component: () => import('../views/HomeView.vue')
        }
      ]
    },
    {
      path: '/download',
      name: 'download',
      component: () => import('../views/DownloadView.vue')
    },
    {
      path: '/register',
      name: 'register',
      component: () => import('../views/RegisterView.vue')
    },
    {
      path: '/recovery',
      name: 'recovery',
      component: () => import('../views/RecoveryView.vue')
    },
    {
      path: '/about',
      name: 'about',
      component: () => import('../views/AboutView.vue')
    },
    {
      path: '/career',
      name: 'career',
      component: () => import('../views/CareerView.vue')
    },
    {
      path: '/news',
      name: 'news',
      component: () => import('../views/NewsView.vue')
    },
    {
      path: '/universe',
      name: 'universe',
      component: () => import('../views/UniverseView.vue'),
      children: [
        {
          path: 'region/:name',
          component: () => import('../views/UniverseView.vue')
        },
        {
          path: 'character/:name',
          component: () => import('../views/UniverseView.vue')
        },
        {
          path: 'item/:name',
          component: () => import('../views/UniverseView.vue')
        },
        {
          path: 'ability/:name',
          component: () => import('../views/UniverseView.vue')
        },
        {
          path: 'story/:name',
          component: () => import('../views/UniverseView.vue')
        }
      ]
    }
  ]
})

export default router
