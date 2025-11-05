<template>
  <AuthForm v-model:input="input" @submit="authService.recoverAccount" />
</template>

<script setup lang="ts">
const input = ref({ email: "" });
</script>
