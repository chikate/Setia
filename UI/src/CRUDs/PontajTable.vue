<template>
  <div>
    <CRUDT :store="usePontajStore()">
      <Column field="id_User" header="User">
        <template #body="slotProps">
          <div>
            {{ slotProps.data.user.email }}
          </div>
        </template>
      </Column>
      <Column field="beginTime" header="Start Time">
        <template #body="slotProps">
          {{ new Date(slotProps.data.beginTime).toLocaleString() }}
        </template>
      </Column>
      <Column field="endTime" header="Stop Time">
        <template #body="slotProps">
          {{ new Date(slotProps.data.endTime).toLocaleString() }}
        </template>
      </Column>
      <template #dialog>
        <span class="p-float-label">
          <Calendar
            showTime
            hourFormat="24"
            v-model:model-value="usePontajStore().selectedItem.beginTime"
          />
          <label>Date</label>
        </span>
        <span class="flex flex-row align-items-center gap-2">
          <Checkbox v-model="usePontajStore().selectedItem.active" binary />
          <label>Active</label>
        </span>
      </template>
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
import CRUDT from './CRUDT.vue'

import Column from 'primevue/column'

import Calendar from 'primevue/calendar'
import Checkbox from 'primevue/checkbox'

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
