<template>
  <div class="flex flex-column gap-2 w-full align-items-start">
    <DataTable :value="[{ name: '' }]">
      <Column header="#">
        <template #body="slotProps">
          {{ slotProps.index + 1 }}
        </template>
      </Column>
      <Column header="Name" />
      <Column header="Price" />
      <Column :header="`Last ${7} days`">
        <template #body="{ data, field }">
          {{ data[field] }}
        </template>
      </Column>
    </DataTable>
  </div>
</template>
