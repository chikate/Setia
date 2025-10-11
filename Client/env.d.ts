/// <reference types="node" />
/// <reference types="vite/client" />
/// <reference types="vue" />
/// <reference types="unplugin-vue-router/client" />
/// <reference types="vite-plugin-pwa/client" />

import { Router, RouteLocationNormalized } from "vue-router";

declare module "@vue/runtime-core" {
  interface ComponentCustomProperties {
    $router: Router;
    $route: RouteLocationNormalized;
  }
}

// src/types.d.ts
import "vue-router";

declare module "vue-router" {
  interface RouteMeta {
    title?: string;
    requiresAuth?: boolean;
    role?: string;
  }
}
