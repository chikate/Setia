<script setup lang="ts">
const search = ref()
</script>

<template>
  <main
    class="p-8"
    style="background: linear-gradient(rgba(23, 23, 23, 0) 20%, rgba(23, 23, 23, 1))"
  >
    <!-- <div class="bg-cover bg-center"> -->
    <!-- style="background-image: url('https://localhost:44381/1/path36.png')" -->
    <div class="flex flex-column align-items-center">
      <a class="text-7xl font-bold">SEARCH PROFILES</a>
      <InputText v-model="search" @keypress.enter="$router.push(`/profile/${search}`)" />
    </div>
  </main>
</template>
