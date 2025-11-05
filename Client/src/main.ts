import App from "@/App.vue";
import { createApp } from "vue";
import { createRouter, createWebHistory } from "vue-router";
import { routes } from "vue-router/auto-routes";
import PrimeVue from "primevue/config";
import Aura from "@primevue/themes/aura";
import Tooltip from "primevue/tooltip";
import BadgeDirective from "primevue/badgedirective";
import InputGroup from "primevue/inputgroup";
import InputGroupAddon from "primevue/inputgroupaddon";
import ToastService from "primevue/toastservice";
import { createPinia } from "pinia";
import piniaPluginPersistedstate from "pinia-plugin-persistedstate";

export const app = createApp(App);

export const router = createRouter({
  history: createWebHistory(),
  routes,
});
router.beforeEach(canUserAccessRoute);
app.use(router);

app.use(PrimeVue, {
  theme: {
    preset: Aura,
    options: {
      prefix: "p",
      darkModeSelector: ".dark-mode",
      cssLayer: true,
    },
  },
});

app.provide("darkMode", darkMode);

app.use(ToastService, { life: 3000 });
app.component("InputGroup", InputGroup);
app.component("InputGroupAddon", InputGroupAddon);
app.directive("tooltip", Tooltip);
app.directive("badge", BadgeDirective);

app.use(createPinia().use(piniaPluginPersistedstate));

app.mount("body");

const originalFetch = window.fetch;

window.fetch = async function (
  input: RequestInfo | URL,
  init: RequestInit = {}
): Promise<Response> {
  const forcedHeaders: HeadersInit = {
    Authorization: `Bearer ${getCookie("access_token")}`,
    "X-Requested-With": "XMLHttpRequest",
  };

  const headers: HeadersInit = {
    ...(init.headers || {}),
    ...forcedHeaders,
  };

  const response = await originalFetch(input, { ...init, headers });

  const clone = response.clone();
  let message = "";

  try {
    const text = await clone.text();
    message = text?.length > 200 ? text.slice(0, 200) + "..." : text;
  } catch {
    message = "[Unable to read response body]";
  }

  const severity = response.ok
    ? "success"
    : response.status >= 400 && response.status < 600
    ? "error"
    : "info";

  if (message && (window as any).app?.config?.globalProperties?.$toast) {
    (window as any).app.config.globalProperties.$toast.add({
      summary: capitalizeWords(severity),
      detail: message || "No response body",
      severity,
    });
  }

  return response;
};
