<template>
  <!-- class="fixed mt-7" -->
  <Toast position="bottom-left" group="bl" />
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
