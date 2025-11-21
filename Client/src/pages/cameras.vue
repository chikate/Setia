<template>
  <div class="flex flex-column align-items-center w-full">
    <FileViewer src="https://youtu.be/lLJayGmjIDs" />
  </div>
</template>

<script setup lang="ts">
import FileViewer from '@/components/FileViewer.vue';

defineOptions({ name: "Cameras", icon: "🎦", role: ["Admin"] });
</script>
