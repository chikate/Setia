<template>
  <div class="flex-column align-items-start gap-2">
    <input type="file" id="fileInput" webkitdirectory />
    <div class="bg-gray-200 border-round px-3" v-for="node in diagramData" :key="node.name">
      {{ node.name }}
      <!-- <div v-if="node.links.length > 0">{{ node.links }}</div> -->
    </div>
  </div>
</template>

<script setup lang="ts">
const diagramData = ref<any[]>([])
onMounted(() => {
  document.getElementById('fileInput')?.addEventListener('change', async (event: any) => {
    const files = event.target.files

    for (let i = 0; i < files.length; i++) {
      const file = files[i]

      if (file.name.endsWith('.cs') || file.name.endsWith('.ts') || file.name.endsWith('.vue')) {
        const reader = new FileReader()

        reader.onload = (e) => {
          const fileContent = e.target?.result as string
          const links = fileContent.split('\n').filter((line) => line.includes('using'))
          diagramData.value.push({
            name: file.name,
            links
          })
        }

        reader.readAsText(file)
      }
    }
  })
})

// const projects = defineModel('projects', {
//   type: Array,
//   default: [{ name: 'asd', tasks: [{ name: 'asd' }, { name: 'asd' }] }]
// })

onBeforeMount(init)
async function init() {}
</script>
