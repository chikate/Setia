<template>
  <div class="flex-column">
    <div
      class="w-screen h-screen bg-cover bg-center flex flex-column"
      style="
        background-image: url('https://cdnb.artstation.com/p/assets/images/images/020/631/361/4k/maria-puchkova-5.jpg?1568568172');
      "
    >
      <div
        class="h-screen w-screen flex flex-column align-items-center justify-content-center"
        style="background: linear-gradient(rgba(23, 23, 23, 0) 20%, rgba(23, 23, 23, 1))"
      >
        <a class="text-8xl font-bold">Vikings</a>
        <a class="text-3xl font-bold"></a>
      </div>
    </div>
    <div class="flex-column p-4">
      <img src="https://i.ex-cdn.com/mgn.vn/files/content/2022/08/17/one-piece-1245.jpg" />
      <h1>Language</h1>
      <iframe width="100%" height="500" src="" />
      <p>
        [Vikings] (Russia) - Territory (Scandinavia) - Culture Inspiration - Elbaf (One Piece)
        Language https://en.wikipedia.org/wiki/Old_Norse#Phonology Monsters Fenryr
      </p>
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
