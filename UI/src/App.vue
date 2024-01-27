<template>
  <Toast class="fixed mt-7" />
  <div class="flex flex-column">
    <NavBar />
    <!-- <main style="min-height: 100vh" class="overflow-auto h-full w-full flex flex-column"> -->
    <main>
      <Transition name="fade" mode="out-in">
        <div v-if="true">
          <RouterView v-if="true" class="flex flex-grow-1" />
          <PageNotFound v-else />
          <FooterBar />
        </div>
      </Transition>
    </main>
    <div class="fixed w-full h-full vignette pointer-events-none" />
  </div>
</template>

<script setup lang="ts">
import NavBar from './components/NavBar.vue'
import FooterBar from './components/FooterBar.vue'
import Toast from 'primevue/toast'
import PageNotFound from './views/PageNotFound.vue'
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
