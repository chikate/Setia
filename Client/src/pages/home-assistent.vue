<template>
  <div class="flex flex-column align-items-center w-full">
    <div v-for="(room, room_index) in rooms" :key="room_index"
      class="flex flex-column custom-shadow-1 border-round overflow-auto bg-white w-11 my-3"
      :class="{ 'border-gray-400': tabIndex[0] == room_index }" @click="tabIndex[0] = room_index">
      <div class="flex flex-column p-2 align-items-start" :style="{
        backgroundImage: `linear-gradient(to top right, rgba(255,255,255,1) 50%, rgba(255,255,255,0.5)), url(${room.avatar})`,
        backgroundRepeat: 'no-repeat',
        backgroundPosition: 'top right',
        backgroundSize: '100% auto',
      }">
        <InputText v-model="rooms[room_index].name" readonly
          class="px-2 text-xl font-semibold border-none shadow-none z-1 bg-transparent text-gray-500" />

        <div class="flex-wrap align-items-center">
          <div v-for="(feature, feature_index) in room.features" :key="feature_index" v-show="feature.value"
            class="border-teal-500 m-1">
            <label class="cursor-pointer p-1 px-2 text-teal-500" @click="tabIndex[1] = feature_index">
              {{ `${feature.name} ${feature.value}` }}
              {{ mapUnit(feature.unit) }}
            </label>
            <i class="pi pi-temperature" />
          </div>
        </div>
      </div>

      <div class="p-4 pt-3" v-if="tabIndex[0] == room_index && tabIndex[1] == 0">
        <div class="flex flex-column gap-4">
          <div class="flex flex-row align-items-center px-3">
            <label class="pt-1 text-gray-500 text-xl font-semibold">
              {{ rooms[room_index].features[0].value }}{{ mapUnit(rooms[room_index].features[0].unit) }}
            </label>
            <Slider class="flex-grow-1 ml-4" v-model="rooms[room_index].features[0].value" :min="10" :max="35" />
          </div>

          <div class="w-full h-12rem">
            <LineChart :dataset="temperatureHistory[room_index]" />
          </div>
        </div>
      </div>

      <div class="p-4 pt-3" v-if="tabIndex[0] == room_index && tabIndex[1] == 1">
        Air Quality Controls Coming Soon...
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">

defineOptions({ name: "Home Assistant", icon: "🏡", role: ["Admin"] });

const tabIndex = ref([0, 0]);

const mapUnit = (unit: string) =>
  unit == "celsius"
    ? "°C"
    : unit == "kelvin"
      ? "K"
      : unit == "faren"
        ? "°F"
        : unit || "";

const rooms = ref([
  {
    name: "Living Room",
    avatar:
      "https://a0.muscache.com/im/pictures/miso/Hosting-1301285001218713567/original/37fc611f-1766-4d77-83ec-680cb23bfa2d.jpeg?im_w=1200&im_format=avif",
    features: [
      { name: "Temperature", value: 22, unit: "celsius" },
      { name: "Air", value: 97 },
      { name: "Lights", value: 1 },
      { name: "Power", value: 1, unit: "W" },
    ],
  },
]);

const temperatureHistory = ref([
  Array.from({ length: 24 }, () => Math.floor(20 + Math.random() * 4)),
]);

onMounted(() => {
  setInterval(() => {
    temperatureHistory.value[0].push(20 + Math.random() * 4);
    temperatureHistory.value[0].shift();
  }, 3000);
});
</script>
