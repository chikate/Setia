import { fileURLToPath, URL } from "node:url";

import { defineConfig } from "vite";
import VueRouter from "unplugin-vue-router/vite";
import Vue from "@vitejs/plugin-vue";
import { VueRouterAutoImports } from "unplugin-vue-router";
import AutoImport from "unplugin-auto-import/vite";
import { PrimeVueResolver } from "unplugin-vue-components/resolvers";
import Components from "unplugin-vue-components/vite";
import { VitePWA } from "vite-plugin-pwa";

export default defineConfig({
  plugins: [
    VueRouter(),
    Vue(),
    AutoImport({
      imports: ["vue", VueRouterAutoImports, "vue-router"],
      vueTemplate: true,
      dirs: ["src/globals/**"],
    }),
    Components({
      resolvers: [PrimeVueResolver()],
      dirs: ["src/components/**"],
    }),
    VitePWA({
      registerType: "autoUpdate",
      devOptions: {
        enabled: process.env.NODE_ENV == "development",
      },
      manifest: {
        name: "Dragos App",
        short_name: "App",
        scope: "/",
        start_url: "/",
        display: "standalone",
        background_color: "#ffffff",
        theme_color: "#ffffff",
        icons: [
          {
            src: "/Calories.png",
            sizes: "192x192",
            type: "image/png",
          },
          {
            src: "/Calories.png",
            sizes: "512x512",
            type: "image/png",
          },
        ],
      },
    }),
  ],
  resolve: {
    alias: {
      "@": fileURLToPath(new URL("./src", import.meta.url)),
    },
  },
  server: {
    port: 3000,
    strictPort: true,
  },
});
