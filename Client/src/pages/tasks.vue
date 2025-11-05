<template>
  <div class="flex">
    <div class="flex flex-column align-items-start">
      <InputText placeholder="Add task" @keydown.enter="handleAddTask" />
      <DataTable v-for="value in projects" :value="value.tasks">
        <Column field="title" header="Title" />
        <Column field="code" header="Code" />
        <Column field="assignee" header="Assaignee" />
      </DataTable>
    </div>

    <div class="flex flex-column align-items-start">
      <EditorContent :editor class="editor custom-shadow-1" />
    </div>

    <div class="flex flex-column align-items-start">
      1
      <div class="flex flex-row">t</div>
      2
    </div>
  </div>
</template>

<script setup lang="ts">
import { Editor, EditorContent } from "@tiptap/vue-3";
import StarterKit from "@tiptap/starter-kit";
import TextAlign from "@tiptap/extension-text-align";
import { ToastMessageOptions } from "primevue/toast";
import { useToast } from "primevue/usetoast";

defineOptions({
  name: "Tasks",
  icon: "✅",
});

const content = ref();
const editor = new Editor({
  extensions: [
    StarterKit,
    TextAlign.configure({
      types: ["heading", "paragraph"],
    }),
  ],
  content: content.value,
});

const toast = useToast();

export interface IProject {
  id?: string;
  name?: string;
  description?: string;
  tasks: ITask[];
}
export interface ITask {
  id?: string;
  name: string;
  description?: string;
  assigns?: string[];
  status?: number;
}

const projects = ref<IProject[]>([{ name: "asd", tasks: [{ title: "1" }] }]);
const projectIndexInFocus = ref<number>();
const taskIdInFocus = ref();

onBeforeMount(init);
async function init() {}

function handleAddTask(event: KeyboardEvent) {
  const target = event.target as HTMLInputElement;
  const inputValue = target.value.trim(); // trim to avoid whitespace-only values

  const toastMessage: ToastMessageOptions = {
    severity: "info",
    summary: "Adding parameter",
    detail: "", // ensure this is initialized
  };

  if (!inputValue) {
    toastMessage.severity = "error";
    toastMessage.detail = "Invalid task!";
  } else {
    projects.value[0].tasks.push({
      name: "inputValue",
      description: inputValue,
      assigns: [
        "https://primefaces.org/cdn/primevue/images/avatar/asiyajavayant.png",
      ],
    });
    toastMessage.severity = "success";
    toastMessage.detail = "Task added successfully!";
  }

  toast.add(toastMessage);
  target.value = "";
}
</script>

<style scoped>
.editor {
  border-radius: 5px;
  aspect-ratio: 1/1.414;
  padding: 1rem;
  cursor: text;
}
</style>
