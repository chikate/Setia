<script setup lang="ts">
defineProps({
  title: { type: String, required: true },
  text: { type: String, required: true }
})
</script>

<template>
  <div class="flex flex-row">
    <Button :label="title" text @click="$emit('clickMain')" />
    <Button icon="pi pi-check" text @click="$emit('cliclAccept')" />
    <Button icon="pi pi-times" text @click="$emit('clickCancel')" />
  </div>
</template>
