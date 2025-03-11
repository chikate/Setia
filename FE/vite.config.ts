import { fileURLToPath, URL } from "node:url";

import { defineConfig } from "vite";
import VueRouter from "unplugin-vue-router/vite";
import Vue from "@vitejs/plugin-vue";
import { VueRouterAutoImports } from "unplugin-vue-router";
import AutoImport from "unplugin-auto-import/vite";
import { PrimeVueResolver } from "unplugin-vue-components/resolvers";
import Components from "unplugin-vue-components/vite";

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
  ],
  resolve: {
    alias: {
      "@": fileURLToPath(new URL("src", import.meta.url)),
    },
  },
  server: {
    port: 3000,
    strictPort: true,
  },
});
