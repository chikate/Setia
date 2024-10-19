import App from '@/App.vue'

import { createApp } from 'vue'

import { createPinia } from 'pinia'
import piniaPluginPersistedstate from 'pinia-plugin-persistedstate'

import { createRouter, createWebHistory } from 'vue-router'
import { routes } from 'vue-router/auto-routes'

import PrimeVue from 'primevue/config'
import Tooltip from 'primevue/tooltip'
import BadgeDirective from 'primevue/badgedirective'
import InputGroup from 'primevue/inputgroup'
import InputGroupAddon from 'primevue/inputgroupaddon'
import ToastService from 'primevue/toastservice'

import { canUserAccessRoute } from '@/helpers'

export const app = createApp(App)

// Pinia
// needs to be declared/instanced
const pinia = createPinia()
pinia.use(piniaPluginPersistedstate)
app.use(pinia)

// Routes
// needs to be declared/instanced
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes
  // scrollBehavior() {
  //   return { top: 0 }
  // }
})
router.beforeEach(async (to, from, next) => {
  if (await canUserAccessRoute(to.fullPath)) return next()
})
app.use(router)

// Components
app.use(PrimeVue)
app.use(ToastService, {
  life: 3000 // Set global toast lifetime in milliseconds
})
app.directive('tooltip', Tooltip)
app.directive('badge', BadgeDirective)
app.component('InputGroup', InputGroup)
app.component('InputGroupAddon', InputGroupAddon)

// Mount
app.mount('body')

// WebSocket Connection
// const socket = new WebSocket(WEB_SOCKET_URL)
// socket.onopen = () => {
//   socket.send('Client connected!')
//   console.info('WebSocket connection established!')
// }
// socket.onmessage = (data) => console.log(`Received: ${data}`)
// socket.onerror = (error) => console.error(`WebSocket error: ${error}`)
