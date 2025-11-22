import App from "@/App.vue";

import { createRouter, createWebHistory } from "vue-router";
import { routes } from "vue-router/auto-routes";

import piniaPluginPersistedstate from "pinia-plugin-persistedstate";
import { setupPrimeVue } from "@/plugins/primevue";

export const app = createApp(App);

export const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach(canUserAccessRoute);
app.use(router);

setupPrimeVue(app);

app.use(createPinia().use(piniaPluginPersistedstate));

setupGlobalFetchInterceptor(app);

app.mount("body");
