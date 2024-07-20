<script setup lang="ts">
const padding = defineModel('padding', { type: Number, required: false, default: 1 })
const menuItems = defineModel('menuItems', {
  type: Array,
  required: false,
  default: () => {
    return useRouter()
      .getRoutes()
      .map((route) => ({
        key: route.path,
        label: route?.name?.split('/')[1].replaceAll('-', ' ').replaceAll('_', ' '),
        children: route.path.split('/')[2]
          ? route.path.split('/').map((elem) => {
              if (elem != '') return { key: elem, label: elem }
            })
          : undefined
      }))
  }
})
const selectedMenuItems = defineModel('selectedMenuItems', {
  type: Object,
  required: false,
  default: {}
})
const percentageOfTheScreen = defineModel('percentageOfTheScreen', {
  type: Number,
  required: false,
  default: 85
})
</script>

<template>
  <main>
    <Splitter
      v-if="useAuthStore()?.token"
      class="flex-row align-items-center overflow-hidden bg-gray-50"
    >
      <SplitterPanel
        :size="100 - percentageOfTheScreen"
        :minSize="100 - percentageOfTheScreen"
        class="shadow-1 bg-white flex overflow-auto"
        :class="`p-${padding}`"
      >
        <div class="flex-grow-1" />
        <Tree
          :value="menuItems"
          v-model:selectionKeys="selectedMenuItems"
          selectionMode="single"
          filter
          filterMode="strict"
          loadingMode="icon"
          @node-expand="console.log($event)"
          @update:selectionKeys="$router.push(Object.keys($event)[0] ?? '/')"
        />
      </SplitterPanel>
      <SplitterPanel :size="percentageOfTheScreen" :class="`p-${padding} overflow-auto flex-row`">
        <router-view v-slot="{ Component }" class="flex-grow-1 overflow-auto">
          <transition name="fade" mode="out-in">
            <component :is="Component" />
          </transition>
        </router-view>
        <div class="flex-grow-1" />
        <!-- <Button icon="pi pi-times" rounded /> -->
      </SplitterPanel>
    </Splitter>
    <div v-else class="h-full flex justify-content-around">
      <LoginComponent />
    </div>
    <main class="fixed vignette pointer-events-none z-5" />
  </main>
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
  transition: all 0.1s ease;
}

.p-treeselect-label {
  white-space: wrap !important;
  flex-wrap: wrap !important;
  display: flex !important;
  row-gap: 0.25rem !important;
}
.p-treeselect.p-treeselect-chip .p-treeselect-token {
  margin-right: 0.25rem;
}
.p-inputwrapper-filled.p-treeselect.p-treeselect-chip .p-treeselect-label {
  padding: 0.25rem;
}
</style>
