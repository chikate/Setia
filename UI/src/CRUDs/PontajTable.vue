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
          <div>
            {{ new Date(slotProps.data.beginTime).toLocaleDateString() }}
            {{ new Date(slotProps.data.beginTime).toLocaleTimeString() }}
          </div>
        </template>
      </Column>

      <Column field="endTime" header="Stop Time">
        <template #body="slotProps">
          <div>
            {{ new Date(slotProps.data.endTime).toLocaleDateString() }}
            {{ new Date(slotProps.data.endTime).toLocaleTimeString() }}
          </div>
        </template>
      </Column>
      <template #dialog>
        <Calendar
          showTime
          hourFormat="24"
          showWeek
          @update:model-value="
            usePontajStore().selectedItem.beginTime = `${new Date($event).toLocaleDateString()} ${new Date($event).toLocaleTimeString()}`
          "
          :model-value="usePontajStore().selectedItem.beginTime"
        />
      </template>
    </CRUDT>
  </div>
</template>

<script setup lang="ts">
import { usePontajStore } from '@/CRUDs/PontajStore'
import Column from 'primevue/column'
import CRUDT from './CRUDT.vue'

import Calendar from 'primevue/calendar'
</script>
