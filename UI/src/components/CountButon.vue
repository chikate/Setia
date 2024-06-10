<script setup lang="ts">
const icon = defineModel('icon', { type: String, required: true, default: 'pi-thumbs-up' })
// const count = defineModel('count', { type: Number, required: false, default: 0 })

defineProps({
  count: { type: Number, required: false, default: 0 }
})
</script>

<template>
  <div class="flex flex-row align-items-center column-gap-2 mt-2">
    <div class="pi cursor-pointer" :class="icon" />
    {{ count == 0 ? '' : count ?? '' }}
  </div>
</template>
