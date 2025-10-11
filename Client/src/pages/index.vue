<template>
  <div id="desktop" @contextmenu.prevent="openDesktopMenu">
    <div
      v-for="app in installedApps"
      :key="app.name"
      :class="selectedApps.has(app.name) ? 'bg-blue-500' : 'hover:bg-blue-200'"
      class="desktop-item"
      @click="toggleSelectApp($event, app.name)"
      @contextmenu.prevent="openAppCtx($event, app)"
      @dblclick="openWindow(app)"
      draggable
    >
      <label class="text-6xl emoji-outline">{{ app.icon ?? "🌠" }}</label>
      {{ app.name }}
    </div>
  </div>

  <div
    v-for="window in windows"
    :id="`window-${window.id}`"
    :key="window.id"
    class="window backdrop-blur custom-shadow-1"
    :class="[
      activeWinId == window.id ? 'border-blue-500' : 'border-gray-50',
      window.state == 'pinned' && 'pinned',
    ]"
    style="background-color: rgba(233, 233, 233, 0.99)"
    @mousedown="focusWindow(window.id)"
  >
    <div
      class="flex flex-row justify-content-between align-items-center bg-gray-500 w-full"
      @mousedown.stop="startDrag($event, window)"
    >
      <div class="flex flex-row align-items-center">
        <div class="p-2">
          {{ window.app.icon }}
        </div>
        <Breadcrumb
          class="m-0 p-2 bg-transparent"
          :home="{ label: window.app.name }"
        />
        <div
          s
          class="hover:text-blue-500 cursor-pointer h-full p-2"
          @click="setSize(window.id)"
        >
          ▣
        </div>
        <div
          style="width: 2rem"
          class="hover:text-blue-500 cursor-pointer h-full p-2"
          @click="togglePin(window.id)"
        >
          {{ window.state == "pinned" ? "📍" : "📌" }}
        </div>
        <div class="tabs flex flex-row">
          <div
            v-for="(file, index) in tabs"
            :key="file"
            @click="activeTabIndex = index"
            :class="{ 'bg-blue-200': activeTabIndex == index }"
            class="flex flex-row align-items-center"
          >
            <i class="pi pi-file text-center" style="min-width: 32px" />
            <label>
              {{ file }}
            </label>
            <Button text icon="pi pi-times" @click="tabs.splice(index, 1)" />
          </div>
        </div>
        <!-- <div
            style="width: 2rem"
            class="flex flex-column  justify-content-center hover:text-blue-500 cursor-pointer"
            @click="minimize(w.id)"
          >
            _
          </div> -->
      </div>
      <div
        style="width: 2rem"
        class="hover:bg-red-500 cursor-pointer h-full p-2"
        @click="closeWindow(window.id)"
      >
        ✕
      </div>
    </div>
    <component :is="window.app" class="h-full" />
    <div
      class="resizer"
      @mousedown.stop.prevent="startResize($event, window)"
    />
  </div>

  <div id="tray" class="flex flex-row-reverse p-2">
    <div
      v-for="appWindow in windows"
      :id="`tray-${appWindow.id}`"
      :key="appWindow.id"
      class="border-round"
      :class="activeWinId == appWindow.id ? 'bg-blue-500' : 'bg-gray-50'"
      @click="toggleMaximize(appWindow.id)"
    >
      <div v-tooltip.top="appWindow.app.name ?? 'app'">
        {{ appWindow.app.icon ?? "🌠" }}
      </div>
    </div>
  </div>

  <Dialog modal id="startMenu" v-model:visible="visibleMenu" class="w-3">
    <template #header>
      <div />
    </template>
    <div class="flex flex-column gap-3 h-full overflow-auto">
      <InputText
        id="searchStart"
        placeholder="Search apps..."
        v-model="search"
        class="flex-1 p-2 rounded-md border-0 bg-white/5 text-[var(--text)]"
        @keypress.enter="
          openWindow(filteredApps[0]);
          visibleMenu = false;
        "
      />

      <div class="flex flex-column text-xl h-full overflow-auto">
        <div
          v-for="app in filteredApps"
          :key="app.id"
          @click="
            openWindow(app);
            visibleMenu = false;
          "
          class="flex align-items-center cursor-pointer hover:bg-blue-50 p-2 border-round"
        >
          {{ app.icon }} {{ app.name }}
        </div>
      </div>
    </div>
  </Dialog>

  <ContextMenu ref="menu" :model="ctx.items" />
</template>

<script setup lang="ts">
const menu = ref();
const visibleMenu = ref();

// Tabs
const tabs = ref(["aasdasdsdd1", "aasdassdd2", "aasdsdd3"]);
const activeTabIndex = ref(1);

const handler = (e: KeyboardEvent) =>
  e.key == "Escape" && activeWinId.value
    ? closeWindow(activeWinId.value)
    : undefined;

onMounted(init);
function init() {
  window.addEventListener("keydown", handler);
  onBeforeUnmount(() => {
    window.removeEventListener("keydown", handler);
  });
}

