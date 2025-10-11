<template>
  <div
    class="card flex flex-row relative bg-gray-700 hover:bg-gray-600 cursor-pointer border-round overflow-hidden"
  >
    <img
      src="https://frankfurt.apollo.olxcdn.com/v1/files/qr0k1ccnla9p2-RO/image;s=1000x700"
      style="object-fit: contain; height: 100px"
    />
    <div class="flex flex-column p-2">
      <label class="h-full">Titlu</label>
      <label>location - date</label>
      <label>area</label>
      <label class="absolute text-right top-0 right-0 p-2">Price</label>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { Column } from "primevue";

const usersIds = defineModel("usersIds", { type: String });

onBeforeMount(init);
async function init() {
  usersCRUDService.loadItems();
}
</script>
