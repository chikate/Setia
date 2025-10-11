<template>
  <!-- <div class="p-2 align-items-center flex gap-4">
    <i class="sm:hidden flex pi pi-bars pl-3 cursor-pointer" />
    <Breadcrumb
      class="m-0 p-2 bg-transparent sm:flex hidden"
      :home="{ label: `Gov`, command: () => $router.push('/') }"
      :model="
        $route.fullPath
          .split('/')
          .map((elem) => ({
            label: capitalizeString(elem),
            command: () => $router.push(`/${elem}`),
          }))
          .slice(1)
      "
    />

    <div class="flex-grow-1" />

    <Avatar
      class="shadow-1"
      :image="`https://www.youtube.com/embed/zrFWHAyI2W0`"
      shape="circle"
    />
  </div>
  <div class="flex h-full w-full overflow-auto">
    <div
      class="sm:flex hidden h-full overflow-auto"
      style="min-width: 250px; width: 250px; max-width: 20vw"
    >
      <Tree
        dragdrop
        v-model:selectionKeys="selectedKey"
        class="p-0 bg-transparent w-full h-full"
        :value="[
          {
            key: '0',
            label: 'Favorites',
            data: '.github folder',
            icon: 'pi pi-fw pi-star',
            children: [
              {
                key: '0-0',
                label: 'workflows',
                data: 'workflows folder',
                icon: 'pi pi-fw pi-folder',
              },
            ],
          },
          {
            key: '1',
            label: 'Computer',
            data: 'src folder',
            icon: 'pi pi-fw pi-folder',
            children: [
              {
                key: '1-0',
                label: 'OS: System',
                data: 'assets folder',
                icon: 'pi pi-fw pi-folder',
                children: [
                  {
                    key: '1-0-0',
                    label: 'vue.svg',
                    data: 'vue.svg file',
                    icon: 'pi pi-fw pi-file',
                  },
                ],
              },
              {
                key: '1-1',
                label: 'A: Disk',
                data: 'components folder',
                icon: 'pi pi-fw pi-folder',
                children: [
                  {
                    key: '1-1-0',
                    label: 'HelloWorld.vue',
                    data: 'HelloWorld.vue file',
                    icon: 'pi pi-fw pi-file',
                  },
                ],
              },
              {
                key: '1-2',
                label: 'B: USB',
                data: 'components folder',
                icon: 'pi pi-fw pi-folder',
                children: [
                  {
                    key: '1-2-0',
                    label: 'HelloWorld.vue',
                    data: 'HelloWorld.vue file',
                    icon: 'pi pi-fw pi-file',
                  },
                ],
              },
            ],
          },
          {
            key: '2',
            label: 'Apps',
            icon: 'pi pi-fw pi-th-large',
            children: installedApps.map((app, i) => ({
              key: `2-${i}`,
              label: `${app.icon} ${app.name}`,
            })),
          },
        ]"
      />
    </div>
    <div
      class="h-full w-full bg-gray-800 overflow-auto"
      style="border-top-left-radius: 5px"
    > -->
  <router-view />
  <!-- </div>
  </div>
  <Toast position="top-right" :life="6000" /> -->
</template>

<script setup lang="ts">
const selectedKey = ref([]);
// const showSidebar = ref(false);
// const menuItems = ref(
//   useRouter()
//     .getRoutes() // Get all routes from the Vue Router
//     .reduce((acc, route) => {
//       // Reduce the routes into a hierarchical structure
//       // Skip routes containing dynamic paths or specific keywords
//       if ([":", "register", "login"].some((path) => route.path.includes(path)))
//         return acc;

//       // Split the route path into parts, separating by "/"
//       const [_, root, ...children] = route.path.split("/");

//       // If there's no root or the route path is invalid, skip this route
//       if (!(root || _)) return acc;

//       // Try to find an existing node with the same root key, or create a new node
//       const node = acc.find((item) => item.key == root) ?? {
//         key: root, // Unique key for this menu item
//         icon: MENU_ICONS[root?.toLowerCase() ?? "default"] ?? "pi pi-circle", // Icon for the root node (fallback to default)
//         label: capitalizeWords(route.meta?.title ?? root.replace(/[-_]/g, " ")), // Display label (capitalize and format the root if title does not exist)
//         children: [], // Initialize children as an empty array
//       };

//       // If the node is newly created, add it to the accumulator
//       if (!acc.includes(node)) acc.push(node);

//       // Special case for the "Drive" menu - initialize children to an empty TreeNode
//       if (node.label == "Drive") node.children = [{} as TreeNode];

//       // If there are child routes, add them as children to this node
//       children.length
//         ? node.children?.push({
//             key: route.path.slice(1), // Use the full path (excluding leading "/") as the key
//             label: capitalizeWords(children.join(" ")), // Generate a label for the child route
//           })
//         : (node.leaf = true); // Mark the node as a leaf if there are no children

//       return acc; // Return the updated accumulator
//     }, [] as TreeNode[]) // Initialize the accumulator as an empty array of TreeNode
// );

// const onNodeClick = async (event: any) => (window.location.href = event);

// const onNodeExpand = async (event: any) =>
//   event.key.startsWith("drive")
//     ? (event.children = await fileContentToTreeNode(
//         event.leaf ? "" : event.key,
//         2
//       ))
//     : undefined;

// const fileContentToTreeNode = async (
//   key: string,
//   level: number
// ): Promise<TreeNode[]> =>
//   Promise.all(
//     (await fileManager.getFolderContent(key)).map(async (elem: string) => ({
//       key: `${key}${key ? "/" : ""}${elem}`,
//       label: elem,
//       children:
//         !elem.includes(".") && level > 1
//           ? await fileContentToTreeNode(`${key}/${elem}`, level - 1)
//           : undefined,
//       icon: FILE_ICONS[elem.split(".").pop() ?? "folder"],
//     }))
//   );
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

#wallpaper {
  background-size: cover;
  background-position: center;
}

/* body {
  background-image: url("https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/8c9c3684-1124-4459-a82b-f1c63f6abf07/dgo05a4-ea8c4633-cf50-40ce-aec6-1f0f8b93e8bc.gif?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOjdlMGQxODg5ODIyNjQzNzNhNWYwZDQxNWVhMGQyNmUwIiwiaXNzIjoidXJuOmFwcDo3ZTBkMTg4OTgyMjY0MzczYTVmMGQ0MTVlYTBkMjZlMCIsIm9iaiI6W1t7InBhdGgiOiJcL2ZcLzhjOWMzNjg0LTExMjQtNDQ1OS1hODJiLWYxYzYzZjZhYmYwN1wvZGdvMDVhNC1lYThjNDYzMy1jZjUwLTQwY2UtYWVjNi0xZjBmOGI5M2U4YmMuZ2lmIn1dXSwiYXVkIjpbInVybjpzZXJ2aWNlOmZpbGUuZG93bmxvYWQiXX0.lOBaKSdXWWw6ft43cuUQLPLTBcupAvdNbqOWmnHq40c");
  background-position: center;
  background-size: cover;
  background-color: white;
} */
</style>
