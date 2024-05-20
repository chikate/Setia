<template>
  <main v-if="async () => await useAuthStore().checkUserTags(['Role.Admin'])">
    <div class="flex flex-wrap gap-8 justify-content-start">
      <CRUDT :store="usePontajCRUDStore()" />
      <CRUDT :store="useUsersCRUDStore()" />
      <CRUDT :store="useTagsCRUDStore()" />
    </div>
  </main>
</template>

<script setup lang="ts">
import { usePontajCRUDStore } from '@/stores/cruds/PontajCRUDStore'
import { useUsersCRUDStore } from '@/stores/cruds/UsersCRUDStore'
import { useTagsCRUDStore } from '@/stores/cruds/TagsCRUDStore'
import { useAuthStore } from '@/stores/AuthStore'
</script>
