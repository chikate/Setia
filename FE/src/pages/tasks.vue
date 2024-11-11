<template>
  <div class="flex-column align-items-start gap-3">
    <InputText placeholder="Add task" @keydown.enter="projects.push()" />
    <div class="card flex-column gap-3">
      <div v-for="(project, index) in projects" :key="index" class="cursor-pointer p-2">
        {{ project.name }}
        <div v-for="(task, index) in project.tasks" :key="index" class="cursor-pointer p-2">
          {{ task.description }}
          {{ task.assigns }}
          <Button label="asd" />
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
export interface IProject {
  id?: string
  name?: string
  description?: string
  tasks: ITask[]
}
export interface ITask {
  id?: string
  name: string
  description?: string
  assigns?: string[]
  status?: number
}

const projects = defineModel('projects', {
  type: Array<IProject>,
  default: [{ name: 'asd', tasks: [{ name: 'asd' }, { name: 'asd' }] }]
})

onBeforeMount(init)
async function init() {}
</script>
