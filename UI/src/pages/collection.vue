<script setup lang="ts">
import type { UserCollection } from '@/interfaces'

const t = computed(() =>
  useUserCollectionCRUDStore().allLoadedItems?.filter(
    (post: UserCollection) => post.author === useAuthStore().userData?.username
  )
)
</script>

<template>
  <main>
    <div
      class="flex flex-column gap-4 py-4 overflow-y-auto"
      style="max-height: 90vh"
      @vue:beforeMount="useUserCollectionCRUDStore().get()"
    >
      <div v-if="(t?.length ?? 0) > 0">
        <PostComponent
          v-for="(postData, i) in t?.map((elem: UserCollection) => elem.postData)"
          :key="i"
          :post-data="postData"
        />
      </div>
      <div v-else>Nothing in collections</div>
    </div>
  </main>
</template>
