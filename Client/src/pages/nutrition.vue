<script setup lang="ts">
definePage({
  meta: {
    title: "Nutrition",
    description: "",
    roles: ["user"],
  },
});

interface IFood {
  name: string;
  profile: { nutrient: string; value: string }[];
  effects: string[];
  spawnLocation: string;
}
const foods = defineModel("foods", {
  type: Array<IFood>,
  default: [
    {
      name: "Apple",
      profile: [
        { nutrient: "Calories", value: "~50" },
        { nutrient: "Sugar", value: "~10g" },
        { nutrient: "Fiber", value: "~3g" },
        { nutrient: "Vitamin C", value: "5%" },
        { nutrient: "Vitamin K", value: "3%" },
        { nutrient: "Copper", value: "3%" },
        { nutrient: "Potassium", value: "3%" },
        { nutrient: "Vitamin B2", value: "2%" },
        { nutrient: "Vitamin B6", value: "2%" },
        { nutrient: "Manganese", value: "2%" },
      ],
      effects: [
        "* Antioxidants - Vit C & Flavonoids: Quercetin, Catechin & Chlorogenic Acid - Anti-Inflammatory & Antiviral Effects. Lowers Blood Sugar. \nImprovement of Cognitive Function. Fiber - Satiety & Digestive Health. Reduces the Risk of Type 2 Diabetes, Heart Disease & Cancer.",
        "* Antioxidants - Vit C & Flavonoids: Quercetin, Catechin & Chlorogenic Acid - Anti-Inflammatory & Antiviral Effects. Lowers Blood Sugar.",
        "* Improvement of Cognitive Function. Fiber - Satiety & Digestive Health. Reduces the Risk of Type 2 Diabetes, Heart Disease & Cancer.",
      ],
    },
    {
      name: "Banana",
      profile: [
        { nutrient: "Calories", value: "90" },
        { nutrient: "Sugar", value: "12g" },
        { nutrient: "Fiber", value: "2.6g" },
        { nutrient: "Vitamin C", value: "15%" },
        { nutrient: "Vitamin B6", value: "20%" },
        { nutrient: "Potassium", value: "12%" },
        { nutrient: "Magnesium", value: "8%" },
        { nutrient: "Copper", value: "5%" },
      ],
      effects: [
        "* Rich in Potassium: Supports heart health and muscle function. \nImproves digestion and helps reduce bloating with its fiber content.",
        "* Vitamin B6: Aids in brain function and the production of neurotransmitters.",
        "* High in antioxidants: Protects against oxidative stress and inflammation.",
      ],
    },
    {
      name: "Green Apple",
      profile: [
        { nutrient: "Calories", value: "100" },
        { nutrient: "Fat", value: "0g" },
        { nutrient: "Protein", value: "0g" },
        { nutrient: "Carbs", value: "27g" },
        { nutrient: "Fiber", value: "4g" },
        { nutrient: "Vitamin C", value: "8%" },
        { nutrient: "Potassium", value: "5%" },
      ],
      effects: [
        "* Low in calories: Supports weight management.",
        "* Rich in fiber: Improves digestive health and supports gut health.",
        "* Vitamin C: Boosts immune system and promotes healthy skin.",
      ],
    },
    {
      name: "Orange",
      profile: [
        { nutrient: "Calories", value: "62" },
        { nutrient: "Sugar", value: "12g" },
        { nutrient: "Fiber", value: "3g" },
        { nutrient: "Vitamin C", value: "92%" },
        { nutrient: "Vitamin A", value: "5%" },
        { nutrient: "Potassium", value: "6%" },
        { nutrient: "Calcium", value: "4%" },
      ],
      effects: [
        "* High in Vitamin C: Strengthens the immune system and enhances skin health.",
        "* Antioxidants: Protects cells from oxidative stress.",
        "* Fiber: Aids in digestion and promotes gut health.",
      ],
    },
    {
      name: "Carrot",
      profile: [
        { nutrient: "Calories", value: "41" },
        { nutrient: "Sugar", value: "5g" },
        { nutrient: "Fiber", value: "2g" },
        { nutrient: "Vitamin A", value: "184%" },
        { nutrient: "Vitamin C", value: "7%" },
        { nutrient: "Potassium", value: "5%" },
        { nutrient: "Calcium", value: "3%" },
      ],
      effects: [
        "* Rich in Vitamin A: Supports eye health and vision.",
        "* High in antioxidants: Protects against oxidative damage and supports immune function.",
        "* Fiber: Improves digestion and supports healthy bowel movements.",
      ],
    },
    {
      name: "Spinach",
      profile: [
        { nutrient: "Calories", value: "23" },
        { nutrient: "Sugar", value: "0g" },
        { nutrient: "Fiber", value: "2.6g" },
        { nutrient: "Vitamin A", value: "56%" },
        { nutrient: "Vitamin C", value: "47%" },
        { nutrient: "Vitamin K", value: "460%" },
        { nutrient: "Iron", value: "15%" },
        { nutrient: "Calcium", value: "10%" },
      ],
      effects: [
        "* High in Vitamin K: Essential for bone health and blood clotting.",
        "* Rich in iron: Supports healthy red blood cell production.",
        "* Antioxidants: Helps protect cells from oxidative damage.",
      ],
    },
  ],
});

