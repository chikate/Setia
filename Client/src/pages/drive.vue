<template>
  <div class="flex flex-column h-full overflow-auto gap-2">
    <SelectButton v-model="viewStyle" :options="['table']" />

    <Breadcrumb :home="{ icon: 'pi pi-folder' }" :model="pathParts" />

    <InputText placeholder="Search..." @keyup.enter="search" />

    <DataTable
      v-if="viewStyle == 'table'"
      :value="files"
      selection-mode="multiple"
      class="overflow-auto"
      striped-rows
    >
      <Column header="#" class="text-center">
        <template #body="{ index }">
          {{ index + 1 }}
        </template>
      </Column>

      <Column>
        <template #body="{ data: file }">
          <FileViewer :src="`${folder}/${file}`" />
        </template>
      </Column>

      <Column header="Name" class="">
        <template #body="{ data }">
          {{ data }}
        </template>
      </Column>

      <Column class="text-right">
        <template #body>
          <i class="pi pi-ellipsis-v" />
        </template>
      </Column>
    </DataTable>

    <BaseCardComponent
      v-else
      v-for="file in files"
      :key="file"
      :label="file"
      :icon="getIcon(file)"
      class="border-round border-1 border-gray-200 custom-shadow-1 flex-grow-1"
      :class="{ 'bg-yellow-200': isFolder(file) }"
      @click="file.includes('.') ? undefined : go(`${folder}/${file}`)"
      @detailsClick="($refs.menuu as any)?.toggle($event)"
      @click.middle="openPublicFile(`${folder}/${file}`)"
      @dblclick="
        file.includes('.') ? openPublicFile(`${folder}/${file}`) : undefined
      "
    >
      <FileViewer :src="`${folder}/${file}`" />
    </BaseCardComponent>

    <Menu ref="menuu" popup :model="menuActions" />
  </div>
</template>

<script setup lang="ts">
import { DriveApi } from "@/composables";
import type { MenuItem } from "primevue/menuitem";

defineOptions({
  name: "Drive",
  icon: "📁",
  role: ["Admin"],
});

const folder = defineModel("folder", { type: String });
const files = ref([]);
const fileInFocus = ref();

const viewStyle = ref<"table">();

const menuActions = ref<MenuItem[]>([
  {
    label: "Actions",
    items: [
      { label: "Delete", icon: "pi pi-trash" },
      {
        label: "Download",
        icon: "pi pi-download",
        command: async () =>
          await new DriveApi()
            .apiDriveDownloadGet(fileInFocus.value)
            .then(downloadInBrowser),
      },
      { label: "Share", icon: "pi pi-link" },
    ],
  },
  {
    label: "Details",
    items: [
      {
        label: "",
        icon: "pi pi-hashtag",
        command: () => {
          navigator.clipboard.writeText(
            'menuActions.value[1].items[0].label ?? ""'
          );
        },
      },
    ],
  },
]);

const pathParts = computed(() =>
  folder.value.split("/").map((label, i, arr) => ({
    label,
    to: arr.slice(0, i + 1).join("/"),
  }))
);

const isFolder = (f: string) => !f.includes(".");

onBeforeMount(go);
async function go(folderPath: string = "") {
  files.value = await new DriveApi().apiDriveGetFolderContentGet({
    filePath: (folder.value = folderPath.replace(/^\//, "")),
  });
}

const getIcon = (file: string) =>
  FILE_ICONS[file.split(".")?.pop() || "folder"] || "pi pi-folder";

const openPublicFile = async (file: string) => window.open(file, "_blank");

async function search(event: KeyboardEvent) {
  [folder.value, fileInFocus.value] = await new DriveApi().apiDriveSearchGet({
    input: (event.target as HTMLInputElement).value,
  });
}
</script>
