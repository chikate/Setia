<template>
  <div class="flex flex-column border-round justify-content-center">
    <div
      class="flex flex-row justify-content-between align-items-center cursor-pointer"
    >
      <i :class="[icon, 'p-3']" @click="$emit('iconClick')" />
      {{ label }}
      <i class="pi pi-ellipsis-v p-3" @click="$emit('detailsClick')" />
    </div>
    <slot />
  </div>
</template>

<script setup lang="ts">
defineEmits(["detailsClick", "iconClick"]);
const label = defineModel("label", { type: String });
const icon = defineModel("icon", { type: String });
</script>