// --- clock ---
// const clock = ref("");
// const clockFormat = ref({
//   year: "numeric",
//   month: "2-digit",
//   day: "2-digit",
//   hour: "2-digit",
//   minute: "2-digit",
//   second: "2-digit",
//   hour12: true,
// });
// const updateClock = () =>
//   (clock.value = new Date().toLocaleString("en-US", clockFormat.value));
// updateClock();
// setInterval(updateClock, 500);

const selectedApps = ref(new Set<string>());

// --- start menu ---
const startVisible = ref(false);
const search = ref("");

const filteredApps = computed(() =>
  installedApps
    ?.filter((a) => a.name?.toLowerCase().includes(search.value?.toLowerCase()))
    .sort((a, b) => {
      const aStartsWith = a.name
        .toLowerCase()
        .startsWith(search.value.toLowerCase());
      const bStartsWith = b.name
        .toLowerCase()
        .startsWith(search.value.toLowerCase());

      if (aStartsWith && !bStartsWith) {
        return -1;
      }
      if (!aStartsWith && bStartsWith) {
        return 1;
      }

      return a.name.localeCompare(b.name);
    })
);

// --- context menu ---
const ctx = reactive({
  items: [] as { label: string; command: () => void }[],
});
function showCtx(ev: MouseEvent, items) {
  ctx.items = items;
  menu.value.show(ev);
}
const openDesktopMenu = (ev: MouseEvent) =>
  showCtx(ev, [
    {
      label: "Search apps…",
      command: () => (visibleMenu.value = !visibleMenu.value),
    },
    { label: "Refresh", command: () => location.reload() },
    { label: "New note (desktop)", command: openWindow },
  ]);
const openAppCtx = (ev: MouseEvent, a) =>
  showCtx(ev, [
    { label: "Open", command: () => openWindow(a) },
    { label: "Hide", command: () => alert("app") },
    { label: "Uninstall", command: () => alert("app") },
  ]);

function toggleSelectApp(event: MouseEvent, appName: string) {
  const s = selectedApps.value;

  if (event.ctrlKey) {
    s.has(appName) ? s.delete(appName) : s.add(appName);
  } else {
    s.clear();
    s.add(appName);
  }
  selectedApps.value = new Set(s);
}

// --- window management ---

let z = 100;

const activeWinId = ref<string>();

function openWindow(app) {
  if (!app || !app.name) return;

  const existing = windows.find((w) => w.app.name == app.name);
  if (existing) {
    focusWindow(existing.id);
    return;
  }

  const id = String(++z);
  windows.push({
    id,
    app,
    state: window.innerWidth <= 768 ? "maximized" : "draggable",
  });
  setSize(id);
  focusWindow(id);
}

function focusWindow(id: string) {
  const w = windows.find((e) => e.id == id);
  if (!w) return;

  if (w.state != "pinned") {
    z++;
    const windowEl = document.getElementById(`window-${w.id}`);
    if (windowEl) windowEl.style.zIndex = String(z);
  }

  activeWinId.value = id;
}

function closeWindow(id: string) {
  const w = windows.find((e) => e.id == id);
  if (!w) return;

  if (w.state == "pinned") {
    const confirmation = confirm(
      "Are you sure you want to close this pinned window?"
    );
    if (!confirmation) return;
  }

  windows.splice(windows.indexOf(w), 1);

  let top = null;
  let maxZ = -1;
  for (const win of windows) {
    const winEl = document.getElementById(`window-${w.id}`);
    if (!winEl) continue;

    const zIndex = parseInt(winEl.style.zIndex);
    if (zIndex > maxZ) {
      top = win;
      maxZ = zIndex;
    }
  }
  activeWinId.value = top?.id ?? null;
}

function toggleMaximize(id: string) {
  const w = windows.find((w) => w.id == id);
  if (!w) return;

  const el = document.getElementById(`window-${w.id}`);
  if (!el) return;

  if (w.state == "maximized") {
    w.state = "draggable";
    el.style.width = w.style?.width ?? "400px";
    el.style.height = w.style?.height ?? "300px";
    el.style.left = w.style?.left ?? "0";
    el.style.top = w.style?.top ?? "0";
  } else {
    w.state = "maximized";
    el.style.left = "0px";
    el.style.top = "0px";
    el.style.width = `${window.innerWidth}px`;
    el.style.height = `${window.innerHeight}px`;
  }
}

function setSize(id: string) {
  const w = windows.find((e) => e.id == id);
  if (!w) return;

  const el = document.getElementById(`window-${w.id}`);
  if (!el) return;

  el.style.width = w.style?.width ?? "400px";
  el.style.height = w.style?.height ?? "300px";
  el.style.left = w.style?.left ?? "100px";
  el.style.top = w.style?.top ?? "100px";
}

