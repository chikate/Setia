<script setup lang="ts">
definePage({
  meta: {
    title: "Chat",
    description: "Chat with your friends and family",
  },
});

const messages = ref([]);

onBeforeMount(init);
async function init() {}
</script>

<template>
  <div class="flex-row gap-3 p-3 h-full">
    <div id="friendsList" class="flex-column">
      <div
        id="friends"
        class="flex-column"
        v-for="friend in []"
        :key="friend.id"
      >
        <Avatar />
        <div>{{ friend.name }}</div>
      </div>
    </div>
    <div
      id="directMessage"
      class="flex-column gap-2 w-full justify-content-center"
    >
      <div
        id="messages"
        class="flex-row w-full align-items-center gap-1"
        v-for="message in messages"
        :key="message.id"
      >
        <Avatar />
        <div>{{ message.author }}</div>
        <label>{{ message.message }}</label>
      </div>
      <div class="flex-column gap-2">
        <Textarea rows="3" class="h-full w-full" />
        <div class="flex-row gap-2 align-items-end justify-content-end">
          <Button
            icon="pi pi-send"
            label="SEND"
            @click="messages.push({ sender: 'me', message: 'asd' })"
          />
        </div>
      </div>
    </div>
  </div>
</template>
