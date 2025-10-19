<template>
  <div class="flex flex-row gap-2">
    <div class="flex flex-column gap-2">
      <div class="font-medium p-2 gap-2 flex flex-column">
        Parameters
        <InputText
          placeholder="Add parameter"
          @keydown.enter="
            (event: any) => {
              if (
                event.target.value &&
                event.target.value &&
                event.target.value != null &&
                event.target.value != undefined
              )
                if (parameters.indexOf(event.target.value as never) < 0) {
                  parameters.push(event.target.value as never)
                  event.target.value = ''
                } else {
                  $toast.add({
                    severity: 'info',
                    summary: 'Info',
                    detail: 'The parameter already exists!'
                  })
                  event.target.loading = true
                  setTimeout(() => {
                    event.target.loading = false
                  }, 3000)
                }
              else
                $toast.add({
                  severity: 'info',
                  summary: 'Info',
                  detail: 'Invalid parameter!'
                })
            }
          "
        />
        <div
          class="parameter flex flex-row justify-content-between align-items-center cursor-pointer"
          :draggable="true"
          :ondrag="dragStart"
          v-for="item in parameters"
          :key="item"
        >
          <Dropdown
            :model-value="{ name: 'Value', type: 'Value' }"
            :options="[
              { name: 'Table', type: 'Table' },
              { name: 'Value', type: 'Value' },
              { name: 'File', type: 'File' },
            ]"
            option-label="name"
            class="border-0"
          />
          {{ item }}
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
defineOptions({
  name: "MetaData",
  icon: "☄️",
});

const parameters = ref([]);
// const parameters = defineModel('parameters', {
//   type: Array<{ name: string; type: string }>,
//   required: true,
//   default: []
// })

async function dragStart(ev: any) {
  ev.dataTransfer.setData("text", ev.target.id);
}
// async function dragOver(ev: any) {
//   ev.preventDefault()
// }
// async function drop(ev: any) {
//   ev.preventDefault()
// }
</script>
