<template>
  <div class="friends-list p-4 bg-surface-0 dark:bg-surface-900 rounded-lg shadow">
    <h3 class="text-xl font-bold mb-4">Friends</h3>
    
    
    <div v-if="store.requests.length > 0" class="mb-6">
      <h4 class="text-sm font-semibold text-surface-500 mb-2">Requests</h4>
      <div v-for="req in store.requests" :key="req.requestId" class="flex items-center justify-between p-2 hover:bg-surface-100 dark:hover:bg-surface-800 rounded">
        <div class="flex items-center gap-2">
            <Avatar :image="req.sender.avatar" :label="req.sender.name[0]" shape="circle" />
            <span>{{ req.sender.name }}</span>
        </div>
        <div class="flex gap-1">
          <Button icon="pi pi-check" text rounded severity="success" @click="store.acceptRequest(req.requestId)" />
          <Button icon="pi pi-times" text rounded severity="danger" @click="store.rejectRequest(req.requestId)" />
        </div>
      </div>
    </div>


    <div class="flex flex-col gap-2">
      <div 
        v-for="friend in store.friends" 
        :key="friend.id" 
        class="flex items-center gap-3 p-2 cursor-pointer hover:bg-surface-100 dark:hover:bg-surface-800 rounded transition-colors"
        @click="$emit('select', friend)"
      >
        <Avatar :image="friend.avatar" :label="friend.name[0]" shape="circle" size="large" />
        <div>
            <div class="font-semibold">{{ friend.name }}</div>
            <div class="text-xs text-surface-500">Online</div>
        </div>
      </div>
    </div>
    
    
    <div class="mt-4 pt-4 border-t border-surface-200 dark:border-surface-700">
        <div class="p-inputgroup">
            <InputText v-model="newFriendId" placeholder="User ID to add" />
            <Button icon="pi pi-plus" @click="addFriend" :disabled="!newFriendId" />
        </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import Avatar from 'primevue/avatar';
import Button from 'primevue/button';
import InputText from 'primevue/inputtext';

const store = useChatStore();
const newFriendId = ref('');

const addFriend = async () => {
    if (!newFriendId.value) return;
    try {
        await store.sendRequest(newFriendId.value);
        newFriendId.value = '';
        // Show toast success
    } catch (e) {
        // Show toast error
    }
};

onMounted(() => {
    store.initialize();
});
</script>
