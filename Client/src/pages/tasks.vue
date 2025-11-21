<template>
  <div class="kanban flex gap-1 p-1 ">
    <div v-for="col in columns" :key="col.key" class="kanban-column p-3 bg-gray-700 border-round shadow-md w-2"
      @dragover.prevent @drop="onDrop(col.key)">
      <h2 class="font-bold text-lg mb-3">{{ col.label }}</h2>
      <div v-for="task in getTasks(col.key)" :key="task.id"
        class="kanban-card p-3 mb-3 rounded-lg shadow-sm bg-white cursor-pointer" draggable
        @dragstart="onDragStart(task)">
        <div class="font-semibold">{{ task.name }}</div>
        <div class="text-sm text-gray-600">{{ task.description }}</div>
        <div class="flex mt-2">
          <img v-for="(a, i) in task.assigns || []" :key="i" :src="a" class="w-6 h-6 rounded-full border" />
        </div>
      </div>
    </div>
    <EditorContent :editor="editor" class="editor custom-shadow-1" @keydown.enter="handleAddTask" />
    <InputText placeholder="Add task" @keydown.enter="handleAddTask" class="w-full" />
  </div>
</template>

<script setup lang="ts">
import { Editor, EditorContent } from "@tiptap/vue-3";
import StarterKit from "@tiptap/starter-kit";
import TextAlign from "@tiptap/extension-text-align";
import { useToast } from "primevue/usetoast";
import InputText from "primevue/inputtext";

defineOptions({ name: "Tasks", icon: "✅" });

const toast = useToast();
const editor = new Editor({ extensions: [StarterKit, TextAlign.configure({ types: ["heading", "paragraph"] })] });
const columns = [
  { key: "todo", label: "To Do" },
  { key: "inprogress", label: "In Progress" },
  { key: "testing", label: "For Testing" },
  { key: "functional", label: "Functional" },
  { key: "prod", label: "In Production" },
  { key: "done", label: "Done" }
];

interface ITask { id?: string; name: string; description?: string; assigns?: string[]; status?: number }
interface IProject { id?: string; name?: string; description?: string; tasks: ITask[] }
const projects = ref<IProject[]>([{ name: "asd", tasks: [] }]);
let draggedTask = ref<ITask | null>(null);

const onDragStart = (task: ITask) => draggedTask.value = task;
const onDrop = (colKey: string) => { if (draggedTask.value) { draggedTask.value.status = columns.findIndex(c => c.key === colKey); draggedTask.value = null } }
const getTasks = (colKey: string) => projects.value[0].tasks.filter(t => t.status === columns.findIndex(c => c.key === colKey));

const handleAddTask = (e: KeyboardEvent) => {
  const target = e.target as HTMLInputElement;
  const val = target.value.trim();
  if (!val) { toast.add({ severity: "error", summary: "Invalid task!" }); return }
  projects.value[0].tasks.push({ id: crypto.randomUUID(), name: val, description: val, status: 0, assigns: ["https://primefaces.org/cdn/primevue/images/avatar/asiyajavayant.png"] });
  toast.add({ severity: "success", summary: "Task added!" });
  target.value = "";
}
</script>

<style scoped>
.kanban {
  width: 100%
}

.kanban-column {
  min-height: 70vh
}

.kanban-card {
  transition: .2s
}

.kanban-card:hover {
  transform: scale(1.02);
  box-shadow: 0 4px 10px rgba(0, 0, 0, .15)
}

.editor {
  border-radius: 5px;
  aspect-ratio: 1/1.414;
  padding: 1rem;
  cursor: text;
  background: white
}
</style>