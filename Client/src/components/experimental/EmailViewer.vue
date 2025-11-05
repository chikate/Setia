<template>
  <div class="grid">
    <!-- Sidebar -->
    <div class="col-3">
      <div class="">
        <h4>Inbox</h4>
        <ul class="list-none p-0 m-0">
          <li
            v-for="email in emails"
            :key="email.id"
            @click="selectEmail(email)"
            class="p-3 border-bottom-1 surface-border cursor-pointer hover:bg-primary-100"
            :class="{ 'bg-primary-50': selectedEmail?.id == email.id }"
          >
            <div class="font-bold">{{ email.sender }}</div>
            <div class="text-sm text-color-secondary">{{ email.subject }}</div>
          </li>
        </ul>
      </div>
    </div>

    <!-- Email Content -->
    <div class="col-9">
      <div class="" v-if="selectedEmail">
        <div class="mb-2">
          <h3>{{ selectedEmail.subject }}</h3>
          <p class="text-sm text-color-secondary">
            From: {{ selectedEmail.sender }}
          </p>
        </div>
        <p>{{ selectedEmail.body }}</p>
      </div>
      <div v-else class="text-center text-color-secondary">
        <p>Select an email to view details</p>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";

const emails = ref([
  {
    id: 1,
    sender: "alice@example.com",
    subject: "Meeting Reminder",
    body: "Just a reminder for the meeting scheduled tomorrow at 10 AM.",
  },
  {
    id: 2,
    sender: "bob@example.com",
    subject: "Weekly Report",
    body: "Here’s the weekly report. Let me know if you need changes.",
  },
  {
    id: 3,
    sender: "carol@example.com",
    subject: "Party Invitation",
    body: "You’re invited to a party this weekend. Hope you can come!",
  },
]);

const selectedEmail = ref(null);

function selectEmail(email) {
  selectedEmail.value = email;
}
</script>

<style scoped>
.cursor-pointer {
  cursor: pointer;
}
</style>
