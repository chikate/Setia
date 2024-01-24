<template>
  <Toast class="fixed mt-7" />
  <NavBar />
  <main style="min-height: 100vh" class="overflow-auto h-full w-full flex flex-column">
    <Transition name="fade" mode="out-in">
      <RouterView class="flex flex-grow-1" />
    </Transition>
    <FooterBar />
  </main>
  <div class="fixed w-full h-full vignette pointer-events-none" />
</template>

<script setup lang="ts">
import NavBar from './components/NavBar.vue'
import FooterBar from './components/FooterBar.vue'
import Toast from 'primevue/toast'
</script>

<style scoped>
.fade-leave-to,
.fade-enter-from {
  opacity: 0;
}
.fade-leave-from,
.fade-enter-to {
  opacity: 1;
}
.fade-enter-active,
.fade-leave-active {
  transition: all 0.2s ease;
}
</style>
