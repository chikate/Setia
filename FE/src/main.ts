import App from './App.vue'

import { createApp } from 'vue'
import { createPinia } from 'pinia'
import piniaPluginPersistedstate from 'pinia-plugin-persistedstate'

import { createRouter, createWebHistory } from 'vue-router/auto'
import PrimeVue from 'primevue/config'

import Tooltip from 'primevue/tooltip'
import BadgeDirective from 'primevue/badgedirective'
import InputGroup from 'primevue/inputgroup'
import InputGroupAddon from 'primevue/inputgroupaddon'
import ToastService from 'primevue/toastservice'

// const linkElement = document.getElementById('theme-link')
// ;(linkElement as HTMLAnchorElement).href =
//   `/node_modules/primevue/resources/themes/aura-${localStorage.getItem('theme')}-noir/theme.css`

const app = createApp(App)

// Pinia
app.use(createPinia().use(piniaPluginPersistedstate))

// Routes
app.use(
  createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    scrollBehavior() {
      return { top: 0 }
    }
  })
  // .beforeEach((to, from, next) => {
  //   console.log('test')
  //   next()
  // })
)

// Components
app.use(PrimeVue)
app.use(ToastService)
app.directive('tooltip', Tooltip)
app.directive('badge', BadgeDirective)
app.component('InputGroup', InputGroup)
app.component('InputGroupAddon', InputGroupAddon)

app.mount('body')
