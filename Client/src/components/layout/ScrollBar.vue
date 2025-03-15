<script setup lang="ts">
// const scrollPercentage = defineModel('scrollPercentage', {
//   type: Number,
//   required: false,
//   default: 0
// })
</script>

<template>
  <RouterView
    class="pointer-events-none select-none"
    style="transform: scale(0.2); height: 100vh"
  />
</template>
