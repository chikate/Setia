<template>
  <main>
    <div
      class="bg-cover bg-center"
      style="
        background-image: url('https://www.riotgames.com/darkroom/2000/6a2fc4addd2ad4793a915202395c8410:a762ac65c1b95d37880e5e267029cc38/son-riot-website.png');
      "
    >
      <main style="background: linear-gradient(rgba(23, 23, 23, 0) 20%, rgba(23, 23, 23, 1))">
        <a class="text-8xl text-center font-bold">
          Hello {{ useAuthStore()?.getToken() ? 'Dragos' : 'there' }}
        </a>
        <a class="text-3xl text-center font-bold">We are happy that you are here!</a>
      </main>
    </div>
  </main>
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

<script setup lang="ts">
import { useAuthStore } from '@/stores/AuthStore'
</script>
