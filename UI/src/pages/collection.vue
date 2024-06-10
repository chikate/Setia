<script setup lang="ts">
import type { UserCollection } from '@/interfaces'
</script>

<template>
  <main>
    <div
      class="flex flex-column gap-4 py-4 overflow-y-auto"
      style="max-height: 90vh"
      @vue:beforeMount="useUserCollectionCRUDStore().get()"
    >
      <PostComponent
        v-for="(postData, i) in useUserCollectionCRUDStore()
          .allLoadedItems?.filter(
            (post: UserCollection) => post.author == useAuthStore().userData?.username
          )
          .map((elem: UserCollection) => elem.postData)"
        :key="i"
        :postData
      />
    </div>
  </main>
</template>
