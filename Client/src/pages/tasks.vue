<template>
  <div class="flex-column align-items-start gap-3 p-3">
    <InputText placeholder="Add task" @keydown.enter="handleAddTask" />
    <div class="flex-row">
      <div class="card flex-column gap-3 h-full overflow-auto">
        <div
          v-for="(project, index) in projects"
          :key="index"
          class="cursor-pointer flex-column gap-1 text-blue-500 h-full overflow-auto"
        >
          {{ project.name }}
          <div class="h-full overflow-auto">
            <div
              v-for="(task, index) in project.tasks"
              :key="index"
              class="flex-wrap gap-2 px-2 align-items-center hover:bg-gray-50 hover:border-blue-500 border-gray-50 justify-content-between cursor-pointer border-1 border-round"
              style="min-width: 20rem"
              @click="taskIdInFocus = task.id"
              @dblclick="$emit('taskDblClick', $event)"
              :draggable="true"
            >
              {{ task.description }}
              <div class="flex-row">
                <Button text label="Pick" />
                <AvatarGroup>
                  <Avatar
                    v-for="(assignee, assignee_index) in task.assigns"
                    :key="assignee_index"
                    :image="assignee"
                    shape="circle"
                    class="border-2"
                    :style="{
                      'border-color': stringToColor(assignee),
                    }"
                  />
                </AvatarGroup>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div>
        {{ taskIdInFocus }}
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ToastMessageOptions } from "primevue/toast";
import { useToast } from "primevue/usetoast";
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
