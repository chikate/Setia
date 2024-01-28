<template>
  <Toast class="fixed mt-7" />
  <div class="flex flex-column">
    <NavBar />
    <main>
      <Transition name="fade" mode="out-in">
        <main v-if="true">
          <RouterView v-if="true" />
          <PageNotFound v-else />
          <FooterBar />
        </main>
      </Transition>
    </main>
    <main class="fixed vignette pointer-events-none" />
  </div>
</template>

<script setup lang="ts">
import NavBar from './components/NavBar.vue'
import FooterBar from './components/FooterBar.vue'
import PageNotFound from './views/PageNotFound.vue'
import Toast from 'primevue/toast'
</script>

<style>
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
