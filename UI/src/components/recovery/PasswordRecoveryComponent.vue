<template>
  <div class="flex flex-column gap-2">
    <lable class="mb-2 flex">Recover password</lable>

    <span class="p-float-label">
      <InputText v-model="inputEmail" />
      <label>Email</label>
    </span>

    <span class="p-float-label">
      <InputText v-model="inputUsername" />
      <label>Username</label>
    </span>

    <div class="mt-2 flex">
      <Button label="Send link" />
    </div>
  </div>
</template>

<script setup lang="ts">
import InputText from 'primevue/inputtext'
import Button from 'primevue/button'
import { ref } from 'vue'

const inputEmail = ref<string>()
const inputUsername = ref<string>()
</script>
