<template>
  <AuthForm
    v-model:input="input"
    :submitFunction="() => new AuthApi().apiAuthRegisterGet(input)"
  />
</template>

<script setup lang="ts">
const input = ref({
  username: "",
  email: "",
  password: "",
  repeatPassword: "",
});
</script>
