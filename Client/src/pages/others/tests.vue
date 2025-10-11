<script setup lang="ts">
import { UsersApi } from "@/composables";
</script>

<template>
  <div class="flex flex-column gap-1">
    <CRUDT :service="UsersApi" />
  </div>
</template>
