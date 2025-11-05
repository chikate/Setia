<template>
  <div class="flex flex-column gap-2 overflow-auto h-full">
    <InputText />
    <AnuntCompoennt v-for="x in 10" />
    <div class="flex flex-wrap p-3">
      <InventorySlot v-for="item in items" :key="item.name" />
    </div>
  </div>
</template>

<script setup lang="ts">
import { ToastMessageOptions } from "primevue/toast";
import { useToast } from "primevue/usetoast";

defineOptions({
  name: "Anunturi",
  icon: "📰",
});

const items = ref([
  { name: "Test", value: 1 },
  { name: "Test", value: 1 },
  { name: "Test", value: 1 },
  { name: "Test", value: 1 },
]);

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

// const projects = defineModel("projects", {
//   type: Array<IProject>,
//   default: [{ name: "asd", tasks: [{ name: "asd" }, { name: "asd" }] }],
// });

const projects = ref<IProject[]>([{ name: "asd", tasks: [] }]);
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
