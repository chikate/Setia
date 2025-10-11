<template>
  <div class="flex flex-column">
    <div
      class="w-full h-screen bg-cover bg-center flex flex flex-column"
      style="
        background-image: url('https://compass-ssl.xboxlive.com/assets/7e/bd/7ebda893-32a6-4a05-9512-d2501bcce5df.jpg?n=NewPlayerOnboarding_dt_image.jpg');
      "
    >
      <div
        class="h-screen w-full flex flex flex-column align-items-center justify-content-center"
        style="
          background: linear-gradient(
            rgba(23, 23, 23, 0) 20%,
            rgba(23, 23, 23, 1)
          );
        "
      >
        <a class="text-8xl font-bold">Pirates</a>
        <a class="text-3xl font-bold"></a>
      </div>
    </div>
    <div class="flex flex-column p-4">
      <h1>Language</h1>
      <iframe
        width="100%"
        height="500"
        src="https://en.wikipedia.org/wiki/Egyptian_language"
      />
      <p>
        [Pirates] Inspiration - Sea of Thieves Tortuga - real life ref “Tortega”
        Monsters Kraken
      </p>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, watch } from "vue";
import { useRouter, useRoute } from "vue-router";

const router = useRouter();
const route = useRoute();

const active = ref<number>(0);
const items = ref([
  {
    label: "Europe",
    icon: "pi pi-heart-fill",
    route: "/universe/region/e",
    background: "",
    description: "",
  },
  {
    label: "Sumeru",
    icon: "pi pi-heart-fill",
    route: "/universe/region/i",
    background: "",
    description: "",
  },
  {
    label: "Deshert",
    icon: "pi pi-heart-fill",
    route: "/universe/region/d",
    background: "",
    description: "",
  },
  {
    label: "Vikings",
    icon: "pi pi-heart-fill",
    route: "/universe/region/v",
    background: "",
    description: "",
  },
  {
    label: "China",
    icon: "pi pi-heart-fill",
    route: "/universe/region/c",
    background: "",
    description: "",
  },
  {
    label: "Japan",
    icon: "pi pi-heart-fill",
    route: "/universe/region/j",
    background: "",
    description: "",
  },
  {
    label: "Pirates",
    icon: "pi pi-heart-fill",
    route: "/universe/region/p",
    background: "",
    description: "",
  },
  {
    label: "Beasts",
    icon: "pi pi-heart-fill",
    route: "/universe/region/b",
    background: "",
    description: "",
  },
  {
    label: "Academy",
    icon: "pi pi-heart-fill",
    route: "/universe/region/a",
    background: "",
    description: "",
  },
]);

onBeforeMount(() => {
  active.value = items.value.findIndex(
    (item) => route.path == router.resolve(item.route).path
  );
});

watch(
  route,
  () => {
    active.value = items.value.findIndex(
      (item) => route.path == router.resolve(item.route).path
    );
  },
  { immediate: true }
);
</script>
