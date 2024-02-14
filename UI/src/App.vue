<template>
  <NavBar />
  <main>
    <router-view v-slot="{ Component }">
      <transition name="fade" mode="out-in">
        <component :is="Component" />
      </transition>
    </router-view>
    <PageNotFound v-if="false" />
  </main>
  <FooterBar />
  <main class="fixed vignette pointer-events-none" />
</template>

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
