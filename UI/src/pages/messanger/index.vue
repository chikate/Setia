<script setup lang="ts">
import { Post } from '@/interfaces'

const posts = ref<Post[]>([])
const message = ref('')
</script>

<template>
  <div class="flex flex-row w-full h-full flex-grow-1 gap-4 p-4 pt-8">
    <div class="flex flex-column w-full bg-primary gap-2 p-3 w-1">
      <UserProfileComponent v-for="(username, i) in ['testUser', 'Dragos']" :key="i" :username />
    </div>
    <div class="flex flex-column w-full px-8 p-4">
      <div class="flex-grow-1 flex flex-column-reverse justify-content-end h-8 p-2 px-4">
        <PostComponent chat v-for="(postData, i) in posts" :key="i" :postData />
      </div>
      <InputText
        v-model="message"
        @keydown.enter="posts.push({ message } as Post), (message = ''), console.log(posts)"
        placeholder="Message"
        class="p-3 px-4"
      />
    </div>
  </div>
</template>
