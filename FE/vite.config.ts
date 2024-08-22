import { fileURLToPath, URL } from 'node:url'

import { defineConfig } from 'vite'
import Vue from '@vitejs/plugin-vue'

import AutoImport from 'unplugin-auto-import/vite'
import { AutoGenerateImports } from 'vite-auto-import-resolvers'

import Components from 'unplugin-vue-components/vite'
import { PrimeVueResolver } from 'unplugin-vue-components/resolvers'

import VueRouter from 'unplugin-vue-router/vite'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    VueRouter(),
    Vue(),
    Components({ resolvers: [PrimeVueResolver()], dirs: ['src/components/**'] }),
    AutoImport({
      imports: AutoGenerateImports(),
      dirs: ['src/stores/**', 'src/**'],
      vueTemplate: true,
      eslintrc: {
        enabled: true
      }
    })
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
