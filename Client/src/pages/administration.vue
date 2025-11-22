<template>
  <div class="flex flex-column justify-content-around gap-2">
    <div class="card p-1 flex flex-row bg-gray-800 border round">
      <Button label="Toggle Dark Mode" @click="darkMode().toggleDarkMode()" />
      <ColorPicker />
    </div>
    <CRUDT v-for="service in services" :key="service" :service />
  </div>
</template>

<script setup lang="ts">
const services = ref([]);

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