function startDrag(e: MouseEvent, w) {
  if (w.state == "pinned") return;

  e.preventDefault();

  const htmlWindowElement = document.getElementById(`window-${w.id}`);
  if (!htmlWindowElement) return;

  focusWindow(w.id);

  const rect = (e.target as HTMLElement)
    .closest(".window")
    .getBoundingClientRect();

  const onMove = (ev: MouseEvent) => {
    htmlWindowElement.style.left = `${Math.max(
      0,
      Math.min(
        ev.clientX - (e.clientX - rect.left),
        window.innerWidth - rect.width
      )
    )}px`;
    htmlWindowElement.style.top = `${Math.max(
      0,
      Math.min(
        ev.clientY - (e.clientY - rect.top),
        window.innerHeight - rect.height
      )
    )}px`;
  };

  const stopDrag = () => {
    window.removeEventListener("mousemove", onMove);
    window.removeEventListener("mouseup", stopDrag);
  };

  window.addEventListener("mousemove", onMove);
  window.addEventListener("mouseup", stopDrag);
}

function startResize(e: MouseEvent, w) {
  e.preventDefault();
  const htmlWindowElement = document.getElementById(`window-${w.id}`);
  if (!htmlWindowElement) return;
  const rect = htmlWindowElement.getBoundingClientRect();
  const onMove = (ev: MouseEvent) => {
    htmlWindowElement.style.width =
      Math.max(240, rect.width + (ev.clientX - e.clientX)) + "px";
    htmlWindowElement.style.height =
      Math.max(140, rect.height + (ev.clientY - e.clientY)) + "px";
  };
  const onUp = () => {
    window.removeEventListener("mousemove", onMove);
    window.removeEventListener("mouseup", onUp);
  };
  window.addEventListener("mousemove", onMove);
  window.addEventListener("mouseup", onUp);

  console.log("asdasdasdasd");
  window.dispatchEvent(new Event("resizeWindow"));
}

function togglePin(id: string) {
  const w = windows.find((e) => e.id == id);
  if (!w) return;

  if (w.state == "pinned") {
    z++;
    const windowEl = document.getElementById(`window-${id}`);
    if (windowEl) {
      windowEl.style.zIndex = String(z);
    }
  } else {
    const windowEl = document.getElementById(`window-${id}`);
    if (windowEl) {
      windowEl.style.zIndex = String(--z);
    }
  }

  if (w.state == "pinned") {
    const pinnedWindows = windows.filter((win) => win.state == "pinned");
    const offsetX = 20;
    const offsetY = 20;
    const maxOffset = 100;
    const positionX = (pinnedWindows.length * offsetX) % maxOffset;
    const positionY = (pinnedWindows.length * offsetY) % maxOffset;

    const windowEl = document.getElementById(`window-${id}`);
    if (windowEl) {
      windowEl.style.left = `${window.innerWidth - 520 - positionX}px`;
      windowEl.style.top = `${window.innerHeight - 350 - positionY}px`;
    }
  }
}
</script>

<style scoped>
* {
  text-shadow: black 1px 1px 2px;
}

/* .tabs > * {
  border-width: 1px !important;
  border-style: solid;
}
.tabs > *:only-child {
  border-top: 5px;
}
.tabs > *:first-child {
  border-top-left-radius: 5px;
}
.tabs > *:last-child {
  border-top-right-radius: 5px;
} */

.window {
  position: absolute;
  overflow: hidden;
  display: flex;
  flex-direction: column;
  background-color: rgba(233, 233, 233, 0.2);
  border: 1px solid gray;
  border-radius: var(--p-content-border-radius);
}

.emoji-outline {
  pointer-events: none;
  /* text-shadow: 2px 0 0 rgba(255, 255, 255, 0.5),
    -2px 0 0 rgba(255, 255, 255, 0.5), 0 2px 0 rgba(255, 255, 255, 0.5),
    0 -2px 0 rgba(255, 255, 255, 0.5), 2px 2px 0 rgba(255, 255, 255, 0.5),
    -2px 2px 0 rgba(255, 255, 255, 0.5), 2px -2px 0 rgba(255, 255, 255, 0.5),
    -2px -2px 0 rgba(255, 255, 255, 0.5), 1px 1px 0 rgba(255, 255, 255, 0.5),
    -1px 1px 0 rgba(255, 255, 255, 0.5), 1px -1px 0 rgba(255, 255, 255, 0.5),
    -1px -1px 0 rgba(255, 255, 255, 0.5); */
}
.resizer {
  position: absolute;
  right: 0;
  bottom: 0;
  width: 16px;
  height: 16px;
  cursor: nwse-resize;
}
.backdrop-blur {
  /* backdrop-filter: blur(33px); */
}
.marquee-box {
  position: absolute;
  border: 1px dashed #06b6d4;
  background: rgba(6, 182, 212, 0.2);
  pointer-events: none;
}

.desktop {
  display: flex;
  flex-direction: row;
  width: 100%;
  justify-content: center;
  align-items: center;
}
.desktop-item {
  cursor: pointer;
  border-radius: var(--p-content-border-radius);
  padding: 0.5rem;
  display: flex;
  flex-direction: column;
  align-items: center;
  text-align: center;
}

#desktop {
  display: flex;
  flex-wrap: wrap;
}

#clock {
  font-family: "Orbitron", sans-serif;
  letter-spacing: 2px;
}

.window.pinned {
  border-color: #06b6d4; /* Accent color */
  z-index: 2147483647;
}
</style>
