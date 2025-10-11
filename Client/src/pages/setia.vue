<template>
  <div class="flex flex flex-column h-full overflow-auto">
    <WorldMap />
    <DeshertRegion />
    <VikingsRegion />
    <PiratesRegion />
    <SumeruRegion />
  </div>
</template>

<script setup lang="ts">
defineOptions({
  name: "Setia",
  icon: "🗺️",
});
</script>
