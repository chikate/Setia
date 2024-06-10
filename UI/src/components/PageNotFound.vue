<script setup lang="ts">
import { LIGHT_THEME_BACKGROUND_IMAGE, DARK_THEME_BACKGROUND_IMAGE } from '@/constants'
const backgroundLink = computed(() =>
  localStorage.getItem('theme') == 'dark'
    ? DARK_THEME_BACKGROUND_IMAGE
    : LIGHT_THEME_BACKGROUND_IMAGE
)
</script>

<template>
  <main>
    <div class="bg-cover bg-center" :style="`background-image: url(${backgroundLink})`">
      <main style="background: linear-gradient(rgba(23, 23, 23, 0) 20%, rgba(23, 23, 23, 1))">
        <a class="text-8xl font-bold">404</a>
        <a class="text-7xl font-bold">PAGE NOT FOUND</a>
        <a class="text-3xl font-medium">You've reached unwanted teritories!</a>
      </main>
    </div>
  </main>
</template>
