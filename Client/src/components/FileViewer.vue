<template>
  <div class="w-full h-full flex justify-content-center" style="line-height: 0">
    <img v-if="getFileIcon(file) == 'pi pi-image'" :src loading="lazy" />
    <video
      v-else-if="getFileIcon(file) == 'pi pi-video'"
      controls
      loading="lazy"
    >
      <source :src type="video/mp4" loading="lazy" />
    </video>
    <iframe
      v-else-if="file.includes('.')"
      :src
      class="w-full h-full"
      loading="lazy"
    />
  </div>
</template>

<script lang="ts" setup>
const src = defineModel("src", { type: String, required: true });
const file = computed(() => src.value.split("/").pop());
const getFileIcon = (file: string) =>
  FILE_ICONS[file.split(".")?.pop() || "folder"] || "pi pi-folder";
</script>
