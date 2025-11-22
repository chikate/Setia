<template>
  <div
    class="message-item group flex gap-3 p-2 hover:bg-surface-100 dark:hover:bg-surface-800 rounded transition-colors relative"
    :class="{ 'bg-primary-50 dark:bg-primary-900/10': isMentioned }"
  >
    <Avatar
      :image="message.authorData?.avatar"
      :label="message.authorData?.name?.[0]"
      shape="circle"
      class="mt-1"
    />

    <div class="flex-1 min-w-0">
      <div class="flex items-baseline gap-2">
        <span class="font-bold text-sm">{{
          message.authorData?.name || "Unknown"
        }}</span>
        <span class="text-xs text-surface-500">{{
          formatDate(message.executionDate)
        }}</span>
        <span
          v-if="message.isEdited"
          class="text-xs text-surface-400 cursor-help"
          title="Edited"
          >(edited)</span
        >
      </div>

      <div
        v-if="!isEditing"
        class="prose dark:prose-invert max-w-none text-sm"
        v-html="message.message"
      ></div>

      <div v-else class="mt-1">
        <MessageInput :placeholder="message.message" @send="saveEdit" />
        <div class="text-xs mt-1">
          <span
            class="text-primary cursor-pointer hover:underline"
            @click="isEditing = false"
            >Cancel</span
          >
        </div>
      </div>

      <div v-if="message.reactions" class="flex gap-1 mt-1"></div>
    </div>

    <div
      class="absolute right-2 top-2 hidden group-hover:flex bg-surface-0 dark:bg-surface-900 shadow-md rounded border border-surface-200 dark:border-surface-700"
    >
      <Button
        icon="pi pi-face-smile"
        text
        rounded
        size="small"
        v-tooltip.top="'Add Reaction'"
      />
      <Button
        icon="pi pi-reply"
        text
        rounded
        size="small"
        v-tooltip.top="'Reply'"
        @click="$emit('reply', message)"
      />
      <Button
        v-if="isMe"
        icon="pi pi-pencil"
        text
        rounded
        size="small"
        v-tooltip.top="'Edit'"
        @click="isEditing = true"
      />
      <Button
        v-if="isMe"
        icon="pi pi-trash"
        text
        rounded
        size="small"
        severity="danger"
        v-tooltip.top="'Delete'"
      />
    </div>
  </div>
</template>

<script setup lang="ts">
import type { MessageModel } from "@/composables/models";
import MessageInput from "./MessageInput.vue";

const props = defineProps<{
  message: MessageModel;
}>();

const emit = defineEmits(["reply"]);
const store = useChatStore();

const isEditing = ref(false);

const isMe = computed(() => {
  const userCookie = getCookie("user");
  return userCookie && JSON.parse(userCookie).id === props.message.authorId;
});

const isMentioned = computed(() => {
  return false;
});

const formatDate = (date?: Date | string | null) => {
  if (!date) return "";
  return new Date(date).toLocaleString();
};

const saveEdit = async (newContent: string) => {
  isEditing.value = false;
};
</script>
