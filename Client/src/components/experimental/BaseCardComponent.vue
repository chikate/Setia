<template>
  <div class="flex flex-column overflow-hidden border-round gap-2">
    <div
      class="flex flex-row justify-content-between align-items-center cursor-pointer"
    >
      <i :class="[icon, 'p-2']" @click="$emit('iconClick')" />
      {{ label }}
      <i class="pi pi-ellipsis-v p-2" @click="$emit('detailsClick')" />
    </div>
    <slot />
  </div>
</template>

<script setup lang="ts">
defineEmits(["detailsClick", "iconClick"]);
const label = defineModel("label", { type: String });
const icon = defineModel("icon", { type: String });
</script>
