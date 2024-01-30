<template>
  <div class="flex justify-content-center">
    <DataTable
      @vue:before-mount="store.getAll()"
      :value="store.allLoadedItems"
      :globalFilterFields="['beginTime', 'endTime', 'active']"
      size="small"
      stripedRows
      scrollable
      :scroll-height="ROWS[DEFAULT_ROWS_INDEX] * 2.65 + 'rem'"
      rowHover
      paginator
      :rows="ROWS[DEFAULT_ROWS_INDEX]"
      :rowsPerPageOptions="ROWS"
      :totalRecords="store.allLoadedItems.length"
      @row-dblclick="(store.selectedItem = $event.data), (showDialog = !showDialog)"
    >
      <template #header>
        <div class="flex flex-wrap gap-2 align-items-center">
          <h4 class="m-0 flex-grow-1">{{ name }}</h4>
          <span class="p-float-label p-input-icon-left">
            <i class="pi pi-search" />
            <InputText placeholder="Search..." />
            <label>Search...</label>
          </span>
          <SplitButton
            rounded
            :icon="false ? 'pi pi-trash' : 'pi pi-plus'"
            @click="false ? '' : (showDialog = !showDialog), store.setSelectionToDefaults()"
            :model="[
              {
                label: 'Multiple delete',
                icon: 'pi pi-trash',
                command: () => {}
              }
            ]"
          />
        </div>
      </template>
      <template #empty>
        <div class="flex-grow-1 text-center">No {{ name.toLowerCase() }}</div>
      </template>

      <Column
        field=""
        header="#"
        style="width: 3.5rem"
        class="text-center"
        headerClass="column-text-center"
      >
        <template #body="slotProps">
          {{ slotProps.index + 1 }}
        </template>
      </Column>

      <Column
        v-for="data in exposedData"
        :key="data"
        :field="data.field"
        :header="data.header ?? capitalizeString(data.field)"
      >
        <!-- <template #filter="{ filterModel, filterCallback }">
          <Dropdown
            v-model="filterModel.value"
            @change="filterCallback()"
            placeholder="Select One"
            class="p-column-filter"
            style="min-width: 12rem"
            :showClear="true"
          >
            <template #option="slotProps">
              <Tag :value="slotProps.option" />
            </template>
          </Dropdown>
        </template> -->
      </Column>

      <Column
        field="active"
        header="Active"
        style="width: 4rem"
        class="text-center"
        headerClass="column-text-center"
      >
        <template #body="slotProps">
          <div class="pi" :class="slotProps.data.active ? 'pi-verified' : 'pi-circle'" />
        </template>
      </Column>
    </DataTable>
    <Dialog
      class="p-fluid"
      position="top"
      v-model:visible="showDialog"
      :header="(store.selectedItem.id ?? 0 <= 0 ? 'Add new ' : 'Edit ') + name.toLocaleLowerCase()"
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

<script setup lang="ts">
import { ROWS, DEFAULT_ROWS_INDEX } from '@/constants'
import { capitalizeString } from '@/helpers'
import { ref } from 'vue'

const props = defineProps(['store', 'exposedData'])

const name = ref<string>(props.store.$id)
const showDialog = ref<boolean>()
</script>

<style scoped>
::v-deep(.p-datatable-tbody) {
  cursor: alias;
}
</style>
