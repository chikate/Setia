<template>
  <main>
    <h1>Adm</h1>
    <div class="flex flex-column py-8 w-full">
      <CRUDT :store="usePontajCRUDStore()" class="py-8 my-8" />
      <CRUDT :store="useUserCRUDStore()" class="py-8 my-8">
        <template #expansion>
          <div class="flex flex-row gap-2 align-items-center">
            <Avatar image="https://localhost:44381/1/path36.png" size="xlarge" shape="circle" />
            <MultiSelect
              @before-show="useRoleCRUDStore().getAll()"
              :options="useRoleCRUDStore().allLoadedItems"
              option-label="name"
              option-value="id"
              display="chip"
              placeholder="Roles"
            />
            <TreeSelect
              v-model="useHelperStore().focusedActions"
              @before-show="useHelperStore().getAllActions()"
              :options="useHelperStore().allLoadedActions"
              @update:model-value="
                console.log(useHelperStore().focusedActions.split('Controller.')[0])
              "
              selectionMode="checkbox"
              filter
              display="chip"
              placeholder="Custom rights"
            />
          </div>
        </template>
      </CRUDT>
      <CRUDT :store="useRoleCRUDStore()" class="py-8 my-8">
        <template #expansion>
          <div class="flex flex-row gap-2 align-items-center">
            <MultiSelect
              :options="useRoleCRUDStore().allLoadedItems"
              option-label="name"
              option-value="id"
              display="chip"
              placeholder="Inherited roles"
              class="align-self-start"
            />
            <TreeSelect
              v-model="useHelperStore().focusedActions"
              @before-show="useHelperStore().getAllActions()"
              :options="useHelperStore().allLoadedActions"
              @update:model-value="
                console.log(useHelperStore().focusedActions.split('Controller.')[0])
              "
              selectionMode="checkbox"
              filter
              display="chip"
              placeholder="Rights"
            />
            <MultiSelect
              @before-show="useUserCRUDStore().getAll()"
              :options="useUserCRUDStore().allLoadedItems"
              option-label="email"
              option-value="id"
              display="chip"
              placeholder="Assigned to"
            />
          </div>
        </template>
      </CRUDT>
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
      <StoryChapter />
    </div>
  </main>
</template>

<script setup>
import { usePontajCRUDStore } from '@/stores/cruds/PontajCRUDStore'
import { useUserCRUDStore } from '@/stores/cruds/UsersCRUDStore'
import { useRoleCRUDStore } from '@/stores/cruds/RolesCRUDStore'
import { useHelperStore } from '@/stores/HelperStore'
</script>
