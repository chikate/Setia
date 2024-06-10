<template>
  <NavBar />
  <main>
    <router-view v-slot="{ Component }">
      <transition name="fade" mode="out-in">
        <component :is="Component" />
      </transition>
    </router-view>
  </main>
  <FooterBar />
  <main class="fixed vignette pointer-events-none" />
  <Toast position="bottom-right" group="main" />
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
  transition: all 0.1s ease;
}
</style>
