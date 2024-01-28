<template>
  <div>
    <CRUDT :store="useUsersStore()">
      <Column field="email" header="Email">
        <template #body="slotProps">
          {{ slotProps.data.email }}
        </template>
      </Column>
      <Column field="name" header="Name">
        <template #body="slotProps">
          {{ slotProps.data.name }}
        </template>
      </Column>
      <Column field="authorityCode" header="Role">
        <template #body="slotProps">
          {{ slotProps.data.authorityCode ?? 'None' }}
        </template>
      </Column>
      <template #dialog>
        <span class="p-float-label">
          <InputText v-model="useUsersStore().selectedItem.email" />
          <label>Email</label>
        </span>
        <span class="p-float-label">
          <InputText v-model="useUsersStore().selectedItem.name" />
          <label>Name</label>
        </span>
        <span class="flex flex-row align-items-center gap-2">
          <Checkbox v-model="useUsersStore().selectedItem.active" binary />
          <label>Active</label>
        </span>
      </template>
    </CRUDT>
  </div>
</template>

<script setup lang="ts">
import { useUsersStore } from '@/CRUDs/UsersStore'
import CRUDT from './CRUDT.vue'

import Column from 'primevue/column'

import InputText from 'primevue/inputtext'
import Checkbox from 'primevue/checkbox'
</script>
