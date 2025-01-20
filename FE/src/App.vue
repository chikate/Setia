<script setup lang="ts">
import type { TreeNode } from 'primevue/treenode'
import Toast from 'primevue/toast'

const sidebarHover = ref(false)

const menuItems = ref(
  useRouter()
    .getRoutes()
    .reduce((acc, route) => {
      if ([':', 'register', 'login'].some((path) => route.path.includes(path))) return acc
      const [_, root, ...children] = route.path.split('/')

      if (!(root || _)) return acc

      const node = acc.find((item) => item.key === root) ?? {
        key: root,
        icon: MENU_ICONS[root?.toLowerCase() ?? 'default'] ?? 'pi pi-circle',
        label: capitalizeWords(root.replace(/[-_]/g, ' ')),
        children: []
      }

      if (!acc.includes(node)) acc.push(node)

      if (node.label == 'Drive') node.children = [{} as TreeNode]

      children.length
        ? node.children?.push({
            key: route.path.slice(1),
            label: capitalizeWords(children.join(' '))
          })
        : (node.leaf = true)
      return acc
    }, [] as TreeNode[])
)

const onNodeClick = async (event: any) => (window.location.href = event)

const onNodeExpand = async (event: any) =>
  event.key.startsWith('drive')
    ? (event.children = await fileContentToTreeNode(event.leaf ? '' : event.key, 2))
    : undefined

const fileContentToTreeNode = async (key: string, level: number): Promise<TreeNode[]> =>
  Promise.all(
    (await fileManager.getFolderContent(key)).map(async (elem: string) => ({
      key: `${key}${key ? '/' : ''}${elem}`,
      label: elem,
      children:
        !elem.includes('.') && level > 1
          ? await fileContentToTreeNode(`${key}/${elem}`, level - 1)
          : undefined,
      icon: FILE_ICONS[elem.split('.').pop() ?? 'folder']
    }))
  )
</script>

<template>
  <div
    id="sidebar"
    class="fixed left-0 flex-column h-screen bg-gray-200 z-5 overflow-auto custom-shadow-1"
    :class="{ 'border-round-right': sidebarHover }"
    @mouseenter="sidebarHover = true"
    @mouseleave="sidebarHover = false"
  >
    <div class="flex-column h-full py-4 w-full">
      <div
        v-for="item in menuItems"
        :key="item.key"
        class="flex-column cursor-pointer"
        style="white-space: nowrap"
      >
        <div class="flex-row py-2 hover:bg-gray-300">
          <div class="flex-row align-items-center w-full" @click="onNodeClick(item.key)">
            <i class="text-xl text-center w64px" :class="item.icon" />
            <label class="cursor-pointer w-full" v-show="sidebarHover">
              {{ item.label }}
            </label>
          </div>
          <i
            @click="onNodeExpand(item)"
            class="pi pi-angle-down text-gray-600 text-center w64px"
            v-show="sidebarHover && (item.children?.length ?? 0 > 0)"
          />
        </div>
      </div>
    </div>
    <div v-if="authService.user" class="flex-row w-full align-items-center">
      <Avatar
        class="p-2 w64px"
        image="data:image/jpeg;base64,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"
      />
      <label class="w-full">{{ authService.user.name }}</label>
      <i class="pi pi-ellipsis-v text-center w64px" v-show="sidebarHover" />
    </div>
    <div v-else class="w-full p-2">
      <Button icon="pi pi-user" @click="onNodeClick('/')" />
    </div>
  </div>
  <router-view v-slot="{ Component }" class="border-round" style="margin-left: 64px">
    <transition name="fade" mode="out-in">
      <component :is="Component" />
    </transition>
  </router-view>
  <!-- <main class="fixed vignette pointer-events-none z-5" /> -->
  <Toast />
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
  transition: opacity 0.1s ease;
}

.p-treeselect-label,
.p-treeselect.p-treeselect-chip .p-treeselect-token {
  white-space: wrap !important;
  display: flex !important;
  flex-wrap: wrap !important;
  row-gap: 0.25rem !important;
  margin-right: 0.25rem;
}
.p-inputwrapper-filled.p-treeselect.p-treeselect-chip .p-treeselect-label {
  padding: 0.25rem;
}
.p-tree {
  padding: 0;
}

.w64px {
  width: 64px;
  min-width: 64px;
  max-width: 64px;
}

#sidebar {
  min-width: 64px;
  width: 64px;
  transition: all 0.1s ease;
}

#sidebar:hover {
  min-width: 300px;
  width: 300px;
}
</style>
