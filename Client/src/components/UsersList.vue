<template>
  <DataTable :value="usersCRUDService.loadedUsers">
    <Column field="avatar"> </Column>
    <Column field="name" />
    <Column />
  </DataTable>
</template>

<script lang="ts" setup>
import { Column } from "primevue";

const usersIds = defineModel("usersIds", { type: String });

onBeforeMount(init);
async function init() {
  usersCRUDService.loadItems();
}
</script>
