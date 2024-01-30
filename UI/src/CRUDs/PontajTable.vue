<template>
  <div>
    <CRUDT
      :store="usePontajStore()"
      :exposedData="[
        { field: 'user.email', header: 'Email' },
        { field: 'beginTime', header: 'Begin Time' },
        { field: 'endTime', header: 'End Time' },
        { field: 'description' }
      ]"
    >
    </CRUDT>
    <Calendar
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
    />
  </div>
</template>

<script setup lang="ts">
import { usePontajStore } from '@/CRUDs/PontajStore'
import { ref } from 'vue'
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
