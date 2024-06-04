<template>
  <div
    v-if="() => useAuthStore().checkUserRights"
    class="flex flex-row w-full h-full flex-grow-1 gap-4 p-4 pt-8"
  >
    <div class="flex flex-column w-full bg-primary p-3 w-3">dsa</div>
    <div class="flex flex-column w-full px-8 p-4">
      <div class="flex-grow-1 flex flex-column justify-content-end h-8 p-2 px-4">
        <div v-for="message in messages" :key="message">{{ message }}</div>
      </div>
      <InputText
        v-model="message"
        @keydown.enter="messages.push(message), (message = '')"
        placeholder="Message"
        class="p-3 px-4"
      />
    </div>
  </div>
</template>

<script setup>
const messages = ref([''])
const message = ref('')
</script>
