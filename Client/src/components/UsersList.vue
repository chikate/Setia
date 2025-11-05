<template>
  <DataTable :value="[]">
    <Column field="avatar"> </Column>
    <Column field="name" />
    <Column />
  </DataTable>
</template>

<script setup lang="ts">
import { Column } from "primevue";

const usersIds = defineModel("usersIds", { type: String });

onBeforeMount(init);
async function init() {}
</script>
