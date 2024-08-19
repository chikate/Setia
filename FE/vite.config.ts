import { fileURLToPath, URL } from 'node:url'
import { defineConfig } from 'vite'

import VueRouter from 'unplugin-vue-router/vite'
import AutoImport from 'unplugin-auto-import/vite'
import { VueRouterAutoImports } from 'unplugin-vue-router'
import Components from 'unplugin-vue-components/vite'
import { PrimeVueResolver } from 'unplugin-vue-components/resolvers'

import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    VueRouter(),
    Components({ resolvers: [PrimeVueResolver()] }),
    AutoImport({
      imports: ['vue', VueRouterAutoImports],
      dirs: ['src/stores/**'],
      vueTemplate: true,
      eslintrc: {
        enabled: true
      }
    }),
    vue()
  ],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  },
  server: {
    port: 3000,
    strictPort: true
    // hmr: {
    //   host: import.meta.env.VITE_SERVER_IP,
    //   protocol: 'ws',
    //   clientPort: 3000
    // }
  }
})
