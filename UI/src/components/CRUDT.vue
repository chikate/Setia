<script setup lang="ts">
import { DEFAULT_ROWS_OPTIONS, DEFAULT_ROWS_INDEX } from '@/constants'
import { FilterMatchMode } from 'primevue/api'

const props = defineProps({
  store: { type: Object, required: true },
  readonly: { type: Boolean, required: false }
})

const showDialog = ref<boolean>()
const showMultipleDelete = ref<boolean>(false)
const showFilters = ref<boolean>(false)
const expandedRows = ref()
const editOrAdd = ref<boolean>(false)
const selectedColumns = ref<{ field: string; header: string; type: string }[]>([])

const keys = ref(Object.keys(props.store.getDefaults()))
const values = ref(keys.value.map((key) => props.store.getDefaults()[key]))

const exposedData = ref(
  keys.value
    .filter(
      (param: string) => param.toLowerCase() != 'deleted' && param.toLowerCase() != 'password'
      // && param.toLowerCase() != 'author' &&
      // param.toLowerCase() != 'executiondate'
    )
    .map((elem: string, i) => ({
      field: elem,
      header:
        elem[0].toUpperCase() +
        elem
          .substring(1)
          .replaceAll(/([A-Z])/g, ' $1')
          .toLowerCase()
          .replaceAll(' data', ''),
      type: values.value[i]
    }))
)

