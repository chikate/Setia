<template>
  <div
    :id="$options.__name"
    class="flex flex-column gap-2 border-round overflow-hidden align-items-center"
  >
    <!-- File Viewer -->
    <div v-if="postData.attachments">
      <FileViewer
        class="border-round overflow-hidden"
        :src="postData.attachments[0]"
      />
      <div class="flex flex-wrap" v-if="postData.attachments.length > 1">
        <div v-for="value in postData.attachments">
          {{ value }}
        </div>
      </div>
    </div>

    <div class="flex justify-content-end w-full">
      <Avatar :image="authorData.avatar" size="large" shape="circle" />
      <div class="flex-grow-1 flex flex-column justify-content-center px-2">
        <div class="text-xs opacity-40">
          {{ postData.executionDate ?? "00.00.0000" }}
        </div>
        {{ postData.author ?? "Name" }}
      </div>
      <div
        v-show="postData.message"
        v-for="tag in postData.tags"
        :key="tag"
        class="px-2 border-round shadow-1 font-semibold text-white"
        :style="{ backgroundColor: stringToColor(tag) }"
      >
        {{ tag }}
      </div>
      <Button
        type="button"
        icon="pi pi-ellipsis-v"
        @click="menuRef.toggle($event)"
        aria-haspopup="true"
        aria-controls="overlay_menu"
        text
      />
      <Menu ref="menuRef" popup :model="menuItems" />
    </div>

    <!-- Editor -->
    <EditorContent v-if="editing" class="editor w-full" :editor="editor" />

    <!-- Question -->
    <QuestionComponent
      v-if="postData.questionId && postData.questionData"
      v-model:question-data="postData.questionData"
      answerMode
    />

    <!-- Post Creation -->
    <!-- <SplitButton
      v-if="editing && !postData.message"
      class="shadow-1"
      :disabled="!localMessage"
      label="Post"
      :model="postSplitButton"
    /> -->

    <!-- Actions -->
    <div v-if="postData.message" class="flex flex-column w-full">
      <div
        class="p-1 px-2 bg-gray-700 border-round"
        v-html="postData.message"
      />

      <div class="flex flex-wrap justify-content-around">
        <CountButton
          v-for="(icon, i) in icons"
          :key="i"
          :icon="icon.name"
          @click="handleIconClick(i)"
        />
      </div>

      <!-- Emoji Picker -->
      <EmojiPicker
        v-if="showActions === 0 || showActions === 1"
        native
        @select="handleEmoji"
      />

      <!-- Comments -->
      <div v-if="showActions === 2" class="h-8">
        <PostComponent :postData="{ entityId: postData.id }" />
      </div>

      <!-- Share -->
      <div
        v-if="showActions === 3"
        class="flex flex-wrap justify-content-center"
      >
        <UserProfileComponent
          v-for="username in ['testUser', 'Dragos']"
          :key="username"
          :username="username"
          class="flex-grow-1"
          style="max-width: 10rem"
        />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { Editor, EditorContent } from "@tiptap/vue-3";
import StarterKit from "@tiptap/starter-kit";
import Underline from "@tiptap/extension-underline";
import TextAlign from "@tiptap/extension-text-align";
import Placeholder from "@tiptap/extension-placeholder";
import EmojiPicker from "vue3-emoji-picker";

const postData = defineModel("postData", {
  type: Object as PropType<any>,
  required: true,
});
const editing = defineModel("editing", { type: Boolean, default: false });

const showActions = ref<number>();
const localMessage = ref(postData.value.message);
const menuRef = ref();
const authorData = ref({
  avatar:
    "https://sm.ign.com/t/ign_pk/cover/a/avatar-gen/avatar-generations_rpge.600.jpg",
});

// Editor
const editor = new Editor({
  extensions: [
    StarterKit,
    Underline,
    TextAlign.configure({ types: ["heading", "paragraph"] }),
    Placeholder.configure({ placeholder: "Type something..." }),
  ],
  content: postData.value.message,
});

// Computed
const attachmentSrc = computed(() => postData.value.attachments?.[0] ?? "");
const menuItems = computed(() => [
  {
    label: "Options",
    items: [
      { label: "Delete", icon: "pi pi-trash" },
      {
        label: "Edit",
        icon: "pi pi-pencil",
        command: () => (editing.value = !editing.value),
      },
      { label: "Export", icon: "pi pi-upload", command: () => {} },
      {
        label: postData.value.tags?.includes("Private")
          ? "Make it Public"
          : "Make it Private",
        icon: postData.value.tags?.includes("Private")
          ? "pi pi-eye"
          : "pi pi-eye-slash",
        command: () => {},
      },
    ],
  },
]);

const icons = ref([
  { name: "pi-thumbs-up" },
  { name: "pi-thumbs-down" },
  { name: "pi-comments" },
  { name: "pi-star" },
]);

// SplitButton for posting
const postSplitButton = [
  { label: "News" },
  { label: "Add Question", command: toggleQuestion },
];

// Methods
function toggleQuestion() {
  postData.value.questionData
    ? (postData.value.questionData = undefined)
    : addLocalQuestion();
}

function addLocalQuestion() {
  postData.value.questionData = {};
}

function handleIconClick(index: number) {
  showActions.value = showActions.value === index ? undefined : index;
}

function handleEmoji(event: any) {
  // Example: Add emoji reaction
  console.log(event);
}

function stringToColor(str: string) {
  let hash = 0;
  for (let i = 0; i < str.length; i++)
    hash = str.charCodeAt(i) + ((hash << 5) - hash);
  let color = "#";
  for (let i = 0; i < 3; i++)
    color += ((hash >> (i * 8)) & 0xff).toString(16).padStart(2, "0");
  return color;
}
</script>

<style scoped>
.editor {
  background-color: transparent;
  border-radius: 5px;
  padding: 0 0.7rem;
  line-height: 0.5rem;
  cursor: text;
  background-color: white;
  color: black;
}
</style>
