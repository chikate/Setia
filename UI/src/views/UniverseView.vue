<template>
  <main>
    <div class="flex flex-column">
      <WorldMap />
      <DeshertRegion />
      <VikingsRegion />
      <PiratesRegion />
      <SumeruRegion />
    </div>
  </main>
</template>

<script setup lang="ts">
import DeshertRegion from '@/components/regions/DeshertRegion.vue'
import VikingsRegion from '@/components/regions/VikingsRegion.vue'
import PiratesRegion from '@/components/regions/PiratesRegion.vue'
import SumeruRegion from '@/components/regions/SumeruRegion.vue'
import WorldMap from '@/components/regions/WorldMap.vue'
</script>
