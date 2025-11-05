<template>
  <div
    v-if="isAuthenticated()"
    id="layout"
    class="flex flex-column h-full w-full"
  >
    <div
      id="navbar"
      v-if="isAuthenticated() && isAdmin()"
      class="flex gap-5 align-items-center p-2"
    >
      <i class="sm:hidden flex pi pi-bars pl-2 cursor-pointer" />
      <Breadcrumb
        class="m-0 p-0 bg-transparent sm:flex hidden"
        :home="{ label: `Gov`, command: () => $router.push('/') }"
        :model="
          $route.fullPath
            .split('/')
            .map((elem) => ({
              label: capitalizeWords(elem.replaceAll('-', ' ')),
              command: () => $router.push(`/${elem}`),
            }))
            .slice(1)
            .filter((t) => t.label != '')
        "
      />
      <div class="flex-grow-1" />
      <Button
        icon="pi pi-search"
        label="Search"
        size="small"
        outlined
        class="border-1 custom-shadow-1"
        @click="search.visible = true"
      />
      <OverlayBadge
        :value="notifications.length"
        size="small"
        class="cursor-pointer-none cursor-pointer flex align-items-center"
        @click="($refs.notificationsMenu as any).toggle($event)"
      >
        <i class="pi pi-bell custom-shadow-1" />
      </OverlayBadge>
      <div
        class="cursor-pointer-none cursor-pointer flex align-items-center relative"
        @click="($refs.avatarMenu as any).toggle($event)"
      >
        <Avatar
          class="flex align-items-center cursor-pointer custom-shadow-1"
          :image="`https://frankfurt.apollo.olxcdn.com/v1/files/qr0k1ccnla9p2-RO/image;s=1000x700`"
          shape="circle"
        />
        <div
          class="bg-green-500 border-1 p-1 border-green-600 bottom-0 right-0 border-round absolute"
        />
      </div>
      <TieredMenu
        ref="avatarMenu"
        :model="[
          {
            label: 'Profile',
            icon: 'pi pi-user',
            command: () => $router.push('/@/dragos'),
          },
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
          {
            label: 'Log Out',
            icon: 'pi pi-quit',
            command: () => {
              clearCookies();
              $router.go(0);
            },
          },
        ]"
        popup
      />
      <Menu ref="notificationsMenu" :model="notifications" popup />
    </div>
    <div
      class="flex flex-row gap-2 align-items-start overflow-hidden w-full h-full justify-content-center"
    >
      <PanelMenu
        v-if="isAuthenticated() && isAdmin()"
        class="sm:flex hidden h-full overflow-auto"
        dragdrop
        v-model:selectionKeys="selectedKey"
        multiple
        v-model:expandedKeys="expandedKeys"
        :model="items"
        style="min-width: 250px; width: 250px"
      >
        <template #item="{ item }">
          <div class="flex justify-content-between align-items-center">
            <RouterLink
              v-slot="{ href, navigate }"
              :to="item.route ?? ''"
              custom
              class="no-underline text-primary"
            >
              <a
                class="flex gap-2 align-items-center cursor-pointer text-surface-700 dark:text-surface-0 p-1"
                :href="href"
                :target="item.target ?? ''"
                @click="navigate"
              >
                <span :class="item.icon" />
                {{ item.label }}
              </a>
            </RouterLink>
            <span
              v-if="item.items?.length"
              class="pi pi-angle-down text-primary p-1"
            />
          </div>
        </template>
      </PanelMenu>
      <router-view />
    </div>
    <Dialog
      modal
      v-model:visible="search.visible"
      class="w-3 p-0"
      dismissableMask
      :showHeader="false"
      contentClass="p-2 bg-transparent"
    >
      <InputText
        autofocus
        v-model="search.query"
        placeholder="Search apps..."
        class="p-2"
        @keypress.enter="
          // launch(filtered[0]);
          search.visible = false
        "
      />
      Search result
      <div class="flex flex-wrap p-2 pt-0">
        <div
          v-for="app in filtered"
          :key="app.name"
          class="app-item flex-grow-1 cursor-pointer bg-gray-700 p-1 border-round"
          @click="
            // launch(app);
            search.visible = false
          "
        >
          {{ app.icon }} {{ app.name }}
        </div>
      </div>
      Favorites
      <div class="flex flex-wrap p-2 pt-0">
        <div
          v-for="app in osStore().favorites"
          :key="app.name"
          class="app-item flex-grow-1 cursor-pointer bg-gray-700 p-1 border-round"
          @click="
            // launch(app);
            search.visible = false
          "
        >
          {{ app.label }}
        </div>
      </div>
    </Dialog>
  </div>

  <DrawingBoard
    v-if="ctrlKeyPressed"
    class="fixed w-screen h-screen"
    style="z-index: 99999"
  />

  <AuthLogin v-if="!isAuthenticated()" />

  <ContextMenu ref="menu" :model="defaultMenuItems" />

  <Toast />
</template>

<script setup lang="ts">
import OverlayBadge from "primevue/overlaybadge";
import Toast from "primevue/toast";

const notifications = ref([{ label: "Notification1", icon: "5" }]);

const menu = ref();
const search = ref({ visible: false, query: "" });
const filtered = computed(() =>
  installedApps?.filter((app) =>
    app.name.toLowerCase().includes(search.value.query.toLowerCase())
  )
);
const defaultMenuItems = [
  { label: "🔍 Search", command: () => (search.value.visible = true) },
  { label: "🔄 Refresh", command: () => location.reload() },
];

const selectedKey = ref([]);
const items = ref<any>([
  {
    key: 0,
    label: "Favorites",
    icon: "pi pi-fw pi-star",
    items: osStore().favorites,
  },
  {
    key: 1,
    label: "Computer",
    icon: "pi pi-fw pi-folder",
    items: [
      {
        label: "OS: System",
        icon: "pi pi-fw pi-folder",
        items: [
          {
            label: "vue.svg",
            icon: "pi pi-fw pi-file",
          },
        ],
      },
      {
        label: "A: Disk",
        icon: "pi pi-fw pi-folder",
        items: [
          {
            label: "HelloWorld.vue",
            icon: "pi pi-fw pi-file",
          },
        ],
      },
      {
        label: "B: USB",
        icon: "pi pi-fw pi-folder",
        items: [
          {
            label: "HelloWorld.vue",
            icon: "pi pi-fw pi-file",
          },
        ],
      },
    ],
  },
  {
    key: 2,
    label: "Apps",
    icon: "pi pi-fw pi-th-large",
    route: "/",
    items: installedApps?.map((app, i) => ({
      label: `${app.icon} ${app.name}`,
      route: app.__name,
    })),
  },
]);
const ctrlKeyPressed = ref(false);

const expandedKeys = ref([]);

const expandNode = (node) => (expandedKeys.value[node.key] = true);

expandNode(items.value[0]);
expandNode(items.value[2]);

window.addEventListener(
  "keydown",
  (event) => (ctrlKeyPressed.value = event.ctrlKey)
);
window.addEventListener(
  "keyup",
  (event) => (ctrlKeyPressed.value = event.ctrlKey)
);

// window.addEventListener(
//   "keydown",
//   (event) => event.key.toLowerCase() == "s" && (searchDialog.value = true)
// );

window.addEventListener("contextmenu", (event) => {
  event.preventDefault();
  menu.value.show(event);
});

// SignalR
signalRConnection.on("ReceiveNotification", setNotification);
function setNotification(data) {
  notifications.value = data;
}

// document.getElementById("sendBtn").addEventListener("click", async () => {
//   await connection.invoke("SendNotification", "Hello from client!");
// });
</script>
