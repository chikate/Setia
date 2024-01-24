import './assets/DarkMode-Theme.css'
import 'primeicons/primeicons.css'
import 'primeflex/primeflex.css'

import './assets/base.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'

import router from './router'
import PrimeVue from 'primevue/config'
import ToastService from 'primevue/toastservice'
import Tooltip from 'primevue/tooltip'

const app = createApp(App)

app.use(createPinia())
app.use(router)
app.use(PrimeVue)

app.use(ToastService)
app.directive('tooltip', Tooltip)

app.mount('body')
