<template>
  <div>
    <DataTable
      @vue:mounted="store.getAll()"
      :value="store.allLoadedItems"
      size="small"
      stripedRows
      scrollable
      rowHover
      paginator
      :rows="ROWS[1]"
      :rowsPerPageOptions="ROWS"
      :totalRecords="store.allLoadedItems.length"
    >
      <template #header>
        <div class="flex flex-row align-items-center">
          <div class="flex-grow-1 text-left">{{ name }}</div>
          <Button
            rounded
            icon="pi pi-plus"
            @click="(showDialog = !showDialog), store.resetSelection()"
          />
        </div>
      </template>
      <template #empty>
        <div class="flex-grow-1 text-center">Empty</div>
      </template>

      <Column
        field=""
        header="#"
        style="width: 3%"
        class="text-center"
        headerClass="column-text-center"
      >
        <template #body="slotProps">
          {{ slotProps.index + 1 }}
        </template>
      </Column>

      <slot />

      <Column
        field="active"
        header="Active"
        style="width: 5%"
        class="text-center"
        headerClass="column-text-center"
      >
        <template #body="slotProps">
          <div class="pi" :class="slotProps.data.active ? 'pi-verified' : 'pi-circle'" />
          {{ slotProps.data.active }}
        </template>
      </Column>

      <!-- <Column style="width: 1%" class="text-center">
        <template #body="slotProps">
          <Button
            text
            icon="pi pi-ellipsis-v"
            @click="(store.selectedItem = slotProps.data), opEditMenu.toggle($event)"
          />
        </template>
      </Column> -->
    </DataTable>
    <!-- <OverlayPanel ref="opEditMenu">
      <div class="flex flex-column">
        <Button text label="Edit" class="text-teal-500" />
        <Button text label="Audit" class="text-orange-500" />
        <Button text label="Delete" class="text-red-500" @click="store.delete()" />
      </div>
    </OverlayPanel> -->
    <Dialog
      position="top"
      v-model:visible="showDialog"
      :header="'Edit ' + name"
      modal
      closable
      :draggable="false"
    >
      <div class="flex flex-column gap-2">
        <slot name="dialog" />
        <div v-if="!$slots.dialog">
          <div v-for="field in Object.getOwnPropertyNames(store.selectedItem)" :key="field">
            <Calendar
              v-if="typeof store.selectedItem[field] == 'object'"
              showTime
              hourFormat="24"
              showWeek
              v-model:model-value="store.selectedItem[field]"
            />
            <InputText
              v-if="typeof store.selectedItem[field] == 'string'"
              v-model:model-value="store.selectedItem[field]"
              :placeholder="field"
            />
          </div>
        </div>

        <Button label="Add" @click="(showDialog = false), store.add()" />
      </div>
    </Dialog>
  </div>
</template>

<script setup lang="ts">
import Dialog from 'primevue/dialog'
import DataTable from 'primevue/datatable'
import Button from 'primevue/button'
import Column from 'primevue/column'
// import OverlayPanel from 'primevue/overlaypanel'
import { ROWS } from '@/constants'
import { ref } from 'vue'
import InputText from 'primevue/inputtext'
import Calendar from 'primevue/calendar'

const props = defineProps(['store'])

const showDialog = ref<boolean>()
const name = ref<string>(String(props.store.$id).replaceAll('Store', ''))
</script>

<style>
.column-text-center {
  .p-column-header-content {
    text-align: center !important;
    display: block !important;
  }
}
</style>
