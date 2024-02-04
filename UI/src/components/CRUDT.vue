<script setup lang="ts">
import { ROWS, DEFAULT_ROWS_INDEX } from '@/constants'
import { capitalizeString } from '@/helpers'
import { ref } from 'vue'

const props = defineProps(['store'])

const showDialog = ref<boolean>()
const showMultipleDelete = ref<boolean>(false)
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
        param != 'updatedBy'
    )
    .map((elem) => ({
      field: elem,
      header: elem
    }))
)

// add column filters here otherwhise it will brake, we need to make this runtime depending push values
import { FilterMatchMode } from 'primevue/api'
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
  authorityCode: { value: null, matchMode: FilterMatchMode.CONTAINS }
})
</script>

<template>
  <div class="flex justify-content-center">
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
      filterDisplay="row"
      :globalFilterFields="exposedData.field"
      paginator
      :rows="ROWS[DEFAULT_ROWS_INDEX]"
      :rowsPerPageOptions="ROWS"
      :totalRecords="store.allLoadedItems.length"
      style="max-height: 80vh"
      class="flex-grow-1 px-8"
    >
      <template #header>
        <div class="flex flex-wrap gap-2 align-items-center">
          <h4 class="m-0 flex-grow-1">{{ store.$id }}</h4>
          <div style="text-align: left">
            <MultiSelect
              v-model:modelValue="selectedColumns"
              :options="exposedData"
              optionLabel="header"
              display="chip"
              placeholder="Select Columns"
            />
          </div>
          <span class="p-float-label p-input-icon-left">
            <i class="pi pi-search" />
            <InputText v-model="filters['global'].value" placeholder="Search..." />
            <label>Search...</label>
          </span>
          <SplitButton
            rounded
            :icon="showMultipleDelete ? 'pi pi-trash' : 'pi pi-plus'"
            @click="
              showMultipleDelete ? '' : (showDialog = !showDialog), store.setSelectionToDefaults()
            "
            :model="[
              {
                label: showMultipleDelete ? 'Cancel multiple delete' : 'Multiple delete',
                icon: 'pi pi-trash',
                command: () => {
                  ;(showMultipleDelete = !showMultipleDelete), (selectedProduct = [])
                }
              }
            ]"
          />
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
      />
      <Column
        v-if="!(selectedColumns.length == exposedData?.length) && $slots.expansion"
        expander
        class="pr-0"
      />

      <Column v-if="!(selectedColumns.length == exposedData?.length)" style="width: 3rem">
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
          <span class="p-float-label">
            <!-- <Calendar
            v-if="!isNaN(new Date(store.selectedItem[field]).getTime())"
            v-model="store.selectedItem[field]"
            showTime
            hourFormat="24"
          /> -->
            <AutoComplete
              v-if="typeof store.selectedItem[field] == 'object'"
              v-model="store.selectedItem[field]"
              :options="store.selectedItem[field]"
              option-label="name"
              filter
              :placeholder="capitalizeString(field)"
            />
            <InputNumber
              v-else-if="typeof store.selectedItem[field] == 'number'"
              v-model="store.selectedItem[field]"
              :placeholder="capitalizeString(field)"
            />
            <Button
              v-else-if="typeof store.selectedItem[field] == 'boolean'"
              class="p-0 bg-gray-900"
            >
              <Checkbox
                class="p-2 w-full h-full justify-content-end"
                v-model="store.selectedItem[field]"
                :placeholder="capitalizeString(field)"
                binary
              />
            </Button>
            <InputText
              v-else-if="typeof store.selectedItem[field] == 'string'"
              v-model="store.selectedItem[field]"
              :placeholder="capitalizeString(field)"
            />
            <label>{{ capitalizeString(field) }}</label>
          </span>
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