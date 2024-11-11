<script setup lang="ts">
import type { Post } from '@/global/interfaces'
</script>

<template>
  <div
    id="PostsWrapper"
    class="flex flex-column gap-4 py-4"
    style="max-height: 90vh"
    @vue:beforeMount="usePostsCRUDStore().get()"
  >
    <PostComponent
      v-for="(postData, i) in usePostsCRUDStore().allLoadedItems?.filter(
        (elem: Post) => elem.tags.indexOf('News') > -1
      )"
      :key="i"
      :post-data="postData"
    />
  </div>
</template>
