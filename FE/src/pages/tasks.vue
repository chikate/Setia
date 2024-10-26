<template>
  <div class="flex-column gap-3">
    <InputText placeholder="Search after name, path, regestry number" @keyup.enter="searchFile" />
    <div class="flex-wrap gap-3">
      <div v-for="(project, index) in projects" :key="index" class="cursor-pointer p-2">
        {{ project }}
        <div v-for="(task, index) in project.tasks" :key="index" class="cursor-pointer p-2">
          {{ task }}
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
export interface IProject {
  id?: string
  name: string
  description: string
  tasks: ITask[]
}
export interface ITask {
  id?: string
  name: string
  description: string
  assigns: string[]
  status: number
}

const projects = defineModel('projects', { type: Array<IProject>, default: [] })

onBeforeMount(init)
async function init() {}
</script>
