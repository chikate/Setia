<template>
  <div class="flex-column justify-content-center gap-8 align-items-center">
    asd

    <Button
      v-for="file in folderContet"
      :key="file"
      :label="file"
      icon="pi pi-file"
      @click="driveStore().download(file).then(download)"
    />
  </div>
</template>

<script setup lang="ts">
import { download } from '@/helpers'

const folderContet = defineModel('tierListitems', {
  type: Array<string>,
  required: false,
  default: []
})

onBeforeMount(init)
async function init() {
  folderContet.value = await driveStore().getFolderContent()
}
</script>
