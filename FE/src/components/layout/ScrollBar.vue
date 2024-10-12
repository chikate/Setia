<script setup lang="ts">
const scrollPercentage = defineModel('scrollPercentage', {
  type: Number,
  required: false,
  default: 0
})
</script>

<template>
  <div class="h-full">
    <RouterView class="select-none" style="opacity: 0.9; transform: scale(0.2)" />
    <div
      style="height: calc(50 * 2) px"
      :style="`transform: translateY(calc(${scrollPercentage * 1}))`"
      class="w-full"
    />
  </div>
</template>
