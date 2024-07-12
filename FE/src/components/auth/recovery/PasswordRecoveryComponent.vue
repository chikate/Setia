<template>
  <div class="flex flex-column gap-2">
    <lable class="mb-2 flex">Recover password</lable>
    <FloatLabel>
      <InputText v-model="inputEmail" />
      <label>Email</label>
    </FloatLabel>
    <FloatLabel>
      <InputText v-model="inputUsername" />
      <label>Username</label>
    </FloatLabel>
    <div class="mt-2 flex">
      <Button label="Send link" />
    </div>
  </div>
</template>

<script setup lang="ts">
const inputEmail = ref<string>('')
const inputUsername = ref<string>('')
</script>
