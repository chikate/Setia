<template>
  <div>
    <DataTable
      @vue:before-mount="store.getAll()"
      :value="store.allLoadedItems"
      @row-dblclick="(showDialog = !showDialog), (store.selectedItem = $event.data)"
      size="small"
      stripedRows
      scrollable
      :scroll-height="ROWS[1] * 2.653 + 'rem'"
      rowHover
      paginator
      :rows="ROWS[1]"
      :rowsPerPageOptions="ROWS"
      :totalRecords="store.allLoadedItems.length"
      @page="store.getAll()"
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
        </template>
      </Column>
      <Column style="width: 1%" class="text-center" v-if="false">
        <template #body="slotProps">
          <Button
            text
            icon="pi pi-ellipsis-v"
            @click="(store.selectedItem = slotProps.data), opEditMenu.toggle($event)"
          />
        </template>
      </Column>
    </DataTable>
    <OverlayPanel ref="opEditMenu" dismissable>
      <div class="flex flex-column">
        <Button text label="Edit" class="text-teal-500" />
        <Button text label="Audit" class="text-orange-500" />
        <Button text label="Delete" class="text-red-500" @click="store.delete()" />
      </div>
    </OverlayPanel>
    <Dialog
      position="top"
      v-model:visible="showDialog"
      :header="(store.selectedItem.id <= 0 ? 'Add new ' : 'Edit ') + name.toLocaleLowerCase()"
      modal
      closable
      :draggable="false"
    >
      <div class="flex flex-column gap-4">
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
            <Checkbox
              v-if="typeof store.selectedItem[field] == 'boolean'"
              v-model:model-value="store.selectedItem[field]"
              :placeholder="field"
            />
          </div>
        </div>

        <div class="flex flex-wrap gap-3 font-bold">
          <Button class="flex-grow-1 bg-primary-reverse" label="Back" @click="showDialog = false" />
          <Button
            class="flex-grow-1"
            :label="store.selectedItem.id <= 0 ? 'Add' : 'Save'"
            @click="(showDialog = false), store.selectedItem.id <= 0 ? store.add() : store.update()"
          />
          <Button
            v-if="store.selectedItem.id > 0"
            class="flex-grow-1 bg-red-500"
            label="Delete"
            @click="(showDialog = false), store.delete()"
          />
        </div>
      </div>
    </Dialog>
  </div>
</template>

<script setup lang="ts">
import { ROWS } from '@/constants'
import { ref, watch } from 'vue'
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Button from 'primevue/button'
import Dialog from 'primevue/dialog'
import OverlayPanel from 'primevue/overlaypanel'
import InputText from 'primevue/inputtext'
import Calendar from 'primevue/calendar'
import Checkbox from 'primevue/checkbox'

const props = defineProps(['store'])

const opEditMenu = ref()

const showDialog = ref<boolean>()
const name = ref<string>(String(props.store.$id).replaceAll('Store', ''))

watch(
  () => props.store.allLoadedItems,
  () => {
    props.store.getAll()
  }
)
</script>
