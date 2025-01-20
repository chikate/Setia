<template>
  <div class="flex-column gap-2">
    <div class="border-round border-1 flex-wrap gap-2 px-2 align-items-center">
      <label class="cursor-pointer p-2" @click="goToFolderLocation()"> Home </label>
      <label
        v-for="(folderName, index) in folderLocation.split('/')"
        v-show="folderName"
        :key="index"
        class="cursor-pointer p-2"
        @click="
          goToFolderLocation(
            folderLocation
              .split('/')
              .slice(0, index + 1)
              .join('/')
          )
        "
      >
        > {{ folderName }}
      </label>
    </div>
    <InputText placeholder="Search after name, path, regestry number" @keyup.enter="searchFile" />
    <div class="container gap-2 w-full align-items-start">
      <BaseCardComponent
        v-for="file in folderContet"
        :key="file"
        :label="file"
        :icon="getFileIcon(file)"
        @click="file.includes('.') ? undefined : goToFolderLocation(`${folderLocation}/${file}`)"
        @detailsClick="clickButtonDetails(file, $event)"
        @click.middle="openPublicFile(`${folderLocation}/${file}`)"
        @dblclick="file.includes('.') ? openPublicFile(`${folderLocation}/${file}`) : undefined"
        style="max-height: 400px"
      >
        <div v-if="getFileIcon(file) == 'pi pi-image'" class="overflow-hidden align-self-center">
          <img
            :src="`${folderLocation}/${file}`"
            class="border-round-bottom"
            style="width: 100%; object-fit: cover"
          />
        </div>
        <video v-else-if="getFileIcon(file) == 'pi pi-video'" controls class="border-round-bottom">
          <source :src="`${folderLocation}/${file}`" type="video/mp4" />
        </video>
        <iframe
          v-else-if="file.includes('.')"
          :src="`${folderLocation}/${file}`"
          class="border-round-bottom"
          loading="lazy"
        />
      </BaseCardComponent>
      <Menu ref="menu" popup :model="menuActions" />
    </div>
    <!-- <ContextMenu ref="menu" :model="menuActions" /> -->
  </div>
</template>

<script setup lang="ts">
const folderLocation = defineModel('folderLocation', { type: String, default: '' })
const folderContet = ref()
const fileInFocus = ref()

const menu = ref()
const menuActions = ref([
  {
    label: 'Actions',
    items: [
      { label: 'Delete', icon: 'pi pi-trash' },
      {
        label: 'Download',
        icon: 'pi pi-download',
        command: () => fileManager.download(fileInFocus.value).then(downloadInBrowser)
      },
      { label: 'Share', icon: 'pi pi-link' }
    ]
  },
  {
    label: 'Details',
    items: [
      {
        label: '',
        icon: 'pi pi-hashtag',
        command: () => {
          navigator.clipboard.writeText(menuActions.value[1].items[0].label ?? '')
        }
      }
    ]
  }
])

async function goToFolderLocation(folderPath = '') {
  folderLocation.value = folderPath.replace(/^\//, '')
  folderContet.value = await fileManager.getFolderContent(folderLocation.value)
}

async function clickButtonDetails(file: string, event: MouseEvent) {
  fileInFocus.value = file
  menu.value.toggle(event)
  menuActions.value[1].items[0].label = await fileManager.GetFileRegistryNumber(fileInFocus.value)
}

onBeforeMount(goToFolderLocation)

const getFileIcon = (file: string) => FILE_ICONS[file.split('.')?.pop() ?? 'folder'] ?? 'pi pi-file'

async function openPublicFile(file: string) {
  window.open(file, '_blank')
}

async function searchFile(event: KeyboardEvent) {
  const target = event.target as HTMLInputElement
  if (target.value)
    [folderLocation.value, fileInFocus.value] = await fileManager.GetFileFromRegistryNumber(
      target.value
    )
}
</script>

<style scoped>
.container {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(300px, 1fr)); /* Adjust item size */
}
.container > * {
  line-height: 0 !important;
}
</style>
