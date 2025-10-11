<template>
  <AuthForm
    v-model:input="input"
    :submitFunction="AuthApi.apiAuthRegisterGet"
  />
</template>

<script setup lang="ts">
const input = ref({ email: "" });
</script>
