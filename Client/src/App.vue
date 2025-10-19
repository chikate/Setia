<template>
  <DrawingBoard
    v-if="ctrlKeyPressed"
    class="fixed w-full h-full"
    style="z-index: 99999"
  />
  <div class="p-2 align-items-center flex gap-4">
    <i class="sm:hidden flex pi pi-bars pl-3 cursor-pointer" />
    <Breadcrumb
      class="m-0 p-2 bg-transparent sm:flex hidden"
      :home="{ label: `Gov`, command: () => $router.push('/') }"
      :model="
        $route.fullPath
          .split('/')
          .map((elem) => ({
            label: capitalizeWords(elem.replaceAll('-', ' ')),
            command: () => $router.push(`/${elem}`),
          }))
          .slice(1)
      "
    />
    <div class="flex-grow-1" />
    <OverlayBadge
      value="2"
      size="small"
      class="cursor-pointer-none cursor-pointer flex align-items-center h-full"
      @click="$refs.notificationsMenu.toggle($event)"
    >
      <i class="pi pi-bell" />
    </OverlayBadge>
    <div
      class="cursor-pointer-none cursor-pointer flex align-items-center h-full"
      @click="$refs.avatarMenu.toggle($event)"
    >
      <Avatar
        class="flex align-items-center cursor-pointer"
        :image="`https://frankfurt.apollo.olxcdn.com/v1/files/qr0k1ccnla9p2-RO/image;s=1000x700`"
        shape="circle"
      />
    </div>
    <TieredMenu
      ref="avatarMenu"
      :model="[
        { label: 'Profile', icon: 'pi pi-user' },
        {
          separator: true,
        },
        {
          label: 'Online',
          icon: 'pi pi-filled-dot',
          items: [
            { label: 'Online', icon: '' },
            {
              label: 'Busy',
              icon: '',
              items: [{ label: '5 minutes', icon: '' }],
            },
            {
              label: 'Away',
              icon: '',
              items: [{ label: '5 minutes', icon: '' }],
            },
            {
              label: 'Invisible',
              icon: '',
              items: [{ label: '5 minutes', icon: '' }],
            },
          ],
        },
      ]"
      popup
    />
    <Menu
      ref="notificationsMenu"
      :model="[{ label: 'Notification1', icon: '5' }]"
      popup
    />
  </div>
  <div class="flex h-full w-full overflow-auto">
    <div
      class="sm:flex hidden h-full overflow-auto"
      style="min-width: 250px; width: 250px; max-width: 20vw"
    >
      <PanelMenu
        dragdrop
        v-model:selectionKeys="selectedKey"
        multiple
        class="w-full h-full px-2"
        :model="[
          {
            label: 'Favorites',
            icon: 'pi pi-fw pi-star',
            items: osStore().favorites,
          },
          {
            label: 'Computer',
            icon: 'pi pi-fw pi-folder',
            items: [
              {
                label: 'OS: System',
                icon: 'pi pi-fw pi-folder',
                items: [
                  {
                    label: 'vue.svg',
                    icon: 'pi pi-fw pi-file',
                  },
                ],
              },
              {
                label: 'A: Disk',
                icon: 'pi pi-fw pi-folder',
                items: [
                  {
                    label: 'HelloWorld.vue',
                    icon: 'pi pi-fw pi-file',
                  },
                ],
              },
              {
                label: 'B: USB',
                icon: 'pi pi-fw pi-folder',
                items: [
                  {
                    label: 'HelloWorld.vue',
                    icon: 'pi pi-fw pi-file',
                  },
                ],
              },
            ],
          },
          {
            label: 'Apps',
            icon: 'pi pi-fw pi-th-large',
            route: '/',
            items: installedApps?.map((app, i) => ({
              label: `${app.icon} ${app.name}`,
              route: app.__name,
            })),
          },
        ]"
      >
        <template #item="{ item }">
          <div class="flex justify-content-between">
            <router-link
              v-slot="{ href, navigate }"
              :to="item.route"
              custom
              class="no-underline text-primary"
            >
              <a
                v-ripple
                class="flex items-center cursor-pointer text-surface-700 dark:text-surface-0 gap-2 p-2"
                :href="href"
                :target="item.target"
                @click="navigate"
              >
                <span :class="item.icon" />
                {{ item.label }}
              </a>
            </router-link>
            <span
              v-if="item.items?.length"
              class="pi pi-angle-down text-primary p-2"
            />
          </div>
        </template>
      </PanelMenu>
    </div>
    <div
      id="board"
      class="h-full w-full bg-gray-800 overflow-auto relative"
      style="border-top-left-radius: 5px"
    >
      <router-view />
    </div>
  </div>
  <Toast position="top-right" :life="6000" />
</template>

<script setup lang="ts">
import OverlayBadge from "primevue/overlaybadge";

const selectedKey = ref([]);
const ctrlKeyPressed = ref(false);

window.addEventListener(
  "keydown",
  (event) => (ctrlKeyPressed.value = event.ctrlKey)
);
window.addEventListener(
  "keyup",
  (event) => (ctrlKeyPressed.value = event.ctrlKey)
);

onBeforeUnmount(() => {
  window.removeEventListener(
    "keydown",
    (event) => (ctrlKeyPressed.value = event.ctrlKey)
  );
  window.removeEventListener(
    "keyup",
    (event) => (ctrlKeyPressed.value = event.ctrlKey)
  );
});
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
