<template>
  <div class="chat-layout flex h-[calc(100vh-4rem)] overflow-hidden bg-surface-50 dark:bg-surface-950">
    <!-- Sidebar -->
    <div class="sidebar w-72 bg-surface-100 dark:bg-surface-900 flex flex-col border-r border-surface-200 dark:border-surface-700">
        <!-- Search -->
        <div class="p-4 shadow-sm z-10">
            <span class="p-input-icon-left w-full">
                <i class="pi pi-search" />
                <InputText placeholder="Find or start a conversation" class="w-full p-inputtext-sm" />
            </span>
        </div>

        <!-- Conversation List -->
        <div class="flex-1 overflow-y-auto px-2 py-2 custom-scrollbar">
            <!-- Groups Section -->
            <div class="mb-4">
                <div class="flex items-center justify-between px-3 mb-2 group">
                    <span class="text-xs font-bold text-surface-500 uppercase tracking-wider hover:text-surface-700 dark:hover:text-surface-300 transition-colors">Groups</span>
                    <i class="pi pi-plus cursor-pointer text-surface-500 hover:text-primary transition-colors" @click="showCreateGroup = true" v-tooltip="'Create Group'"></i>
                </div>
                <div class="flex flex-col gap-1">
                    <div 
                        v-for="group in store.groups" 
                        :key="group.id"
                        class="flex items-center gap-3 p-2 mx-1 rounded-md cursor-pointer transition-all duration-200"
                        :class="store.activeChat?.id === group.id ? 'bg-surface-200 dark:bg-surface-700 text-surface-900 dark:text-surface-0' : 'text-surface-600 dark:text-surface-400 hover:bg-surface-200 dark:hover:bg-surface-800 hover:text-surface-900 dark:hover:text-surface-200'"
                        @click="store.loadChatHistory(group.id!, 'group')"
                    >
                        <Avatar :label="group.name[0]" shape="circle" class="bg-surface-300 dark:bg-surface-700 text-surface-700 dark:text-surface-200" />
                        <span class="truncate font-medium">{{ group.name }}</span>
                    </div>
                </div>
            </div>

            <!-- DMs Section -->
            <div>
                <div class="flex items-center justify-between px-3 mb-2">
                    <span class="text-xs font-bold text-surface-500 uppercase tracking-wider">Direct Messages</span>
                </div>
                <div class="flex flex-col gap-1">
                    <div 
                        v-for="friend in store.friends" 
                        :key="friend.id"
                        class="flex items-center gap-3 p-2 mx-1 rounded-md cursor-pointer transition-all duration-200"
                        :class="store.activeChat?.id === friend.id ? 'bg-surface-200 dark:bg-surface-700 text-surface-900 dark:text-surface-0' : 'text-surface-600 dark:text-surface-400 hover:bg-surface-200 dark:hover:bg-surface-800 hover:text-surface-900 dark:hover:text-surface-200'"
                        @click="store.loadChatHistory(friend.id!, 'user')"
                    >
                        <Avatar :image="friend.avatar" :label="friend.name[0]" shape="circle" />
                        <div class="flex flex-col overflow-hidden">
                            <span class="truncate font-medium">{{ friend.name }}</span>
                            <span class="text-xs opacity-70 truncate">Online</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
        <!-- User Profile (Bottom) -->
        <div class="p-3 bg-surface-200 dark:bg-surface-850 flex items-center gap-3 border-t border-surface-300 dark:border-surface-700">
            <Avatar :image="me.avatar" :label="me.name[0]" shape="circle" class="cursor-pointer hover:opacity-80 transition-opacity" />
            <div class="flex flex-col overflow-hidden">
                <span class="text-sm font-bold truncate">{{ me.name }}</span>
                <span class="text-xs text-surface-500 truncate">#{{ me.id?.substring(0, 4) }}</span>
            </div>
            <div class="ml-auto flex gap-1">
                 <Button icon="pi pi-microphone" text rounded size="small" aria-label="Mute" />
                 <Button icon="pi pi-cog" text rounded size="small" aria-label="Settings" />
            </div>
        </div>
    </div>

    <!-- Main Chat Area -->
    <div class="flex-1 flex flex-col min-w-0 bg-surface-0 dark:bg-surface-900 relative">
        <slot name="chat-window" :toggleRightPanel="() => showRightPanel = !showRightPanel"></slot>
    </div>

    <!-- Right Panel (Members/Info) -->
    <div v-if="store.activeChat?.type === 'group' && showRightPanel" class="w-64 bg-surface-100 dark:bg-surface-900 border-l border-surface-200 dark:border-surface-700 hidden lg:flex flex-col transition-all duration-300">
        <div class="p-4 border-b border-surface-200 dark:border-surface-700 font-bold flex justify-between items-center">
            <span>Members</span>
            <span class="text-xs bg-surface-200 dark:bg-surface-800 px-2 py-0.5 rounded-full">3</span>
        </div>
        <div class="p-2 overflow-y-auto flex-1 custom-scrollbar">
            <!-- Mock members for now, need endpoint -->
            <div class="text-center text-surface-500 text-sm mt-4 italic">
                Member list coming soon
            </div>
            <div class="mt-4 px-2">
                 <Button label="Add Member" size="small" outlined class="w-full" @click="showAddMember = true" />
            </div>
        </div>
    </div>
    
    <!-- Dialogs -->
    <Dialog v-model:visible="showCreateGroup" header="Create Group" modal class="w-96">
        <div class="flex flex-col gap-4">
            <span class="p-float-label mt-4">
                <InputText id="group-name" v-model="newGroupName" class="w-full" autofocus />
                <label for="group-name">Group Name</label>
            </span>
            <div class="flex justify-end gap-2">
                <Button label="Cancel" text @click="showCreateGroup = false" />
                <Button label="Create" @click="createGroup" :loading="creating" />
            </div>
        </div>
    </Dialog>
    
    <Dialog v-model:visible="showAddMember" header="Add Member" modal class="w-96">
        <div class="flex flex-col gap-4">
            <span class="p-float-label mt-4">
                <InputText id="member-id" v-model="newMemberId" class="w-full" autofocus />
                <label for="member-id">User ID</label>
            </span>
            <div class="flex justify-end gap-2">
                <Button label="Cancel" text @click="showAddMember = false" />
                <Button label="Add" @click="addMember" />
            </div>
        </div>
    </Dialog>
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue';
import Avatar from 'primevue/avatar';
import Button from 'primevue/button';
import InputText from 'primevue/inputtext';
import Dialog from 'primevue/dialog';

const store = useChatStore();
const showCreateGroup = ref(false);
const showAddMember = ref(false);
const showRightPanel = ref(true);
const newGroupName = ref('');
const newMemberId = ref('');
const creating = ref(false);

const me = computed(() => {
    const userCookie = getCookie("user");
    return userCookie ? JSON.parse(userCookie) : { name: 'Me', avatar: null };
});

const createGroup = async () => {
    if (!newGroupName.value) return;
    creating.value = true;
    try {
        await store.createGroup(newGroupName.value);
        showCreateGroup.value = false;
        newGroupName.value = '';
    } finally {
        creating.value = false;
    }
};

const addMember = async () => {
    // TODO: Implement add member action in store
    showAddMember.value = false;
};
</script>
