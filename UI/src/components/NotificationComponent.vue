<template>
  <div class="flex flex-row">
    <Button :label="title" text @click="$emit('clickMain')" />
    <Button icon="pi pi-check" text @click="$emit('cliclAccept')" />
    <Button icon="pi pi-times" text @click="$emit('clickCancel')" />
  </div>
</template>

<script setup lang="ts">
const props = defineProps({
  title: { type: String, required: true },
  text: { type: String, required: true }
})
</script>

<style scoped></style>
