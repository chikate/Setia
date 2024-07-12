<script setup lang="ts">
import type { Post } from '@/interfaces'
</script>

<template>
  <main>
    <div
      id="PostsWrapper"
      class="flex flex-column gap-4 py-4 overflow-y-auto"
      style="max-height: 90vh"
      @vue:beforeMount="usePostsCRUDStore().get()"
    >
      <PostComponent
        v-for="(postData, i) in usePostsCRUDStore().allLoadedItems?.filter(
          (elem: Post) => elem.tags?.indexOf('News') > -1
        )"
        :key="i"
        :post-data="postData"
      />
    </div>
  </main>
</template>
