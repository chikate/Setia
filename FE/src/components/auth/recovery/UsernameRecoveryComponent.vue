<script setup lang="ts">
const inputEmail = defineModel('inputEmail', {
  type: String,
  required: false,
  default: ''
})
</script>

<template>
  <div class="flex flex-column gap-2">
    <lable class="mb-2 flex">Recover usernames</lable>

    <FloatLabel>
      <InputText v-model="inputEmail" class="w-full" />
      <label>Email</label>
    </FloatLabel>

    <div class="mt-2 flex">
      <Button label="Send link" />
    </div>
  </div>
</template>
