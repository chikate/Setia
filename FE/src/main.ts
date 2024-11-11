import App from '@/App.vue'

import { createApp } from 'vue'

import { createRouter, createWebHistory } from 'vue-router'
import { routes } from 'vue-router/auto-routes'

import PrimeVue from 'primevue/config'

import Tooltip from 'primevue/tooltip'
import BadgeDirective from 'primevue/badgedirective'
import InputGroup from 'primevue/inputgroup'
import InputGroupAddon from 'primevue/inputgroupaddon'
import ToastService from 'primevue/toastservice'

import { canUserAccessRoute } from '@/global/helpers'

export const app = createApp(App)

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes
})
router.beforeEach(async (to, from, next) =>
  (await canUserAccessRoute(to.fullPath)) ? next() : undefined
)
app.use(router)

app.use(PrimeVue)
app.directive('tooltip', Tooltip)
app.directive('badge', BadgeDirective)
app.component('InputGroup', InputGroup)
app.component('InputGroupAddon', InputGroupAddon)
app.use(ToastService, {
  life: 3000 // Set global toast lifetime in milliseconds
})

app.mount('body')

// const socket = new WebSocket(`ws://${import.meta.env.VITE_SERVER ?? 'localhost:44381'}`) // 'ws://localhost:5000/your-endpoint'

// socket.onopen = () => {
//   console.log('WebSocket connection established')
//   socket.send('Hello Server!')
// }

// socket.onmessage = (event) => console.log('Message from server:', event.data)

// socket.onclose = () => console.log('WebSocket connection closed')

// socket.onerror = (error) => console.error('WebSocket error:', error)
