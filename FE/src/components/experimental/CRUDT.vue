<template>
  <div class="card p-1" style="min-width: 300px">
    <DataTable
      :value="allLoadedItems"
      :loading="!allLoadedItems"
      stripedRows
      rowHover
      scrollable
      scrollHeight="flex"
      size="small"
      v-model:selection="selectedItem"
      v-model:expandedRows="expandedRows"
      :filterDisplay="showFilters ? 'row' : undefined"
      :globalFilterFields="[exposedData[0].field]"
      :paginator="(allLoadedItems?.length ?? 0) > 5"
      :rows="5"
      :rowsPerPageOptions="[5, 10]"
      :totalRecords="allLoadedItems?.length ?? 0"
      reorderableColumns
      @row-dblclick="showDialog = !showDialog && !readonly"
      @row-click="(selectedItem = $event.data), (editOrAdd = true), $emit('rowClick', $event)"
    >
      <template #header>
        <div class="flex-row gap-2 align-items-center px-2">
          <h2 class="w-full m-0 p-0 font-bold">
            {{
              store.storeName[0].toUpperCase() +
              store.storeName
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
          <div><InputText placeholder="Search..." /></div>
          <SplitButton
            v-if="!readonly"
            @click="
              showMultipleDelete
                ? (store.delete(), $emit('deleteClick', $event))
                : ((selectedItem = store.defaultValues),
                  (editOrAdd = false),
                  $emit('addClick', $event),
                  (showDialog = !showDialog))
            "
            :model="[
              {
                label: showMultipleDelete ? 'Cancel multiple select' : 'Multiple select',
                icon: 'pi pi-th-large',
                command: () => (
                  (showMultipleDelete = !showMultipleDelete),
                  (selectedItem = props.store.defaultValues)
                )
              },
              {
                label: 'Collapse all',
                icon: 'pi pi-angle-right',
                command: () => {
                  expandedRows = null
                }
              },
              {
                label: showMultipleDelete ? 'Hide' : 'Show filters',
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
          Empty {{ store.storeName.replaceAll(/([A-Z])/g, ' $1').toLowerCase() }}
        </div>
      </template>
      <template #expansion><slot name="expansion" /></template>
      <Column
        v-if="showMultipleDelete && !(selectedColumns?.length ?? 0 == exposedData?.length ?? 0)"
        selectionMode="multiple"
        class="pr-0 pt-0"
        style="width: 1px"
        key="select"
      />
      <Column
        v-if="!(selectedColumns?.length ?? 0 == exposedData?.length ?? 0)"
        style="width: 1px"
        header="#"
        headerClass="column-text-center"
        class="text-center"
        key="index"
        frozen
      >
        <template #body="{ index }">{{ index + 1 }}</template>
      </Column>
      <Column
        v-for="(col, i) in exposedData.filter((item: any) => !selectedColumns.includes(item))"
        :key="col.field + '_' + i"
        :id="col.field + '_' + i"
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
          <div v-else>{{ data[field].toString() }}</div>
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
          {{ !editOrAdd ? 'Add new ' : 'Edit ' + store.storeName.toLocaleLowerCase() }}
        </h3>
        <div class="flex-row gap-3 font-bold">
          <Button class="bg-primary-reverse" label="Back" @click="showDialog = false" />
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
          <InputGroupAddon style="min-width: 8rem" v-if="selectedItem[key.field]"
            ><div class="py-0 text-right w-full">{{ key.header }}</div></InputGroupAddon
          >
          <AutoComplete
            v-if="
              key.field.toLowerCase().includes('tag') && !(store.storeName.toLowerCase() == 'tags')
            "
            multiple
            :placeholder="key.header"
            v-model="selectedItem[key.field]"
            class="w-full"
            filter
          />
          <Dropdown
            v-else-if="key.field.toLowerCase() == 'user'"
            :placeholder="`Select a ${key.header}`"
            v-model="selectedItem[key.field]"
            :options="useUsersCRUDStore().getItems"
            optionLabel="username"
            optionValue="username"
            editable
            filter
          />
          <InputNumber
            v-else-if="Boolean(typeof key.type == typeof Number())"
            v-model="selectedItem[key.field]"
            :placeholder="key.header"
          />
          <Password
            v-else-if="key.field.toLowerCase() == 'password'"
            v-model="selectedItem[key.field]"
            :placeholder="key.header"
          />
          <Button
            v-else-if="Boolean(typeof key.type == typeof Boolean())"
            class="p-0 surface-hover border-gray-700 w-full"
          >
            <Checkbox
              class="p-2 w-full h-full justify-content-end align-items-center"
              v-model="selectedItem[key.field]"
              :placeholder="key.header"
              binary
            />
          </Button>
          <InputText v-else v-model="selectedItem[key.field]" :placeholder="key.header" />
        </InputGroup>
      </div>
    </Dialog>
  </div>
</template>

<script setup lang="ts">
const props = defineProps({
  store: { type: Object, required: true },
  readonly: { type: Boolean, required: false }
})

const allLoadedItems = ref()
const selectedItem = ref()
const expandedRows = ref()
const showDialog = ref<boolean>(false)
const showMultipleDelete = ref<boolean>(false)
const showFilters = ref<boolean>(false)
const editOrAdd = ref<boolean>(false)
const selectedColumns = ref<{ field: string; header: string; type: string }[]>([])

const keys = ref(Object.keys(props.store.defaultValues))
const values = ref(keys.value.map((key) => props.store.defaultValues[key]))

const exposedData = ref(
  keys.value
    .filter((param) => !['password', 'id', 'executiondate'].includes(param.toLowerCase()))
    .map((elem, i) => ({
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

onBeforeMount(init)
async function init() {
  allLoadedItems.value = await props.store.getItems()
  exposedData.value.forEach(async (elem, index) => {
    if (typeof elem.type == 'object' && !Array.isArray(elem.type) && typeof elem.type != 'string') {
      exposedData.value.splice(index, 1)
      Object.keys(props.store.defaultValues).forEach((key) => {
        if (key.toLowerCase().includes(elem.field.toLowerCase())) {
          exposedData.value.push({
            field: key,
            header:
              key[0].toUpperCase() +
              key
                .substring(1)
                .replaceAll(/([A-Z])/g, ' $1')
                .toLowerCase()
                .replaceAll(' data', ''),
            type: props.store.defaultValues[key]
          })
        }
      })
    }
  })
}
</script>