const route = useRoute();
const router = useRouter();

const selectedFood = ref(
  foods.value.find(
    (food) => String(food.name) === String(route.query.selection)
  ) || foods.value[0]
);
const selectionName = computed({
  get: () => String(route.query.selection || ""),
  set: (value) => {
    router.replace({
      query: { ...route.query, selection: value || undefined },
    });
    selectedFood.value =
      foods.value.find((food) => String(food.name) === value) || foods.value[0];
  },
});
const search = computed({
  get: () => String(route.query.search || ""),
  set: (value) =>
    router.replace({
      query: {
        ...route.query,
        search: value || undefined,
      },
    }),
});
onBeforeMount(init);
async function init() {}
</script>

<template>
  <div class="flex-row gap-3 p-3 h-full">
    <div class="flex-column flex-grow-1 gap-3 h-full" style="width: 20vw">
      <InputText v-model="search" placeholder="Search" />
      <div class="flex-wrap overflow-auto">
        <div
          v-for="item in foods?.filter((food) =>
            food.name.toLowerCase().includes(search.toLowerCase())
          )"
          :key="item.name"
          @click="selectionName = item.name"
          class="m-3 border-2 custom-shadow-1 cursor-pointer flex-column justify-content-between align-items-center"
          style="flex: 1 1 auto"
          :style="{
            'border-color': selectedFood.name == item.name ? 'blue' : 'gray',
          }"
        >
          <img
            style="max-width: 100px"
            :src="`/${item.name}.png`"
            class="w-full"
          />
          <label class="text-center pb-2">{{ item.name }}</label>
        </div>
      </div>
    </div>
    <div
      class="flex-column w-full gap-3 h-full overflow-auto"
      style="max-width: 800px"
    >
      <div
        class="flex-row align-items-center bg-gray-200 border-3 border-orange-500"
      >
        <div class="flex-column w-full h-full p-4">
          <label class="text-5xl font-semibold h-full align-items-center flex">
            {{ selectedFood.name }}
          </label>
          <label class="font-semibold text-orange-500">Legendary</label>
        </div>
        <img
          style="max-width: 100px"
          :src="`/${selectedFood.name}.png`"
          class="w-full"
        />
      </div>
      <div v-if="selectedFood.profile" class="flex-column gap-2">
        <label class="text-2xl font-semibold">Stats</label>
        <div
          class="px-2 flex-row gap-2 text-green-400"
          v-for="item in selectedFood.profile?.sort((a, b) =>
            a.nutrient.localeCompare(b.nutrient)
          )"
          :key="item.nutrient"
          style="min-width: 200px"
        >
          <div
            class="cursor-pointer w-full flex-row align-items-center gap-2"
            @click="console.log"
          >
            <img :src="`/${item.nutrient}.png`" style="width: 32px" />
            <label class="font-semibold text-xl hover:text-blue-200">
              {{ item.nutrient }}
            </label>
          </div>
          <label class="font-semibold text-xl text-left">
            {{ item.value }}
          </label>
        </div>
      </div>
      <div v-if="selectedFood.effects" class="flex-column gap-2">
        <label class="text-2xl font-semibold">Special effects</label>
        <div
          class="px-2 flex-row gap-2 text-green-400"
          v-for="item in selectedFood.effects"
          :key="item"
        >
          <label
            class="font-semibold text-xl cursor-pointer hover:text-blue-200"
          >
            {{ item }}
          </label>
        </div>
      </div>
      <div class="flex-column gap-3">
        <label class="text-2xl font-semibold">Spawn location</label>
        <img :src="selectedFood.spawnLocation" class="w-full" />
      </div>
    </div>
  </div>
</template>
