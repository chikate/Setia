<template>
  <div class="flex-column gap-2">
    <div class="border-round border-1 flex-wrap gap-2 px-2 align-items-center">
      <label class="cursor-pointer p-2" @click="goToFolderLocation()"> Home </label>
      <label
        v-for="(folderPath, index) in folderLocation.split('/')"
        v-show="folderPath"
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
        > {{ folderPath }}
      </label>
    </div>
    <InputText placeholder="Search after name, path, regestry number" @keyup.enter="searchFile" />
    <div class="flex-wrap gap-2 align-items-start">
      <BaseCardComponent
        v-for="file in folderContet"
        :key="file"
        :label="file"
        :icon="getFileIcon(file)"
        @click="fileClick(file)"
        @detailsClick="clickButtonDetails(file, $event)"
        style="width: 300px; max-height: 400px"
        class="cursor-pointer"
      >
        <Image
          preview
          width="298"
          imageClass="border-round-bottom"
          v-if="getFileIcon(file) === 'pi pi-image'"
          :src="`${folderLocation}/${file}`"
        />
      </BaseCardComponent>
    </div>
    <ContextMenu ref="menu" :model="menuActions" />
    <Menu ref="menu" popup :model="menuActions" />
  </div>
</template>

<script setup lang="ts">
import { driveStore } from '@/stores/driveStore'
import { downloadInBrowser } from '@/helpers'

const folderContet = defineModel('tierListitems', { type: Array<string>, default: [] })
const folderLocation = defineModel('folderLocation', { type: String, default: '' })
const fileInFocus = ref('Avatar.png')

const menu = ref()
const menuActions = ref([
  {
    label: 'Actions',
    items: [
      { label: 'Delete', icon: 'pi pi-trash' },
      {
        label: 'Download',
        icon: 'pi pi-download',
        command: () => driveStore().download(fileInFocus.value).then(downloadInBrowser)
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
  folderContet.value = await driveStore().getFolderContent(folderLocation.value)
}

async function clickButtonDetails(file: string, event: MouseEvent) {
  fileInFocus.value = file
  menu.value.toggle(event)
  menuActions.value[1].items[0].label = await driveStore().GetFileRegistryNumber(fileInFocus.value)
}

onBeforeMount(goToFolderLocation)

const fileClick = (file: string) =>
  file.includes('.')
    ? useRouter().push(file)
    : goToFolderLocation(`${folderLocation.value}/${file}`)

const getFileIcon = (file: string) => FILE_ICONS[file.split('.')?.pop() ?? 'folder'] || 'pi pi-file'

async function searchFile(event: KeyboardEvent) {
  const target = event.target as HTMLInputElement
  if (target.value)
    [folderLocation.value, fileInFocus.value] = await driveStore().GetFileFromRegistryNumber(
      target.value
    )
}
</script>
