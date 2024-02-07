import App from './App.vue'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import router from './router'
import PrimeVue from 'primevue/config'
import Tooltip from 'primevue/tooltip'
import BadgeDirective from 'primevue/badgedirective'

const app = createApp(App)

app.use(createPinia())
app.use(router)
app.use(PrimeVue)
app.directive('tooltip', Tooltip)
app.directive('badge', BadgeDirective)

app.mount('body')
