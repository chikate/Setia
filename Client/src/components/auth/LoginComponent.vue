<template>
  <AuthForm v-model:input="input" @successful="AuthApi.apiAuthLoginPost" />
</template>

<script setup lang="ts">
const input = ref({ username: "", password: "" });
</script>
