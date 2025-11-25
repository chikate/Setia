<template>
  <AuthForm
    v-model:input="input"
    :submitFunction="
      () => new AuthApi().apiAuthRecoverAccountGet({ email: input.email })
    "
  />
</template>

<script setup lang="ts">
const input = ref({ email: "" });
</script>
