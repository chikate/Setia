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

export const app = createApp(App);

export const router = createRouter({ history: createWebHistory(), routes });
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
    ripple: true,
  },
});

// Provide dark mode globally
app.provide("darkMode", darkMode);

app.directive("tooltip", Tooltip);
app.directive("badge", BadgeDirective);
app.component("InputGroup", InputGroup);
app.component("InputGroupAddon", InputGroupAddon);

app.use(ToastService, {
  life: 3000,
});

app.mount("body");

// SSE Stream (Server Side Events)
(async () => {
  try {
    const reader = (
      await fetch("/api/sse/StreamSSE", {
        headers: {
          Authorization: `Bearer ${getCookie("token")}`,
          "Content-Type": "text/event-stream",
          CacheControl: "no-cache",
          Connection: "keep-alive",
        },
      })
    ).body?.getReader();

    if (!reader) return;

    while (true) {
      const { value, done } = await reader.read();

      if (done) break;

      localStorage.setItem(
        "notifications",
        JSON.parse(new TextDecoder().decode(value))
      );
    }
  } catch (error) {
    console.error(error);
  }
})();
