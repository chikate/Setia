<template>
  <main>
    <div
      class="bg-cover bg-center"
      style="
        background-image: url('https://www.riotgames.com/darkroom/2000/6a2fc4addd2ad4793a915202395c8410:a762ac65c1b95d37880e5e267029cc38/son-riot-website.png');
      "
    >
      <main style="background: linear-gradient(rgba(23, 23, 23, 0) 20%, rgba(23, 23, 23, 1))">
        <a class="text-8xl font-bold">
          Hello {{ useAuthStore()?.getToken() ? 'Dragos' : 'there' }}
        </a>
        <a class="text-3xl font-bold">We are happy that you are here!</a>
      </main>
    </div>
    <div class="flex flex-column py-8">
      <CRUDT :store="usePontajStore()" class="py-8 my-8" />
      <CRUDT :store="useUserStore()" class="py-8 my-8" />
      <!-- <Calendar
        class="align-self-center flex flex-wrap"
        inline
        selectionMode="multiple"
        v-model="freeDays"
        :numberOfMonths="6"
      />
      <Calendar
        class="align-self-center flex flex-wrap"
        inline
        selectionMode="multiple"
        v-model="freeDays"
        :numberOfMonths="6"
      /> -->
      <!-- <CRUDT :store="useUserStore()" class="py-8 my-8">
        <template #expansion>
          <TreeSelect
            v-model="selectedValue"
            :options="nodes"
            selectionMode="checkbox"
            placeholder="Rights"
            class="w-20rem"
          />
        </template>
      </CRUDT> -->
      <StoryChapter />
    </div>
  </main>
</template>

<script setup lang="ts">
import { useAuthStore } from '@/stores/AuthStore'
import { usePontajStore } from '@/stores/PontajStore'
import { useUserStore } from '@/stores/generated/UserStore'
import { ref } from 'vue'

const nodes = ref([])
const selectedValue = ref([])

const freeDays = ref([
  new Date('1/1/' + new Date().getFullYear()),
  new Date('1/2/' + new Date().getFullYear()),
  new Date('1/6/' + new Date().getFullYear()),
  new Date('1/7/' + new Date().getFullYear()),
  new Date('1/24/' + new Date().getFullYear()),

  new Date('5/1/' + new Date().getFullYear()),
  new Date('5/5/' + new Date().getFullYear()),

  new Date('6/5/' + new Date().getFullYear()),
  new Date('6/23/' + new Date().getFullYear()),

  new Date('8/15/' + new Date().getFullYear()),

  new Date('9/30/' + new Date().getFullYear()),

  new Date('12/1/' + new Date().getFullYear()),
  new Date('12/25/' + new Date().getFullYear()),
  new Date('12/26/' + new Date().getFullYear()),
  new Date('12/31/' + new Date().getFullYear())
])
</script>
@/stores/PontajStore
