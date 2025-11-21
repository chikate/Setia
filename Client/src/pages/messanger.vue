<template>
  <div class="messenger-page h-[calc(100vh-4rem)] flex gap-4 p-4">
    <div class="w-1/4 min-w-[250px]">
        <FriendsList @select="selectFriend" />
    </div>
    <div class="flex-1">
        <ChatWindow :user="selectedFriend" />
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import FriendsList from '@/components/FriendsList.vue';
import ChatWindow from '@/components/ChatWindow.vue';
import type { UserModel } from '@/composables/models';

const selectedFriend = ref<UserModel | undefined>(undefined);

const selectFriend = (friend: UserModel) => {
    selectedFriend.value = friend;
};
</script>
