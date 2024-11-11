<script setup lang="ts">
import type { TreeNode } from 'primevue/treenode'

const route = useRoute()
const menuItems = ref(
  useRouter()
    .getRoutes()
    .reduce((acc, route) => {
      if ([':', 'register', 'login'].some((path) => route.path.includes(path))) return acc

      const [_, root, ...children] = route.path.split('/')

      if (!root) return acc

      const node = acc.find((item) => item.key === root) ?? {
        key: root,
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
const selectedMenuItems = computed(() =>
  menuItems.value.find((elem: TreeNode) =>
    elem.label?.includes(capitalizeWords(String(route.name).replace(/[-_]/g, ' ')))
  )
)

const onNodeSelect = async (event: any) => (window.location = event.key)

const onNodeExpand = async (event: any) =>
  event.key.startsWith('drive')
    ? (event.children = await fileContentToTreeNode(event.leaf ? '' : event.key, 2))
    : undefined

const fileContentToTreeNode = async (key: string, level: number): Promise<TreeNode[]> =>
  Promise.all(
    (await fileManager.getFolderContent(key)).map(async (elem: string) => ({
      key: `${key}${key != '' ? '/' : ''}${elem}`,
      label: elem,
      children:
        !elem.includes('.') && level > 1
          ? await fileContentToTreeNode(`${key}/${elem}`, level - 1)
          : undefined,
      icon: FILE_ICONS[elem.split('.')[1] ?? 'folder']
    }))
  )
</script>

<template>
  <div class="flex-row gap-2 p-2 h-full overflow-hidden">
    <Tree
      :value="menuItems"
      class="fixed"
      style="min-width: 300px; max-width: 300px; width: 300px"
      selectionMode="single"
      :selectionKeys="selectedMenuItems"
      filter
      filterMode="strict"
      loadingMode="icon"
      @nodeSelect="onNodeSelect"
      @nodeExpand="onNodeExpand"
    />
    <div style="min-width: 300px; max-width: 300px; width: 300px" />
    <router-view v-slot="{ Component }" class="w-full h-full border-round overflow-auto">
      <transition name="fade" mode="out-in">
        <component :is="Component" />
      </transition>
    </router-view>
  </div>
  <main class="fixed vignette pointer-events-none z-5" />
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
</style>
