<script setup lang="ts">
import type { TreeNode } from 'primevue/treenode'
import Toast from 'primevue/toast'
import { capitalizeWords } from '@/helpers'

const globalPadding = defineModel('globalPadding', {
  type: Number,
  required: false,
  default: 1
})
const menuItems = defineModel('menuItems', {
  type: Array as PropType<TreeNode[]>,
  required: false,
  default: () =>
    useRouter()
      .getRoutes()
      .reduce((acc, route) => {
        if (
          route.path.includes(':') ||
          route.path.includes('register') ||
          route.path.includes('login')
        )
          return acc

        const parts = route.path.split('/')
        const [_, root, ...rest] = parts

        if (!root) return acc

        let node = acc.find((item) => item.key === root) as any
        if (!node) {
          node = {
            key: root,
            label: capitalizeWords(root.replace(/[-_]/g, ' ')),
            children: []
          }
          acc.push(node)
        }

        if (rest.length > 0)
          node.children.push({
            key: parts.slice(1).join('/'),
            label: capitalizeWords(String(rest).replace(/[-_]/g, ' '))
          })
        else node.leaf = true

        return acc
      }, [] as TreeNode[])
})
const selectedMenuItems = defineModel('selectedMenuItems', {
  type: Object,
  required: false,
  default: {}
})
</script>

<template>
  <main>
    <Splitter class="flex-row align-items-center overflow-hidden bg-gray-50">
      <SplitterPanel
        :size="useAppStore().appSplitterDistribution"
        :minSize="10"
        class="shadow-1 bg-white flex overflow-auto border-round-right"
        :class="`p-${globalPadding}`"
      >
        <div class="flex-grow-1" />
        <Tree
          :value="menuItems"
          selectionMode="single"
          v-model:selectionKeys="selectedMenuItems"
          :metaKeySelection="false"
          filter
          filterMode="strict"
          loadingMode="icon"
          @nodeSelect="$router.replace($event.key)"
        />
      </SplitterPanel>
      <SplitterPanel
        :size="100 - useAppStore().appSplitterDistribution"
        :class="`p-${globalPadding} overflow-auto flex-row`"
      >
        <router-view v-slot="{ Component }" class="flex-grow-1 overflow-auto border-round">
          <transition name="fade" mode="out-in">
            <component :is="Component" />
          </transition>
        </router-view>
        <!-- <div class="flex-grow-1" /> -->
        <!-- <Button icon="pi pi-times" rounded /> -->
      </SplitterPanel>
    </Splitter>
    <!-- <ScrollBar /> -->
    <main class="fixed vignette pointer-events-none z-5" />
    <Toast />
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
.p-tree {
  padding: 0;
}
</style>
