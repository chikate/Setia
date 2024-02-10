<script setup lang="ts">
import { useAuthStore } from '@/stores/AuthStore'
import { usePontajCRUDStore } from '@/stores/generated/PontajCRUDStore'
import { useUserCRUDStore } from '@/stores/generated/UsersCRUDStore'
import { useRoleCRUDStore } from '@/stores/generated/RolesCRUDStore'
import { useRightsCRUDStore } from '@/stores/generated/RightsCRUDStore'
</script>

<template>
  <main>
    <div
      class="bg-cover bg-center"
      style="
        background-image: url('https://www.riotgames.com/darkroom/2000/6a2fc4addd2ad4793a915202395c8410:a762ac65c1b95d37880e5e267029cc38/son-riot-website.png');
      "
    >
      <main style="background: linear-gradient(rgba(23, 23, 23, 0) 20%, rgba(23, 23, 23, 1))">
        <a class="text-8xl text-center font-bold">
          Hello {{ useAuthStore()?.getToken() ? 'Dragos' : 'there' }}
        </a>
        <a class="text-3xl text-center font-bold">We are happy that you are here!</a>
      </main>
    </div>
    <div class="flex flex-column py-8">
      <CRUDT :store="usePontajCRUDStore()" class="py-8 my-8" />
      <CRUDT :store="useUserCRUDStore()" class="py-8 my-8">
        <template #expansion>
          <div class="flex flex-column">
            <MultiSelect
              @before-show="useRoleCRUDStore().getAll()"
              :options="useRoleCRUDStore().allLoadedItems"
              option-label="name"
              option-value="id"
              display="chip"
              placeholder="Roles"
            />
            <MultiSelect
              @before-show="useRightsCRUDStore().getAll()"
              :options="useRightsCRUDStore().allLoadedItems"
              option-label="name"
              option-value="id"
              display="chip"
              placeholder="Custom rights"
            />
          </div>
        </template>
      </CRUDT>
      <CRUDT :store="useRoleCRUDStore()" class="py-8 my-8">
        <template #expansion>
          <div class="flex flex-column">
            <MultiSelect
              @before-show="useRightsCRUDStore().getAll()"
              :options="useRightsCRUDStore().allLoadedItems"
              option-label="name"
              option-value="id"
              display="chip"
              placeholder="Rights"
            />
            <MultiSelect
              @before-show="useUserCRUDStore().getAll()"
              :options="useUserCRUDStore().allLoadedItems"
              option-label="name"
              option-value="id"
              display="chip"
              placeholder="Assigned to"
            />
          </div>
        </template>
      </CRUDT>
      <CRUDT :store="useRightsCRUDStore()" class="py-8 my-8" />
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
