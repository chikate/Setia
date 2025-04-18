<template>
  <!-- style="background: linear-gradient(rgba(23, 23, 23, 0) 20%, rgba(23, 23, 23, 1))" -->
  <div class="flex-column justify-content-center gap-8 align-items-center">
    <div class="flex-column text-center"></div>
    <DataTable
      v-if="loadedItems[0]"
      :value="loadedItems"
      paginator
      :rows-per-page-options="[10, 20]"
      :rows="10"
    >
      <Column
        v-for="field in Object.keys(loadedItems[0])"
        :key="field"
        :field
        :header="capitalizeString(field)"
      >
        <template #body="{ data, field }">
          {{
            isNaN(data[field]) ? data[field].toString() : parseInt(data[field])
          }}
        </template>
      </Column>
    </DataTable>
    <div class="flew-wrap w-full h-full">
      <InventorySlot v-for="item in items" :key="item.name" />
    </div>
  </div>
</template>

<script setup lang="ts">
const loadedItems = ref([{ name: "Test", value: 1 }]);
const items = ref([{ name: "Test", value: 1 }]);

onBeforeMount(init);
async function init() {}
</script>

<style scoped>
.p-treeselect-label {
  white-space: wrap !important;
  flex-wrap: wrap !important;
  display: flex !important;
  row-gap: 0.25rem !important;
}
.p-treeselect.p-treeselect-chip .p-treeselect-token {
  margin-right: 0.25rem;
}
.p-inputwrapper-filled.p-treeselect.p-treeselect-chip .p-treeselect-label {
  padding: 0.25rem;
}
</style>
