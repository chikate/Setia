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

const app = createApp(App)
app.use(createPinia().use(piniaPluginPersistedstate))
app.use(
  createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    scrollBehavior() {
      // always start page at top, without this code it will keep the previous scroll
      return { top: 0 }
    }
  })
)
app.use(PrimeVue)

app.directive('tooltip', Tooltip)
app.directive('badge', BadgeDirective)
app.component('InputGroup', InputGroup)
app.component('InputGroupAddon', InputGroupAddon)

app.mount('body')
