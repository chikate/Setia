import type { App } from "vue";
import PrimeVue from "primevue/config";
import Aura from "@primevue/themes/aura";
import ToastService from "primevue/toastservice";
import Tooltip from "primevue/tooltip";
import BadgeDirective from "primevue/badgedirective";

export function setupPrimeVue(app: App) {
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

    app.use(ToastService, { life: 3000 });

    app.directive("tooltip", Tooltip);
    app.directive("badge", BadgeDirective);

    // Provide dark mode state globally if needed, though it's a composable
    app.provide("darkMode", darkMode);
}
