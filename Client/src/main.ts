import App from "@/App.vue";
import { createApp } from "vue";
import { createRouter, createWebHistory } from "vue-router";
import { routes } from "vue-router/auto-routes";
import { createPinia } from "pinia";
import piniaPluginPersistedstate from "pinia-plugin-persistedstate";
import { setupPrimeVue } from "@/plugins/primevue";

export const app = createApp(App);

export const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach(canUserAccessRoute);
app.use(router);

// Setup PrimeVue
setupPrimeVue(app);

// Setup Pinia
app.use(createPinia().use(piniaPluginPersistedstate));

// Setup Global Fetch Interceptor
setupGlobalFetchInterceptor(app);

app.mount("body");
