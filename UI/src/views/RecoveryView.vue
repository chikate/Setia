<template>
  <main>
    <div class="flex flex-row gap-6">
      <PasswordRecoveryComponent />
      <UsernameRecoveryComponent />
    </div>
  </main>
</template>

<script setup lang="ts">
import PasswordRecoveryComponent from '@/components/recovery/PasswordRecoveryComponent.vue'
import UsernameRecoveryComponent from '@/components/recovery/UsernameRecoveryComponent.vue'
</script>
