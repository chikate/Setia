<template>
  <div class="flex-column">
    <div
      class="w-screen h-screen bg-cover bg-center flex flex-column"
      style="background-image: url(WorldMap.png)"
    >
      <div
        class="h-screen w-screen flex flex-column align-items-center justify-content-center"
        style="background: linear-gradient(rgba(23, 23, 23, 0) 20%, rgba(23, 23, 23, 1))"
      />
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, watch } from 'vue'
import { useRouter, useRoute } from 'vue-router'

const router = useRouter()
const route = useRoute()

const active = ref<number>(0)
const items = ref([
  {
    label: 'Europe',
    icon: 'pi pi-heart-fill',
    route: '/universe/region/e',
    background: '',
    description: ''
  },
  {
    label: 'Sumeru',
    icon: 'pi pi-heart-fill',
    route: '/universe/region/i',
    background: '',
    description: ''
  },
  {
    label: 'Deshert',
    icon: 'pi pi-heart-fill',
    route: '/universe/region/d',
    background: '',
    description: ''
  },
  {
    label: 'Vikings',
    icon: 'pi pi-heart-fill',
    route: '/universe/region/v',
    background: '',
    description: ''
  },
  {
    label: 'China',
    icon: 'pi pi-heart-fill',
    route: '/universe/region/c',
    background: '',
    description: ''
  },
  {
    label: 'Japan',
    icon: 'pi pi-heart-fill',
    route: '/universe/region/j',
    background: '',
    description: ''
  },
  {
    label: 'Pirates',
    icon: 'pi pi-heart-fill',
    route: '/universe/region/p',
    background: '',
    description: ''
  },
  {
    label: 'Beasts',
    icon: 'pi pi-heart-fill',
    route: '/universe/region/b',
    background: '',
    description: ''
  },
  {
    label: 'Academy',
    icon: 'pi pi-heart-fill',
    route: '/universe/region/a',
    background: '',
    description: ''
  }
])

onMounted(() => {
  active.value = items.value.findIndex((item) => route.path === router.resolve(item.route).path)
})

watch(
  route,
  () => {
    active.value = items.value.findIndex((item) => route.path === router.resolve(item.route).path)
  },
  { immediate: true }
)
</script>
