import { createRouter, createWebHistory } from 'vue-router/auto'

const router = createRouter({
  history: createWebHistory(),
  scrollBehavior() {
    // always start page at top, without this code it will keep the previous scroll
    return { top: 0 }
  }
})

export default router
