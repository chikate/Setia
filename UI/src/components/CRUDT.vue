<script setup lang="ts">
import { DEFAULT_ROWS_OPTIONS, DEFAULT_ROWS_INDEX } from '@/constants'
import { useAuthStore } from '@/stores/AuthStore'
import { FilterMatchMode } from 'primevue/api'
import { capitalizeString } from '@/helpers'
import { ref } from 'vue'

const props = defineProps(['store'])

const showDialog = ref<boolean>()
const showMultipleDelete = ref<boolean>(false)
const showFilters = ref<boolean>(false)
const selectedProduct = ref()
const expandedRows = ref()
const selectedColumns = ref<{ field: string; header: string }[]>([])
const exposedData = ref(
  Object.getOwnPropertyNames(props.store.getDefaults())
    .filter(
      (param: string) =>
        param != 'active' &&
        param != 'deleted' &&
        param != 'createdBy' &&
        param != 'id_createdBy' &&
        param != 'password' &&
        param != 'updatedBy'
    )
    .map((elem) => ({
      field: elem,
      header: elem
    }))
)

// add column filters here otherwhise it will brake, we need to make this runtime depending push values
const filters = ref({
  global: { value: null, matchMode: FilterMatchMode.CONTAINS },
  'user.email': { value: null, matchMode: FilterMatchMode.CONTAINS },
  active: { value: null, matchMode: FilterMatchMode.EQUALS },
  email: { value: null, matchMode: FilterMatchMode.CONTAINS },
  beginTime: { value: null, matchMode: FilterMatchMode.CONTAINS },
  endTime: { value: null, matchMode: FilterMatchMode.CONTAINS },
  description: { value: null, matchMode: FilterMatchMode.CONTAINS },
  username: { value: null, matchMode: FilterMatchMode.CONTAINS },
  name: { value: null, matchMode: FilterMatchMode.CONTAINS },
  statusCode: { value: null, matchMode: FilterMatchMode.CONTAINS },
  authorityCode: { value: null, matchMode: FilterMatchMode.CONTAINS },
  rights: { value: null, matchMode: FilterMatchMode.EQUALS }
})
</script>

