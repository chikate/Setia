import { fileURLToPath, URL } from 'node:url'

import { defineConfig } from 'vite'
import VueRouter from 'unplugin-vue-router/vite'
import Vue from '@vitejs/plugin-vue'
import { VueRouterAutoImports } from 'unplugin-vue-router'
import AutoImport from 'unplugin-auto-import/vite'
import { PrimeVueResolver } from 'unplugin-vue-components/resolvers'
import Components from 'unplugin-vue-components/vite'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    VueRouter(),
    Vue(),
    AutoImport({
      imports: ['vue', VueRouterAutoImports, 'vue-router', 'pinia'],
      dirs: ['./src/stores/**', './src/*'],
      vueTemplate: true,
      eslintrc: {
        enabled: true
      }
    }),
    Components({ resolvers: [PrimeVueResolver()], dirs: ['./src/components/**'] })
  ],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  },
  publicDir: 'D:/Dragos/Downloads/0Test',
  server: {
    port: 3000,
    strictPort: true,
    proxy: {
      // string shorthand: /foo -> http://localhost:4567/foo
      '/foo': 'http://localhost:4567',
      // with options
      '/api': {
        target: 'https://localhost:44381',
        secure: true,
        changeOrigin: true,
        rewrite: (path) => path.replace(/^\/api/, '')
      }
    }
  }
})
