<template>
  <div class="flex-column">
    <div>asd</div>
  </div>
</template>

<script setup lang="ts">
import * as d3 from "d3";
console.log(d3.version); // Check if D3 is loaded
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

const projects = defineModel("projects", {
  type: Array<IProject>,
  default: [{ name: "asd", tasks: [{ name: "asd" }, { name: "asd" }] }],
});

onBeforeMount(init);
async function init() {}
</script>
