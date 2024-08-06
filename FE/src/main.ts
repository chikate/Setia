import App from './App.vue'

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

import { WEB_SOCKET_URL } from './constants'

const app = createApp(App)

// Pinia
app.use(createPinia().use(piniaPluginPersistedstate))

// Routes
app.use(
  createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes,
    scrollBehavior() {
      return { top: 0 }
    }
  })
  // .beforeEach((to, from, next) => {
  //   console.log('test')
  //   next()
  // })
)

app.use(PrimeVue)

// Components
app.use(ToastService)
app.directive('tooltip', Tooltip)
app.directive('badge', BadgeDirective)
app.component('InputGroup', InputGroup)
app.component('InputGroupAddon', InputGroupAddon)

app.mount('body')

// Replace "localhost:3000" with your server address
const socket = new WebSocket(WEB_SOCKET_URL)

// Event handler for when the connection is established
socket.onopen = () => {
  socket.send('Client connected!')
  console.info('WebSocket connection established!')
}

// Event handler for when a message is received from the server
socket.onmessage = (data) => {
  console.log(`Received: ${data}`)
}

// Event handler for when an error occurs with the WebSocket
socket.onerror = (error) => {
  console.error(`WebSocket error: ${error}`)
}
