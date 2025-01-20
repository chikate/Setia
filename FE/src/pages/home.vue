<template>
  <div class="flex-row h-full">
    <div class="flex-column align-items-center gap-4 p-4 overflow-auto mm-w-100rem">
      <div
        v-for="(room, room_index) in rooms"
        :key="room_index"
        class="card flex-column custom-shadow-1 w-full"
        :class="{ 'border-gray-400': tabIndex[0] == room_index }"
        @click="tabIndex[0] = room_index"
      >
        <div
          class="flex-column gap-2 p-2 align-items-start border-round"
          :style="{
            backgroundImage: `linear-gradient(to top right, rgba(255, 255, 255, 1) 50%, rgba(255, 255, 255, 0.5)), url(${room.avatar})`,
            backgroundRepeat: 'no-repeat',
            backgroundPosition: 'top right',
            backgroundSize: '100% auto'
          }"
        >
          <InputText
            v-model="rooms[room_index].name"
            readonly
            class="w-full px-2 text-xl font-semibold border-none shadow-none z-1 bg-transparent"
          />
          <div class="flex-wrap align-items-center gap-2">
            <div
              v-for="(feature, feature_index) in room.features"
              :key="feature_index"
              class="card border-teal-500"
              v-show="feature.value"
            >
              <label
                class="cursor-pointer p-1 px-2 text-teal-500"
                @click="tabIndex[1] = feature_index"
              >
                {{ `${feature.name} ${feature.value}` }}
                {{
                  feature.unit == 'celsius'
                    ? '°C'
                    : feature.unit == 'kelvin'
                      ? 'K'
                      : feature.unit == 'faren'
                        ? '°F'
                        : feature.unit
                }}
              </label>
              <i class="pi pi-temperature" />
            </div>
          </div>
        </div>
        <div class="w-full p-4 pt-3" v-if="tabIndex[0] == room_index && tabIndex[1] == 0">
          <div class="flex-row gap-5 align-items-center">
            <Slider class="flex-grow-1" />
            <label class="pt-1">
              {{ rooms[room_index].features[0].value }}
              {{
                rooms[room_index].features[0].unit == 'celsius'
                  ? '°C'
                  : rooms[room_index].features[0].unit == 'kelvin'
                    ? 'K'
                    : rooms[room_index].features[0].unit == 'faren'
                      ? '°F'
                      : rooms[room_index].features[0].unit
              }}
            </label>
          </div>
        </div>
        <div class="w-full p-4 pt-3" v-if="tabIndex[0] == room_index && tabIndex[1] == 1">
          1 2 3 4 5 6 7
        </div>
      </div>
    </div>
    <div class="flex-wrap w-full gap-3 py-4">
      <div
        v-for="(floor, index) in [
          'data:image/png;base64,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',
          'data:image/png;base64,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',
          'data:image/png;base64,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'
        ]"
        :key="index"
      >
        <img :src="floor" class="w-full" />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
const tabIndex = ref([0, 0])
const rooms = ref([
  {
    name: 'Living Room',
    avatar:
      'https://a0.muscache.com/im/pictures/miso/Hosting-1301285001218713567/original/37fc611f-1766-4d77-83ec-680cb23bfa2d.jpeg?im_w=1200&im_format=avif',
    features: [
      { name: 'Temperature', value: 22, unit: 'celsius' },
      { name: 'Air', value: 97 },
      { name: 'Lights', value: 1 },
      { name: 'Power', value: 1, unit: 'W' }
    ]
  },
  {
    name: 'Kitchen',
    avatar:
      'https://a0.muscache.com/im/pictures/miso/Hosting-1301285001218713567/original/ad4f5c62-1e83-4b4f-a1e6-83996637d4d6.jpeg?im_w=1200&im_format=avif',
    features: [
      { name: 'Temperature', value: 22, unit: 'celsius' },
      { name: 'Air', value: 97 },
      { name: 'Lights', value: 0 },
      { name: 'Power', value: 100, unit: 'W' }
    ]
  },
  {
    name: 'Bathroom 1',
    avatar:
      'https://a0.muscache.com/im/pictures/miso/Hosting-1301285001218713567/original/a4d0b7b9-1a25-463d-80b9-cb53cbce5d26.jpeg?im_w=720&im_format=avif',
    features: [
      { name: 'Temperature', value: 22, unit: 'celsius' },
      { name: 'Air', value: 97 },
      { name: 'Lights', value: 0 },
      { name: 'Power', value: 0, unit: 'W' }
    ]
  },
  {
    name: 'Bathroom 2',
    avatar: 'Bedroom 3',
    features: [
      { name: 'Temperature', value: 22, unit: 'celsius' },
      { name: 'Air', value: 97 },
      { name: 'Lights', value: 0 },
      { name: 'Power', value: 0, unit: 'W' }
    ]
  },
  {
    name: 'Bedroom 1',
    avatar:
      'https://a0.muscache.com/im/pictures/miso/Hosting-1301285001218713567/original/119db8a2-27c9-4734-8c87-9caa65c21af3.jpeg?im_w=720&im_format=avif',
    features: [
      { name: 'Temperature', value: 22, unit: 'celsius' },
      { name: 'Air', value: 97 },
      { name: 'Lights', value: 0 },
      { name: 'Power', value: 0, unit: 'W' }
    ]
  },
  {
    name: 'Bedroom 2',
    avatar:
      'https://a0.muscache.com/im/pictures/miso/Hosting-1301285001218713567/original/52388b70-0069-4034-b9bb-a6f1e292b994.jpeg?im_w=720&im_format=avif',
    features: [
      { name: 'Temperature', value: 22, unit: 'celsius' },
      { name: 'Air', value: 97 },
      { name: 'Lights', value: 0 },
      { name: 'Power', value: 0, unit: 'W' }
    ]
  }
])
</script>

<style scoped></style>
