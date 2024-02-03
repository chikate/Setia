<template>
  <!-- class="fixed mt-7" -->
  <Toast position="bottom-left" group="bl" />
  <NavBar />
  <router-view v-slot="{ Component }">
    <transition name="fade" mode="out-in">
      <component :is="Component" />
    </transition>
  </router-view>
  <PageNotFound v-if="false" />
  <FooterBar />
  <main class="fixed vignette pointer-events-none" />
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
