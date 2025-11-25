import type { App } from "vue";
import PrimeVue from "primevue/config";
import AuraCustom from "@/presets/aura-custom";
import ToastService from "primevue/toastservice";
import Tooltip from "primevue/tooltip";
import BadgeDirective from "primevue/badgedirective";

export function setupPrimeVue(app: App) {
  app.use(PrimeVue, {
    theme: {
      preset: AuraCustom,
      options: {
        prefix: "p",
        darkModeSelector: ".dark-mode",
        cssLayer: true,
      },
    },
  });

  app.use(ToastService);

  app.directive("tooltip", Tooltip);
  app.directive("badge", BadgeDirective);

  app.provide("themeManager", themeManager);
}
