<template>
  <div class="flex-column">
    <div id="driveBreadCrumb" class="flex-wrap gap-2 p-2 overflow-auto">
      <Button
        v-for="(folderName, index) in ['Home', ...folderLocation.split('/')]"
        :key="index"
        text
        :label="folderName"
        class="cursor-pointer p-2"
        @click="
          goToFolderLocation(
            index == 0
              ? ''
              : folderLocation.split('/').slice(0, index).join('/')
          )
        "
      />
    </div>
    <div d="driveSearchContainer" class="px-2">
      <InputText
        placeholder="Search after name, path, regestry number"
        @keyup.enter="searchFile"
      />
    </div>
    <div class="flex-wrap gap-2 w-full h-full overflow-auto p-2">
      <BaseCardComponent
        v-for="file in folderContet"
        :key="file"
        :label="file"
        :icon="getFileIcon(file)"
        class="border-round border-1 border-gray-200 custom-shadow-1 flex-grow-1"
        :class="file.includes('.') ? '' : 'bg-yellow-200'"
        style="min-width: 250px; max-height: 400px"
        aria-haspopup
        aria-controls="overlay_menu"
        @detailsClick="$refs.menuu?.toggle($event)"
        @click="
          file.includes('.')
            ? undefined
            : goToFolderLocation(`${folderLocation}/${file}`)
        "
        @click.middle="openPublicFile(`${folderLocation}/${file}`)"
        @dblclick="
          file.includes('.')
            ? openPublicFile(`${folderLocation}/${file}`)
            : undefined
        "
      >
        <div
          class="w-full h-full flex justify-content-center"
          style="line-height: 0"
        >
          <img
            v-if="getFileIcon(file) == 'pi pi-image'"
            :src="`${folderLocation}/${file}`"
            loading="lazy"
          />
          <video
            v-else-if="getFileIcon(file) == 'pi pi-video'"
            controls
            loading="lazy"
          >
            <source
              :src="`${folderLocation}/${file}`"
              type="video/mp4"
              loading="lazy"
            />
          </video>
          <iframe
            v-else-if="file.includes('.')"
            :src="`${folderLocation}/${file}`"
            class="w-full h-full"
            loading="lazy"
          />
        </div>
      </BaseCardComponent>
      <!-- <Menu ref="menuu" id="overlay_menu" popup :model="menuActions" /> -->
    </div>
  </div>
</template>

<script setup lang="ts">
definePage({
  meta: {
    title: "Drive",
    description: "",
    roles: ["user", "admin"],
  },
});

const folderLocation = defineModel("folderLocation", {
  type: String,
  default: "",
});
const folderContet = ref();
const fileInFocus = ref();

const menuActions = ref([
  {
    label: "Actions",
    items: [
      { label: "Delete", icon: "pi pi-trash" },
      {
        label: "Download",
        icon: "pi pi-download",
        command: async () =>
          await fileManager.download(fileInFocus.value).then(downloadInBrowser),
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
            menuActions.value[1].items[0].label ?? ""
          );
        },
      },
    ],
  },
]);

async function goToFolderLocation(folderPath = "") {
  folderLocation.value = folderPath.replace(/^\//, "");
  folderContet.value = await fileManager.getFolderContent(folderLocation.value);
}

onBeforeMount(goToFolderLocation);

const getFileIcon = (file: string) =>
  FILE_ICONS[file.split(".")?.pop() || "folder"] || "pi pi-folder";
const openPublicFile = async (file: string) => window.open(file, "_blank");

async function searchFile(event: KeyboardEvent) {
  const target = event.target as HTMLInputElement;
  // if (target.value)
  //   [folderLocation.value, fileInFocus.value] = await fileManager.SearchAndGetFile(target.value)
  console.log(await fileManager.SearchAndGetFile(target.value));
}
</script>

<style scoped>
.container {
  display: grid;
  grid-template-columns: repeat(
    auto-fill,
    minmax(300px, 1fr)
  ); /* Adjust item size */
}
</style>
