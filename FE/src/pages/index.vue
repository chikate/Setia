<script setup lang="ts">
import { LIGHT_THEME_BACKGROUND_IMAGE, DARK_THEME_BACKGROUND_IMAGE } from '@/constants'
const backgroundLink = computed(() =>
  localStorage.getItem('theme') == 'dark'
    ? DARK_THEME_BACKGROUND_IMAGE
    : LIGHT_THEME_BACKGROUND_IMAGE
)
</script>

<template>
  <div
    v-if="useAuthStore()?.userData?.username"
    class="bg-cover bg-center h-full"
    :style="`background-image: url(${backgroundLink})`"
  >
    <div style="background: linear-gradient(rgba(23, 23, 23, 0) 20%, rgba(23, 23, 23, 1))">
      <a class="text-8xl text-center font-bold h-full">
        Hello {{ useAuthStore()?.userData?.username ?? 'there' }}
      </a>
      <a class="text-3xl text-center font-bold">We are happy that you are here!</a>
    </div>
  </div>
  <div v-else class="flex justify-content-around w-full">
    <LoginComponent />
  </div>
</template>

<style>
.p-treeselect-label {
  white-space: wrap !important;
  flex-wrap: wrap !important;
  display: flex !important;
  row-gap: 0.25rem !important;
}
.p-treeselect.p-treeselect-chip .p-treeselect-token {
  margin-right: 0.25rem;
}
.p-inputwrapper-filled.p-treeselect.p-treeselect-chip .p-treeselect-label {
  padding: 0.25rem;
}
</style>
