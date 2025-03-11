<template>
  <div>
    <DataTable :value="players" paginator :rows="5" :rowsPerPageOptions="[5, 10, 20, 50]">
      <template #header> Players leaderboard </template>
      <Column header="#">
        <template #body="slotProps">
          {{ slotProps.index + 1 }}
        </template>
      </Column>
      <Column field="name" header="Name" sortable />
      <Column field="level" header="Level" sortable />
      <Column field="guild" header="Guild" sortable />
    </DataTable>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
const players = ref([
  { name: 'Dragos', level: 100, guild: 'Gods' },
  { name: 'Gaby', level: 90, guild: 'Gods' },
  { name: 'DariusNoLifer', level: 1000, guild: 'Gods' }
])
</script>
