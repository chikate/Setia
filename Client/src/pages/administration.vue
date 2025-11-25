<template>
  <div class="flex flex-column justify-content-around gap-2">
    <div
      class="card p-3 flex flex-column gap-4 bg-surface-0 border-round shadow-2"
    >
      <div class="flex flex-row align-items-center gap-4 flex-wrap">
        <div class="flex align-items-center gap-2">
          <span class="text-surface-900 dark:text-surface-0 font-bold">
            Dark Mode
          </span>
          <Button
            :icon="themeManager().isDarkMode.value ? 'pi pi-moon' : 'pi pi-sun'"
            :class="
              themeManager().isDarkMode.value
                ? 'p-button-rounded p-button-text p-button-secondary'
                : 'p-button-rounded p-button-text p-button-warning'
            "
            @click="themeManager().toggleDarkMode()"
          />
        </div>
        <div class="flex align-items-center gap-2">
          <span class="text-surface-900 dark:text-surface-0 font-bold">
            Theme Color
          </span>
          <ColorPicker
            :modelValue="themeManager().primaryColor.value.replace('#', '')"
            @update:modelValue="(val) => themeManager().setPrimaryColor(val)"
            format="hex"
          />
          <span class="text-surface-700 dark:text-surface-300 text-sm">
            {{ themeManager().primaryColor.value }}
          </span>
        </div>
      </div>

      <!-- Preset Colors -->
      <div class="flex flex-column gap-2">
        <span
          class="text-surface-700 dark:text-surface-300 font-semibold text-sm"
        >
          Quick Presets
        </span>
        <div class="flex gap-2 flex-wrap">
          <Button
            v-for="preset in colorPresets"
            :key="preset.name"
            :label="preset.name"
            :style="{
              backgroundColor: preset.color,
              borderColor: preset.color,
            }"
            size="small"
            class="text-white"
            @click="themeManager().setPrimaryColor(preset.color)"
          />
        </div>
      </div>

      <!-- Theme Preview -->
      <div class="flex flex-column gap-3">
        <h3 class="text-surface-900 dark:text-surface-0 m-0">Theme Preview</h3>

        <!-- Primary Color Shades -->
        <div class="flex flex-column gap-2">
          <span
            class="text-surface-700 dark:text-surface-300 font-semibold text-sm"
          >
            Primary Color Palette
          </span>
          <div class="flex gap-2 flex-wrap">
            <div
              v-for="shade in [
                50, 100, 200, 300, 400, 500, 600, 700, 800, 900, 950,
              ]"
              :key="shade"
              class="flex flex-column align-items-center gap-1"
            >
              <div
                :style="{ backgroundColor: `var(--p-primary-${shade})` }"
                class="w-3rem h-3rem border-round shadow-1"
              />
              <span class="text-xs text-surface-600 dark:text-surface-400">
                {{ shade }}
              </span>
            </div>
          </div>
        </div>

        <!-- UI Components Preview -->
        <div class="flex flex-column gap-2">
          <span
            class="text-surface-700 dark:text-surface-300 font-semibold text-sm"
          >
            Component Examples
          </span>
          <div class="flex gap-2 flex-wrap align-items-center">
            <Button label="Primary" severity="primary" />
            <Button label="Secondary" severity="secondary" />
            <Button label="Success" severity="success" />
            <Button label="Info" severity="info" />
            <Button label="Warning" severity="warning" />
            <Button label="Danger" severity="danger" />
            <Button label="Outlined" severity="primary" outlined />
            <Button label="Text" severity="primary" text />
          </div>
        </div>
      </div>
    </div>
    <CRUDT v-for="service in services" :key="service" :service />
  </div>
</template>

<script setup lang="ts">
const services = ref([]);

const colorPresets = [
  { name: "Indigo", color: "#6366f1" },
  { name: "Blue", color: "#3b82f6" },
  { name: "Purple", color: "#a855f7" },
  { name: "Pink", color: "#ec4899" },
  { name: "Rose", color: "#f43f5e" },
  { name: "Orange", color: "#f97316" },
  { name: "Amber", color: "#f59e0b" },
  { name: "Lime", color: "#84cc16" },
  { name: "Green", color: "#22c55e" },
  { name: "Emerald", color: "#10b981" },
  { name: "Teal", color: "#14b8a6" },
  { name: "Cyan", color: "#06b6d4" },
  { name: "Sky", color: "#0ea5e9" },
];

onBeforeMount(init);
async function init() {
  const openapi = await (await fetch("/openapi/v1.json")).json();
  const paths = openapi.paths;

  const grouped = {};

  for (const [path, methods] of Object.entries(paths)) {
    const parts = path.split("/").filter(Boolean);
    const resource = parts[1];

    if (!resource) continue;
    if (!grouped[resource]) {
      grouped[resource] = {
        name: resource,
        basePath: `/api/${resource}`,
        methods: {},
      };
    }

    for (const [verb, def] of Object.entries(methods))
      grouped[resource].methods[verb] = def;
  }

  services.value = Object.values(grouped);
}

defineOptions({
  name: "Administration",
  icon: "⚙️",
  role: ["Admin"],
});
</script>
