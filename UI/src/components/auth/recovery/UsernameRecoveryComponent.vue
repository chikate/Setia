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

<script setup lang="ts">
const inputEmail = ref<string>('')
</script>
