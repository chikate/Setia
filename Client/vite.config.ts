import { fileURLToPath, URL } from "node:url";
import { defineConfig, loadEnv } from "vite";
import VueRouter from "unplugin-vue-router/vite";
import Vue from "@vitejs/plugin-vue";
import { VueRouterAutoImports } from "unplugin-vue-router";
import AutoImport from "unplugin-auto-import/vite";
import { PrimeVueResolver } from "unplugin-vue-components/resolvers";
import Components from "unplugin-vue-components/vite";
import { VitePWA } from "vite-plugin-pwa";

export default defineConfig(({ mode }) => {
  const env = loadEnv(mode, process.cwd(), "");

  return {
    plugins: [
      VueRouter(),
      Vue(),
      AutoImport({
        imports: ["vue", VueRouterAutoImports, "vue-router", "pinia"],
        vueTemplate: true,
        dirs: ["src/composables/**", "src/stores/**"],
      }),
      Components({
        resolvers: [PrimeVueResolver()],
        dirs: ["src/components/**"],
      }),
      VitePWA({
        registerType: "autoUpdate",
        manifest: {
          short_name: "Carrot",
          name: "Carrot",
          icons: [
            {
              src: "/Carrot.png",
              sizes: "192x192",
              type: "image/png",
            },
            {
              src: "/Carrot.png",
              sizes: "512x512",
              type: "image/png",
            },
          ],
          start_url: ".",
          display: "standalone",
          theme_color: "black",
          background_color: "white",
        },
        workbox: {
          clientsClaim: true,
          skipWaiting: true,
          globPatterns: ["**/*.{html,js,css,png,jpg,jpeg,svg}"],
          maximumFileSizeToCacheInBytes: 10 * 1024 * 1024, // 10 MB
        },
      }),
    ],
    resolve: {
      alias: {
        "@": fileURLToPath(new URL("./src", import.meta.url)),
      },
    },
    server: {
      port: parseInt(env.PORT) || 3000,
      strictPort: true,
    },
  };
});
