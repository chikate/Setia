<template>
  <div class="vignette flex justify-content-center align-items-center">
    <BookingComp />
  </div>
</template>

<script setup lang="ts">
defineOptions({
  name: "Nucet",
  icon: "🛖",
});

onBeforeMount(init);
async function init() {}
</script>
