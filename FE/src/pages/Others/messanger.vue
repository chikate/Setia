<script setup lang="ts">
import type { Post } from '@/global/interfaces'

const posts = ref<Post[]>([])
const message = ref('')
</script>

<template>
  <!-- <div class="flex flex-column w-full bg-primary gap-2 p-3 w-1">
      <InputText
        v-model="userToSendRequestTo"
        @keydown.enter="
          helperStore()
            .sendFriendRequest(userToSendRequestTo)
            .then(() => (userToSendRequestTo = ''))
        "
      />
      <UserProfileComponent v-for="(username, i) in ['testUser', 'Dragos']" :key="i" :username />
    </div> -->
  <div class="flex flex-column h-full w-full px-8 p-4">
    <div class="flex-grow-1 flex h-full flex-column-reverse justify-content-end">
      <PostComponent chat v-for="(postData, i) in posts" :key="i" :post-data="postData" />
    </div>
    <InputText
      v-model="message"
      @keydown.enter="usePostsCRUDStore().add(), (message = '')"
      placeholder="Message"
      class="p-3 px-4"
    />
  </div>
</template>
