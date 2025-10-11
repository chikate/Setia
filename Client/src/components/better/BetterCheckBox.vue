<script setup lang="ts">
const model = defineModel("model", {
  type: Boolean,
  required: false,
  default: false,
});
const label = defineModel("label", {
  type: String,
  required: false,
  default: "",
});
</script>

<template>
  <div
    class="cursor-pointer flex flex-row gap-2 p-2 border-round"
    @click="model = !model"
  >
    <div class="flex-grow-1">{{ label }}</div>
    <Checkbox class="pointer-events-none border-round" binary v-model="model" />
  </div>
</template>
