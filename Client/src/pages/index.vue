<template>
  <main class="flex flex-column">
    <div id="desktop" @click.self="clearSelection">
      <div
        v-for="app in installedApps"
        :key="app.name"
        :class="['desktop-item', isSelectedApp(app.name) ? 'selected' : '']"
        @click="
          isAdmin() ? selectApp($event, app.name) : $router.push(app.__name)
        "
        @dblclick="launch(app)"
        draggable
      >
        <!-- @contextmenu.prevent="showMenu($event, appMenu(app))" -->
        <label class="icon">{{ app.icon || "🌠" }}</label>
        {{ app.name }}
      </div>
    </div>
    <div
      v-for="[id, win] in openedWindows"
      :key="id"
      :id="`win-${id}`"
      class="window"
      :class="[
        { pinned: win.state == 'pinned' },
        activeWindowId == id ? 'active' : '',
      ]"
      @mousedown="focusWindow(id)"
    >
      <div class="title" @mousedown.stop="dragWindow($event, win)">
        {{ win.app.icon }} {{ win.app.name }}
        <div class="flex-grow-1" />
        <span @click="resetWindow(id)">▣</span>
        <span @click="pinWindow(id)">
          {{ win.state == "pinned" ? "📍" : "📌" }}
        </span>
        <span class="close-window" @click="closeWindow(id)">✕</span>
      </div>
      <div class="overflow-auto relative">
        <component :is="win.app" />
      </div>
      <div
        class="resizer"
        @mousedown.stop.prevent="resizeWindow($event, win)"
      />
    </div>
    <div id="tray">
      <div
        v-for="[id, win] in openedWindows"
        :key="id"
        class="tray"
        :class="activeWindowId == id ? 'active' : ''"
        @click="maximize(id)"
        v-tooltip.top="win.app.name"
      >
        {{ win.app.icon || "🌠" }}
      </div>
    </div>
  </main>
</template>

<script setup lang="ts">
const activeWindowId = ref<string>();
const selectedApps = ref(new Set<string>());
const openedWindows = ref<Map<string, any>>(new Map());
let z = 100;

const appMenu = (app) => [
  { label: "▶️ Open", command: () => launch(app) },
  {
    label: "⭐ Favorite",
    command: () =>
      osStore().addToFavorites({
        label: `${app.icon} ${app.name}`,
        route: app.__name,
      }),
  },
  { label: "❌ Uninstall", command: () => alert(`Uninstall ${app.name}`) },
];
const clearSelection = () => selectedApps.value.clear();
const isSelectedApp = (name) => selectedApps.value.has(name);
const selectApp = (e, name) => {
  const selectedApps_local = selectedApps.value;
  e.ctrlKey
    ? selectedApps_local.has(name)
      ? selectedApps_local.delete(name)
      : selectedApps_local.add(name)
    : (selectedApps_local.clear(), selectedApps_local.add(name));
  selectedApps.value = new Set(selectedApps_local);
};
const launch = (app) => {
  if (!app?.name) return;
  const existing = [...openedWindows.value.values()].find(
    (w) => w.app.name == app.name
  );
  if (existing) return focusWindow(existing.id);
  const id = String(++z);
  openedWindows.value.set(id, {
    id,
    app,
    state: innerWidth <= 768 ? "maximized" : "draggable",
  });
  resetWindow(id);
  focusWindow(id);
};

const focusWindow = (id) => {
  const el = document.getElementById(`win-${id}`);
  if (!el) return;
  if (!el.classList.contains("pinned")) el.style.zIndex = String(z++);
  activeWindowId.value = id;
};

const closeWindow = (id) => {
  openedWindows.value.delete(id);
  activeWindowId.value = [...openedWindows.value.keys()].at(-1);
};

const maximize = (id) => {
  const el = document.getElementById(`win-${id}`);
  const win = openedWindows.value.get(id);
  if (!el || !win) return;
  if (win.state == "maximized") return resetWindow(id);
  Object.assign(el.style, {
    left: 0,
    top: 0,
    width: `${innerWidth}px`,
    height: `${innerHeight}px`,
  });
  win.state = "maximized";
};

const resetWindow = (id) => {
  const el = document.getElementById(`win-${id}`);
  if (!el) return;
  Object.assign(el.style, {
    width: "400px",
    height: "300px",
    left: "100px",
    top: "100px",
  });
};

const resizeWindow = (e, win) => {
  const el = document.getElementById(`win-${win.id}`);
  if (!el) return;
  const rect = el.getBoundingClientRect();
  const move = (ev) =>
    Object.assign(el.style, {
      width: `${Math.max(240, rect.width + ev.clientX - e.clientX)}px`,
      height: `${Math.max(140, rect.height + ev.clientY - e.clientY)}px`,
    });
  const stop = () => (off("mousemove", move), off("mouseup", stop));
  on("mousemove", move);
  on("mouseup", stop);
};

const dragWindow = (e, w) => {
  if (w.state == "pinned") return;
  e.preventDefault();
  const el = document.getElementById(`win-${w.id}`);
  el.style.position = "fixed";
  const r = el.getBoundingClientRect(),
    dx = e.clientX - r.left,
    dy = e.clientY - r.top;
  const move = (ev) =>
    Object.assign(el.style, {
      left: `${ev.clientX - dx}px`,
      top: `${ev.clientY - dy}px`,
    });
  on("mousemove", move);
  on("mouseup", () => off("mousemove", move));
};

const pinWindow = (id) => {
  const win = openedWindows.value.get(id);
  if (win) win.state = win.state == "pinned" ? "draggable" : "pinned";
};

const on = (ev, fn) => addEventListener(ev, fn);
const off = (ev, fn) => removeEventListener(ev, fn);
</script>

<style scoped>
#desktop {
  display: flex;
  flex-wrap: wrap;
  align-items: start;
  overflow: auto;
  gap: 0.5rem;
}
.desktop-item {
  cursor: pointer;
  padding: 0.5rem;
  border-radius: 0.5rem;
  display: flex;
  flex-direction: column;
  align-items: center;
  > * {
    pointer-events: none;
  }
}
.desktop-item.selected {
  background: #519fff;
}
.desktop-item:hover {
  background: #bfdbfe;
}
.icon {
  font-size: 2.5rem;
}
.window {
  transition: none;
  position: absolute;
  display: flex;
  flex-direction: column;
  border: 1px solid #ccc;
  border-radius: 0.5rem;
  background: #eee;
  overflow: hidden;
}
.title {
  background: #555;
  color: white;
  cursor: move;
  display: flex;
  justify-content: space-between;
  align-items: center;
  > span {
    cursor: pointer;
    padding: 0.5rem;
  }
}
.resizer {
  position: absolute;
  right: 0;
  bottom: 0;
  width: 12px;
  height: 12px;
  cursor: nwse-resize;
}
.tray {
  display: inline-block;
  padding: 0.25rem 0.5rem;
  border-radius: 0.25rem;
}
.tray.active {
  background: #3b82f6;
  color: white;
}
.close-window:hover {
  background: red;
  color: white;
}
.window.pinned {
  border-color: #06b6d4;
  z-index: 2147483647;
}
</style>
