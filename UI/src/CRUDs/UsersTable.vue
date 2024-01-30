<template>
  <div>
    <CRUDT :store="useUsersStore()" :exposedData="[{ field: 'email' }, { field: 'name' }]"> </CRUDT>
  </div>
</template>

<script setup lang="ts">
import { useUsersStore } from '@/CRUDs/UsersStore'
</script>
