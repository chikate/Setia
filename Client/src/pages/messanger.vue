<template>
  <ChatLayout>
    <template #chat-window="{ toggleRightPanel }">
        <div v-if="store.activeChat" class="flex flex-col h-full">
            <!-- Header -->
            <div class="px-4 py-3 border-b border-surface-200 dark:border-surface-700 flex items-center justify-between shadow-sm z-10 bg-surface-0 dark:bg-surface-900">
                <div class="flex items-center gap-3">
                    <Avatar :image="activeChatInfo.avatar" :label="activeChatInfo.name[0]" shape="circle" class="bg-primary text-primary-contrast" />
                    <div>
                        <div class="font-bold text-lg leading-tight">{{ activeChatInfo.name }}</div>
                        <div v-if="store.activeChat.type === 'group'" class="text-xs text-surface-500">Group Chat</div>
                        <div v-else class="text-xs text-green-500 flex items-center gap-1"><span class="w-2 h-2 rounded-full bg-green-500 inline-block"></span> Online</div>
                    </div>
                </div>
                <div class="flex items-center gap-2">
                    <Button icon="pi pi-phone" text rounded aria-label="Call" />
                    <Button icon="pi pi-video" text rounded aria-label="Video Call" />
                    <Button icon="pi pi-info-circle" text rounded aria-label="Info" @click="toggleRightPanel" />
                </div>
            </div>

            <!-- Messages -->
            <div class="flex-1 overflow-y-auto p-4 flex flex-col gap-2 custom-scrollbar" ref="messagesContainer">
                <MessageItem 
                    v-for="msg in messages" 
                    :key="msg.id" 
                    :message="msg" 
                    @reply="handleReply"
                />
            </div>

            <!-- Input -->
            <div class="p-4 bg-surface-0 dark:bg-surface-900">
                <div v-if="replyingTo" class="flex items-center justify-between bg-surface-100 dark:bg-surface-800 p-2 rounded-t-lg text-sm border-l-4 border-primary mb-1">
                    <div class="flex items-center gap-2">
                        <i class="pi pi-reply text-surface-500"></i>
                        <span>Replying to <span class="font-bold">{{ replyingTo.authorData?.name }}</span></span>
                    </div>
                    <Button icon="pi pi-times" text rounded size="small" @click="replyingTo = null" />
                </div>
                <MessageInput @send="sendMessage" class="shadow-sm border border-surface-200 dark:border-surface-700 focus-within:border-primary transition-colors" />
            </div>
        </div>
        <div v-else class="flex-1 flex items-center justify-center text-surface-500 flex-col gap-4">
            <i class="pi pi-comments text-6xl text-surface-300"></i>
            <span class="text-xl">Select a conversation to start chatting</span>
        </div>
    </template>
  </ChatLayout>
</template>

<script setup lang="ts">
defineOptions({
  name: "Messanger",
  icon: "💬",
});

import type { MessageModel } from '@/composables/models';

const store = useChatStore();
const messagesContainer = ref<HTMLElement | null>(null);
const replyingTo = ref<MessageModel | null>(null);

const activeChatInfo = computed(() => {
    if (!store.activeChat) return { name: '', avatar: null };
    if (store.activeChat.type === 'user') {
        const friend = store.friends.find(f => f.id === store.activeChat!.id);
        return friend || { name: 'Unknown User', avatar: null };
    } else {
        const group = store.groups.find(g => g.id === store.activeChat!.id);
        return group || { name: 'Unknown Group', avatar: null };
    }
});

const messages = computed(() => {
    if (!store.activeChat) return [];
    return store.messages[store.activeChat.id] || [];
});

const sendMessage = async (content: string) => {
    await store.sendMessage(content, replyingTo.value?.id);
    replyingTo.value = null;
    scrollToBottom();
};

const handleReply = (msg: MessageModel) => {
    replyingTo.value = msg;
};

const scrollToBottom = () => {
    nextTick(() => {
        if (messagesContainer.value) {
            messagesContainer.value.scrollTop = messagesContainer.value.scrollHeight;
        }
    });
};

watch(() => messages.value.length, scrollToBottom);
</script>
