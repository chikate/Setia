<template>
  <div class="flex flex-column align-items-center">
    <div
      v-for="(room, room_index) in rooms"
      :key="room_index"
      class="flex flex-column custom-shadow-1 border-round overflow-auto bg-white"
      :class="{ 'border-gray-400': tabIndex[0] == room_index }"
      @click="tabIndex[0] = room_index"
    >
      <div
        class="flex flex-column p-2 align-items-start"
        :style="{
          backgroundImage: `linear-gradient(to top right, rgba(255, 255, 255, 1) 50%, rgba(255, 255, 255, 0.5)), url(${room.avatar})`,
          backgroundRepeat: 'no-repeat',
          backgroundPosition: 'top right',
          backgroundSize: '100% auto',
        }"
      >
        <InputText
          v-model="rooms[room_index].name"
          readonly
          class="px-2 text-xl font-semibold border-none shadow-none z-1 bg-transparent text-gray-500"
        />
        <div class="flex-wrap align-items-center">
          <div
            v-for="(feature, feature_index) in room.features"
            :key="feature_index"
            class="border-teal-500"
            v-show="feature.value"
          >
            <label
              class="cursor-pointer p-1 px-2 text-teal-500"
              @click="tabIndex[1] = feature_index"
            >
              {{ `${feature.name} ${feature.value}` }}
              {{
                feature.unit == "celsius"
                  ? "°C"
                  : feature.unit == "kelvin"
                  ? "K"
                  : feature.unit == "faren"
                  ? "°F"
                  : feature.unit
              }}
            </label>
            <i class="pi pi-temperature" />
          </div>
        </div>
      </div>
      <div
        class="p-4 pt-3"
        v-if="tabIndex[0] == room_index && tabIndex[1] == 0"
      >
        <div class="flex flex-row align-items-center px-3">
          <label class="pt-1 text-gray-500">
            {{ rooms[room_index].features[0].value }}
            {{
              rooms[room_index].features[0].unit == "celsius"
                ? "°C"
                : rooms[room_index].features[0].unit == "kelvin"
                ? "K"
                : rooms[room_index].features[0].unit == "faren"
                ? "°F"
                : rooms[room_index].features[0].unit
            }}
          </label>
          <Slider class="flex-grow-1" />
        </div>
      </div>
      <div
        class="p-4 pt-3"
        v-if="tabIndex[0] == room_index && tabIndex[1] == 1"
      >
        1 2 3 4 5 6 7
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
defineOptions({
  name: "My Home",
  icon: "🏡",
  role: ["Admin"],
});

const tabIndex = ref([0, 0]);
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
  {
    name: "Kitchen",
    avatar:
      "https://a0.muscache.com/im/pictures/miso/Hosting-1301285001218713567/original/ad4f5c62-1e83-4b4f-a1e6-83996637d4d6.jpeg?im_w=1200&im_format=avif",
    features: [
      { name: "Temperature", value: 22, unit: "celsius" },
      { name: "Air", value: 97 },
      { name: "Lights", value: 0 },
      { name: "Power", value: 100, unit: "W" },
    ],
  },
  {
    name: "Bathroom 1",
    avatar:
      "https://a0.muscache.com/im/pictures/miso/Hosting-1301285001218713567/original/a4d0b7b9-1a25-463d-80b9-cb53cbce5d26.jpeg?im_w=720&im_format=avif",
    features: [
      { name: "Temperature", value: 22, unit: "celsius" },
      { name: "Air", value: 97 },
      { name: "Lights", value: 0 },
      { name: "Power", value: 0, unit: "W" },
    ],
  },
  {
    name: "Bathroom 2",
    avatar: "Bedroom 3",
    features: [
      { name: "Temperature", value: 22, unit: "celsius" },
      { name: "Air", value: 97 },
      { name: "Lights", value: 0 },
      { name: "Power", value: 0, unit: "W" },
    ],
  },
  {
    name: "Bedroom 1",
    avatar:
      "https://a0.muscache.com/im/pictures/miso/Hosting-1301285001218713567/original/119db8a2-27c9-4734-8c87-9caa65c21af3.jpeg?im_w=720&im_format=avif",
    features: [
      { name: "Temperature", value: 22, unit: "celsius" },
      { name: "Air", value: 97 },
      { name: "Lights", value: 0 },
      { name: "Power", value: 0, unit: "W" },
    ],
  },
  {
    name: "Bedroom 2",
    avatar:
      "https://a0.muscache.com/im/pictures/miso/Hosting-1301285001218713567/original/52388b70-0069-4034-b9bb-a6f1e292b994.jpeg?im_w=720&im_format=avif",
    features: [
      { name: "Temperature", value: 22, unit: "celsius" },
      { name: "Air", value: 97 },
      { name: "Lights", value: 0 },
      { name: "Power", value: 0, unit: "W" },
    ],
  },
]);
</script>
