/// <reference types="node" />
/// <reference types="vite/client" />
/// <reference types="vue" />
/// <reference types="unplugin-vue-router/client" />
/// <reference types="vue-router" />

import { Router } from 'vue-router'

declare module '@vue/runtime-core' {
  interface ComponentCustomProperties {
    $router: Router
    $route: ReturnType<Router['currentRoute']>
  }
}
