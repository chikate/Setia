<template>
  <div class="chat-window flex flex-col h-full bg-surface-0 dark:bg-surface-900 rounded-lg shadow overflow-hidden">
    <div v-if="user" class="header p-4 border-b border-surface-200 dark:border-surface-700 flex items-center gap-3">
        <Avatar :image="user.avatar" :label="user.name[0]" shape="circle" />
        <span class="font-bold text-lg">{{ user.name }}</span>
    </div>
    
    <div v-if="user" class="messages flex-1 overflow-y-auto p-4 flex flex-col gap-3">
        <div 
            v-for="msg in messages" 
            :key="msg.id" 
            class="max-w-[70%] p-3 rounded-lg"
            :class="msg.authorId === myId ? 'self-end bg-primary text-primary-contrast' : 'self-start bg-surface-100 dark:bg-surface-800'"
        >
            {{ msg.message }}
        </div>
    </div>
    
    <div v-else class="flex-1 flex items-center justify-center text-surface-500">
        Select a friend to start chatting
    </div>

    <div v-if="user" class="input-area p-4 border-t border-surface-200 dark:border-surface-700 flex gap-2">
        <InputText v-model="messageText" class="flex-1" placeholder="Type a message..." @keydown.enter="send" />
        <Button icon="pi pi-send" @click="send" :disabled="!messageText.trim()" />
    </div>
  </div>
</template>

<script setup lang="ts">
import type { UserModel } from '@/composables/models';
import Avatar from 'primevue/avatar';
import Button from 'primevue/button';
import InputText from 'primevue/inputtext';

const props = defineProps<{
    user?: UserModel
}>();

const store = useChatStore();
const messageText = ref('');

const myId = computed(() => {
    const userCookie = getCookie("user");
    return userCookie ? JSON.parse(userCookie).id : null;
});
const messages = computed(() => props.user ? store.messages[props.user.id!] || [] : []);

watch(() => props.user, async (newUser) => {
    if (newUser?.id) {
        await store.loadChatHistory(newUser.id);
    }
});

const send = async () => {
    if (!messageText.value.trim() || !props.user?.id) return;
    
    await store.sendMessage(props.user.id, messageText.value);
    messageText.value = '';
};
</script>