onBeforeMount(async () => {
  exposedData.value.forEach(async (elem, index) => {
    if (typeof elem.type == typeof Object() && !Array.isArray(elem.type)) {
      exposedData.value.splice(index, 1)
      Object.keys(props.store.getDefaults()[elem.field]).forEach(async (e) => {
        exposedData.value.push({
          field: elem.field + '.' + e,
          header:
            e[0].toUpperCase() +
            e
              .substring(1)
              .replaceAll(/([A-Z])/g, ' $1')
              .toLowerCase()
              .replaceAll(' data', ''),
          type: props.store.getDefaults()[elem.field][e]
        })
      })
    }
  })
})

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
  <div :id="store.$id" class="flex justify-content-center h-full w-full">
    <DataTable
      @vue:before-mount="store.get()"
      :value="store.allLoadedItems"
      :loading="!store.allLoadedItems"
      stripedRows
      rowHover
      scrollable
      scrollHeight="flex"
      size="small"
      class="flex-grow-1 border-round p-2"
      style="height: 80vh; max-width: 70vw"
      v-model:selection="(store as any).selectedItems"
      v-model:expandedRows="expandedRows"
      v-model:filters="filters"
      :filterDisplay="showFilters ? 'row' : undefined"
      :globalFilterFields="[exposedData[0]?.field]"
      :paginator="store.allLoadedItems?.length > DEFAULT_ROWS_OPTIONS[DEFAULT_ROWS_INDEX]"
      :rows="DEFAULT_ROWS_OPTIONS[DEFAULT_ROWS_INDEX]"
      :rowsPerPageOptions="DEFAULT_ROWS_OPTIONS"
      :totalRecords="store.allLoadedItems?.length ?? 0"
      reorderableColumns
      @row-dblclick="showDialog = !showDialog && !readonly"
      @row-click="
        ((store as any).editItem = $event.data), (editOrAdd = true), $emit('rowClick', $event)
      "
    >
      <!-- @row-click="(expandedRows[$event.index] = true), console.log(expandedRows)" -->
      <template #header>
        <div class="flex flex-row gap-2 align-items-end px-2">
          <h2 class="w-full m-0 p-0 font-bold">
            {{
              store.$id[0].toUpperCase() +
              store.$id
                .substring(1)
                .replaceAll(/([A-Z])/g, ' $1')
                .toLowerCase()
            }}
          </h2>
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
            v-if="!readonly"
            @click="
              showMultipleDelete
                ? (store.delete(), $emit('deleteClick', $event))
                : (store.resetToDefaults(),
                  (editOrAdd = false),
                  $emit('addClick', $event),
                  (showDialog = !showDialog))
            "
            :model="[
              {
                label: showMultipleDelete ? 'Cancel multiple select' : 'Multiple select',
                icon: 'pi pi-th-large',
                command: () => {
                  ;(showMultipleDelete = !showMultipleDelete), props.store?.resetToDefaults
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
        <div class="flex-grow-1 text-center">
          Empty
          {{ store.$id.replaceAll(/([A-Z])/g, ' $1').toLowerCase() }}
        </div>
      </template>
      <template #expansion>
        <slot name="expansion" />
      </template>
      <Column
        v-if="showMultipleDelete && !(selectedColumns?.length == exposedData?.length)"
        selectionMode="multiple"
        class="pr-0 pt-0"
        style="width: 1px"
        key="select"
      />
      <!-- <Column
        v-if="!(selectedColumns.length == exposedData?.length) && $slots.expansion"
        expander
        class="pr-0"
        style="width: 1px"
        key="expander"
      /> -->

      <Column
        v-if="!(selectedColumns?.length == exposedData?.length)"
        style="width: 1px"
        header="#"
        headerClass="column-text-center "
        class="text-center"
        key="index"
        frozen
      >
        <template #body="{ index }">
          {{ index + 1 }}
        </template>
      </Column>

      <Column
        v-for="(col, i) in exposedData?.filter((item: any) => !selectedColumns.includes(item))"
        :key="col.field + '_' + i"
        :header="col.header"
        :field="col.field"
        :filterField="col.field"
        :show-clear-button="false"
        :class="typeof col.type == typeof Boolean() ? 'text-center' : ''"
        :style="typeof col.type == typeof Boolean() ? 'width: 5rem' : ''"
        :headerClass="typeof col.type == typeof Boolean() ? 'column-text-center' : ''"
        :frozen="i == 0"
      >
        <template #body="{ data, field }">
          <i
            v-if="Boolean(typeof data[field] == typeof Boolean())"
            :class="`pi ${data[field] ? 'pi-verified' : 'pi-circle'}`"
          />
          <div v-else>
            {{
              (() => {
                const value = field.split('.').reduce((acc, part) => acc && acc[part], data)
                return value?.toString().split('T')[1]?.endsWith('Z')
                  ? new Date(value)?.toUTCString()?.replaceAll(' GMT', '') ?? value?.toString()
                  : value?.toString()
              })()
            }}
          </div>
        </template>
        <template #filter="{ filterModel, filterCallback }">
          <InputText
            v-model="filterModel.value"
            type="text"
            @input="filterCallback"
            class="p-column-filter"
            placeholder="Search by country"
          />
        </template>
      </Column>

      <!-- <Column
        v-if="
          !(selectedColumns.length == exposedData?.length) && store.allLoadedItems
            ? store.allLoadedItems[0]?.active
            : false
        "
        header="Active"
        field="Active"
        filterField="Active"
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
      </Column> -->
    </DataTable>
    <Dialog
      class="p-fluid"
      position="top"
      v-model:visible="showDialog"
      modal
      :draggable="false"
      :closable="false"
      style="max-width: 50vw"
    >
      <template #header>
        <h3 class="flex-grow-1 font-bold m-0 p-0">
          {{ (!editOrAdd ? 'Add new ' : 'Edit ') + store.$id.toLocaleLowerCase() }}
        </h3>
        <div class="flex flex-row gap-3 font-bold">
          <Button
            class="bg-primary-reverse"
            label="Back"
            @click="(showDialog = false), store.get()"
          />
          <SplitButton
            v-if="editOrAdd"
            label="Save"
            @click="store.update().then(() => (showDialog = false))"
            :model="[
              {
                label: 'Delete',
                icon: 'pi pi-trash',
                command: () => store.delete().then(() => (showDialog = false))
              }
            ]"
          />
          <Button
            v-if="!editOrAdd"
            label="Add"
            @click="store.add().then(() => (showDialog = false))"
          />
        </div>
      </template>
      <div class="flex flex-column gap-4">
        <InputGroup v-for="key in exposedData" :key="key.field">
          <InputGroupAddon class="py-0 text-left" style="min-width: 7rem">
            {{ key.header }}
          </InputGroupAddon>

          <div class="flex flex-column" v-if="key.field.toLowerCase().includes('tag')">
            <Chips
              :placeholder="key.header"
              v-model="(store as any).editItem[key.field]"
              class="w-full"
              filter
            />
            <Listbox
              :placeholder="`Select ${key.header}`"
              v-model="(store as any).editItem[key.field]"
              :options="useTagsCRUDStore().allLoadedItems"
              option-label="tag"
              option-value="tag"
              multiple
              :filter="(useTagsCRUDStore()?.allLoadedItems?.length ?? 0) > 20"
              class="w-full"
              listStyle="height:20.28rem"
            />
            <!-- :virtualScrollerOptions="{ itemSize: 38 }" -->
          </div>

          <Dropdown
            v-else-if="key.field.toLowerCase() == 'user'"
            :placeholder="`Select a ${key.header}`"
            v-model="(store as any).editItem[key.field]"
            :options="useUsersCRUDStore().allLoadedItems"
            optionLabel="username"
            optionValue="username"
            editable
            filter
          />

          <InputNumber
            v-else-if="Boolean(typeof key.type == typeof Number())"
            v-model="(store as any).editItem[key.field]"
            :placeholder="key.header"
          />

          <Password
            v-else-if="key.field.toLowerCase() == 'password'"
            v-model="(store as any).editItem[key.field]"
            :placeholder="key.header"
          />

          <Button
            v-else-if="Boolean(typeof key.type == typeof Boolean())"
            class="p-0 surface-hover border-gray-700 w-full"
          >
            <Checkbox
              class="p-2 w-full h-full justify-content-end align-items-center"
              v-model="(store as any).editItem[key.field]"
              :placeholder="key.header"
              binary
            />
          </Button>

          <InputText
            v-else
            v-model="(store as any).editItem[key.field]"
            :placeholder="key.header"
          />

          <!-- <Calendar
              v-else-if="
                new RegExp(
                  '\d{1,4}[-./]\d{1,2}[-./]\d{2,4}(?:\.\d+)?Z?',
                  store.editItem[field]
                )
              "
              @vue:beforeMount="
                (store as any).editItem[field] = new Date(
                  store.editItem[field]
                ).toLocaleString()
              "
              @update:modelValue="
                (store as any).editItem[field] = $event ? $event : undefined
              "
              v-model="(store as any).editItem[field]"
              showTime
              hourFormat="24"
              :placeholder="field"
            /> -->
        </InputGroup>
      </div>
    </Dialog>
  </div>
</template>

<style scoped>
::v-deep(.p-datatable-tbody) {
  cursor: alias;
}

::v-deep(ul) {
  flex-wrap: wrap !important;
}
</style>
