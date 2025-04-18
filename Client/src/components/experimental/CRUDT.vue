<template>
  <div class="card" style="min-width: 300px">
    <DataTable
      :value="valueitems"
      :loading="!valueitems"
      stripedRows
      rowHover
      scrollable
      scrollHeight="flex"
      size="small"
      v-model:selection="selectedItem"
      v-model:expandedRows="expandedRows"
      :filterDisplay="showFilters ? 'row' : undefined"
      :globalFilterFields="[exposedData[0].field]"
      :paginator="(valueitems?.length ?? 0) > 5"
      :rows="5"
      :rowsPerPageOptions="[5, 10]"
      :totalRecords="valueitems?.length ?? 0"
      reorderableColumns
      @row-dblclick="showDialog = !showDialog"
      @row-click="
        (selectedItem = $event.data),
          (editOrAdd = true),
          $emit('rowClick', $event)
      "
    >
      <template #header>
        <div class="flex-row gap-2 align-items-center px-2">
          <h2 class="m-0 p-0 font-bold">{{ formattedServiceName }}</h2>
          <div v-if="showFilters" style="text-align: left">
            <MultiSelect
              v-model:modelValue="selectedColumns"
              :options="exposedData"
              optionLabel="header"
              display="chip"
              placeholder="Hide columns"
            />
          </div>
          <InputText placeholder="Search..." />
          <SplitButton
            v-if="!readonly"
            @click="handleSplitButtonClick"
            :model="splitButtonModel"
          >
            <i :class="showMultipleDelete ? 'pi pi-trash' : 'pi pi-plus'" />
          </SplitButton>
        </div>
      </template>
      <template #empty>
        <div class="flex-grow-1 text-center">
          Empty
          {{
            service.serviceName?.replaceAll(/([A-Z])/g, " $1")?.toLowerCase()
          }}
        </div>
      </template>
      <template #expansion><slot name="expansion" /></template>
      <Column
        v-if="
          showMultipleDelete && selectedColumns.length !== exposedData.length
        "
        selectionMode="multiple"
        class="pr-0 pt-0"
        style="width: 1px"
        key="select"
      />
      <Column
        v-if="selectedColumns.length !== exposedData.length"
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
        v-for="(col, i) in exposedData"
        :key="col.field + '_' + i"
        :header="col.header"
        :field="col.field"
        :filterField="col.field"
        :class="col.type === 'boolean' ? 'text-center' : ''"
        :style="col.type === 'boolean' ? 'width: 5rem' : ''"
        :headerClass="col.type === 'boolean' ? 'column-text-center' : ''"
        :frozen="i === 0"
      >
        <template #body="{ data, field }">
          <i
            v-if="typeof data[field] === 'boolean'"
            :class="`pi ${data[field] ? 'pi-verified' : 'pi-circle'}`"
          />
          <div v-else>{{ data[field]?.toString() }}</div>
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
          {{ !editOrAdd ? "Add new " : "Edit " + formattedServiceName }}
        </h3>
        <div class="flex-row gap-3 font-bold">
          <Button
            class="bg-primary-reverse"
            label="Back"
            @click="showDialog = false"
          />
          <SplitButton
            v-if="editOrAdd"
            label="Save"
            @click="service.update().then(() => (showDialog = false))"
            :model="[
              {
                label: 'Delete',
                icon: 'pi pi-trash',
                command: () =>
                  service.delete().then(() => (showDialog = false)),
              },
            ]"
          />
          <Button
            v-if="!editOrAdd"
            label="Add"
            @click="service.add().then(() => (showDialog = false))"
          />
        </div>
      </template>
      <div class="flex flex-column gap-4">
        <InputGroup v-for="key in exposedData" :key="key.field">
          <InputGroupAddon :class="INPUT_CLASS" v-if="editItem[key.field]">
            {{ key.header }}
          </InputGroupAddon>
          <AutoComplete
            v-if="key.field.includes('tag') && service.serviceName !== 'tags'"
            multiple
            :placeholder="key.header"
            v-model="editItem[key.field]"
            class="w-full"
            filter
          />
          <Dropdown
            v-else-if="key.field === 'user'"
            :placeholder="`Select a ${key.header}`"
            v-model="editItem[key.field]"
            :options="[]"
            optionLabel="username"
            optionValue="username"
            editable
            filter
          />
          <InputNumber
            v-else-if="typeof key.type === 'number'"
            v-model="editItem[key.field]"
            :placeholder="key.header"
          />
          <Password
            v-else-if="key.field === 'password'"
            v-model="editItem[key.field]"
            :placeholder="key.header"
          />
          <Checkbox
            v-else-if="typeof key.type === 'boolean'"
            v-model="editItem[key.field]"
            binary
          />
          <InputText
            v-else
            v-model="editItem[key.field]"
            :placeholder="key.header"
          />
        </InputGroup>
      </div>
    </Dialog>
  </div>
</template>

<script setup lang="ts">
const emits = defineEmits(["deleteClick", "addClick", "rowClick"]);
const props = defineProps(["service"]);
const readonly = defineModel("readonly", { type: Boolean, default: false });

const selectedItem = ref();
const valueitems = ref();
const editItem = ref();
const expandedRows = ref();
const showDialog = ref(false);
const showMultipleDelete = ref(false);
const showFilters = ref(false);
const editOrAdd = ref(false);
const selectedColumns = ref([]);

onBeforeMount(async () => {
  await props.service?.loadItems();
  valueitems.value = JSON.parse(
    localStorage.getItem("LoadedItemsFor" + props.service.name) ?? "null"
  );

  console.log(valueitems.value);
});

const formattedServiceName = computed(
  () =>
    props.service.name?.charAt(0)?.toUpperCase() +
    props.service.name
      ?.slice(1)
      .replaceAll(/([A-Z])/g, " $1")
      .toLowerCase()
);

const exposedData = computed(() =>
  Object.keys(props.service.defaultValues)
    .filter(
      (param) =>
        !["password", "id", "executiondate"].includes(param.toLowerCase())
    )
    .map((key) => ({
      field: key,
      header:
        key.charAt(0).toUpperCase() +
        key
          .slice(1)
          .replace(/([A-Z])/g, " $1")
          .toLowerCase()
          .replace(" data", ""),
      type: typeof props.service.defaultValues[key],
    }))
);

function handleSplitButtonClick() {
  if (readonly.value) return;

  if (showMultipleDelete.value) {
    props.service.delete();
    emits("deleteClick");
    return;
  }

  editItem.value = props.service.defaultValues;
  editOrAdd.value = false;
  showDialog.value = !showDialog.value;
  emits("addClick");
}

const splitButtonModel = computed(() => [
  {
    label: showMultipleDelete.value
      ? "Cancel multiple select"
      : "Multiple select",
    icon: "pi pi-th-large",
    command: () => (showMultipleDelete.value = !showMultipleDelete.value),
  },
  {
    label: "Collapse all",
    icon: "pi pi-angle-right",
    command: () => (expandedRows.value = null),
  },
  {
    label: showMultipleDelete.value ? "Hide" : "Show filters",
    icon: showMultipleDelete.value ? "pi pi-filter" : "pi pi-filter-slash",
    command: () => (showFilters.value = !showFilters.value),
  },
]);
</script>
