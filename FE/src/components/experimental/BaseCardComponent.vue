<template>
  <div class="flex-column">
    <div class="flex-row">
      <Button style="min-width: 32px" text :icon @click="$emit('iconClick')" />
      <Button style="max-height: 64px" class="w-full overflow-auto" text :label />
      <Button style="min-width: 32px" text icon="pi pi-ellipsis-v" @click="$emit('detailsClick')" />
    </div>
    <slot />
    <!-- <Button style="max-height: 64px" class="w-full overflow-auto" text :label /> -->
  </div>
</template>

<script setup lang="ts">
defineEmits(['detailsClick', 'iconClick'])
const label = defineModel('label', {
  type: String,
  required: false,
  default: ''
})
const icon = defineModel('icon', {
  type: String,
  required: false,
  default: ''
})
</script>