<template>
  <!-- @row-click="(expandedRows[$event.index] = true), console.log(expandedRows)" -->
  <div class="flex justify-content-center" v-if="useAuthStore().getToken()">
    <DataTable
      @vue:before-mount="store.getAll()"
      :value="store.allLoadedItems"
      @row-dblclick="(store.selectedItem = $event.data), (showDialog = !showDialog)"
      size="small"
      stripedRows
      scrollable
      scrollHeight="flex"
      rowHover
      dataKey="id"
      v-model:selection="selectedProduct"
      v-model:expandedRows="expandedRows"
      v-model:filters="filters"
      :filterDisplay="showFilters ? 'row' : undefined"
      :globalFilterFields="exposedData.field"
      paginator
      :rows="DEFAULT_ROWS_OPTIONS[DEFAULT_ROWS_INDEX]"
      :rowsPerPageOptions="DEFAULT_ROWS_OPTIONS"
      :totalRecords="store.allLoadedItems.length"
      style="max-height: 80vh"
      class="flex-grow-1 px-8"
      reorderableColumns
    >
      <template #header>
        <div class="flex flex-row gap-2 align-items-center">
          <h4 class="m-0 w-full">{{ store.$id }}</h4>
          <div v-if="showFilters" style="text-align: left">
            <MultiSelect
              v-model:modelValue="selectedColumns"
              :options="exposedData"
              optionLabel="header"
              display="chip"
              placeholder="Hide columns"
            />
          </div>
          <div>
            <InputText v-model="filters['global'].value" placeholder="Search..." />
          </div>
          <SplitButton
            @click="
              showMultipleDelete ? '' : (showDialog = !showDialog), store.setSelectionToDefaults()
            "
            :model="[
              {
                label: showMultipleDelete ? 'Cancel multiple select' : 'Multiple select',
                icon: 'pi pi-th-large',
                command: () => {
                  ;(showMultipleDelete = !showMultipleDelete), (selectedProduct = [])
                }
              },
              {
                label: 'Collapse all',
                icon: 'pi pi-angle-right',
                command: () => {
                  expandedRows = null
                }
              },
              {
                label: showMultipleDelete ? 'Hide' : 'Show' + ' filters',
                icon: showMultipleDelete ? 'pi pi-filter' : 'pi-filter-slash',
                command: () => {
                  showFilters = !showFilters
                }
              }
            ]"
          >
            <i :class="showMultipleDelete ? 'pi pi-trash' : 'pi pi-plus'" />
          </SplitButton>
        </div>
      </template>
      <template #empty>
        <div class="flex-grow-1 text-center">No {{ store.$id.toLowerCase() }}</div>
      </template>
      <template #expansion>
        <slot name="expansion" />
      </template>

      <Column
        v-if="showMultipleDelete && !(selectedColumns.length == exposedData?.length)"
        selectionMode="multiple"
        class="pr-0 pt-0"
        style="width: 1%"
        key="select"
      />
      <Column
        v-if="!(selectedColumns.length == exposedData?.length) && $slots.expansion"
        expander
        class="pr-0"
        style="width: 1%"
        key="expander"
      />

      <Column
        v-if="!(selectedColumns.length == exposedData?.length)"
        style="width: 3rem"
        key="index"
      >
        <template #body="slotProps">
          {{ slotProps.index + 1 }}
        </template>
      </Column>

      <Column
        v-for="(col, index) in exposedData?.filter((item: any) => !selectedColumns.includes(item))"
        :key="col.field + '_' + index"
        :header="col.header"
        :field="col.field"
        :filterField="col.field"
        :show-clear-button="false"
      >
        <template #filter="{ filterModel, filterCallback }">
          <InputText
            v-model="filterModel.value"
            type="text"
            @input="filterCallback()"
            class="p-column-filter"
            placeholder="Search by country"
          />
        </template>
      </Column>

      <Column
        v-if="!(selectedColumns.length == exposedData?.length)"
        header="Active"
        field="active"
        filterField="active"
        dataType="boolean"
        class="text-center"
        headerClass="column-text-center"
        style="width: 5rem"
        :show-clear-button="false"
      >
        <template #body="slotProps">
          <div class="pi" :class="slotProps.data.active ? 'pi-verified' : 'pi-circle'" />
        </template>
        <template #filter="{ filterModel, filterCallback }">
          <TriStateCheckbox v-model="filterModel.value" @change="filterCallback()" />
        </template>
      </Column>
    </DataTable>
    <Dialog
      class="p-fluid"
      position="top"
      v-model:visible="showDialog"
      :header="
        (store.selectedItem.id ?? 0 <= 0 ? 'Add new ' : 'Edit ') + store.$id.toLocaleLowerCase()
      "
      modal
      closable
      :draggable="false"
    >
      <div class="flex flex-column gap-4">
        <div v-for="field in Object.getOwnPropertyNames(store.getDefaults())" :key="field">
          <InputGroup>
            <InputGroupAddon class="py-0" style="min-width: 7rem">
              {{ capitalizeString(field).replaceAll('_id', '') }}
            </InputGroupAddon>
            <Dropdown
              v-if="typeof store.selectedItem[field] == 'object' && field.includes('_id')"
              v-model="store.selectedItem[field]"
              :placeholder="capitalizeString(field).replaceAll('_id', '')"
              filter
              :options="[
                { name: 'asd', id: 0 },
                { name: 'asdas', id: 1 }
              ]"
              option-label="name"
              option-value="id"
            />
            <AutoComplete
              v-else-if="typeof store.selectedItem[field] == 'object'"
              v-model="store.selectedItem[field]"
              :placeholder="capitalizeString(field)"
              filter
              :options="[
                { name: 'asd', id: 0 },
                { name: 'asdas', id: 1 }
              ]"
              option-label="name"
              option-value="id"
            />
            <InputNumber
              v-else-if="typeof store.selectedItem[field] == 'number'"
              v-model="store.selectedItem[field]"
              :placeholder="capitalizeString(field)"
            />
            <Button
              v-else-if="typeof store.selectedItem[field] == 'boolean'"
              class="p-0 bg-gray-900 border-gray-700 w-full"
            >
              <Checkbox
                class="p-2 w-full h-full justify-content-end"
                v-model="store.selectedItem[field]"
                :placeholder="capitalizeString(field)"
                binary
              />
            </Button>
            <Calendar
              v-else-if="!isNaN(new Date(store.selectedItem[field]).getTime())"
              @vue:beforeMount="
                store.selectedItem[field] = new Date(store.selectedItem[field]).toLocaleString()
              "
              @update:modelValue="
                store.selectedItem[field] = $event
                  ? useDateFormat($event, DD - MM - YYYY).value
                  : undefined
              "
              v-model="store.selectedItem[field]"
              showTime
              hourFormat="24"
            />
            <InputText
              v-else-if="typeof store.selectedItem[field] == 'string'"
              v-model="store.selectedItem[field]"
              :placeholder="capitalizeString(field)"
            />
          </InputGroup>
        </div>

        <div class="flex flex-row gap-3 font-bold">
          <Button class="flex-grow-1 bg-primary-reverse" label="Back" @click="showDialog = false" />
          <Button
            class="flex-grow-1"
            :label="store.selectedItem.id ?? 0 > 0 ? 'Save' : 'Add'"
            @click="
              (showDialog = false), store.selectedItem.id ?? 0 > 0 ? store.update() : store.add()
            "
          />
          <Button
            v-if="store.selectedItem.id ?? 0 > 0"
            class="flex-grow-1 bg-red-500 text-white"
            label="Delete"
            @click="(showDialog = false), store.delete()"
          />
        </div>
      </div>
    </Dialog>
  </div>
</template>

<style scoped>
::v-deep(.p-datatable-tbody) {
  cursor: alias;
}
</style>
