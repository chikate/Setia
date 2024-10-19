<template>
  <div class="flex-row gap-2 overflow-hidden">
    <div class="flex-column gap-2">
      <div class="card font-medium p-2 gap-2 flex-column">
        Parameters
        <InputText
          placeholder="Add parameter"
          @keydown.enter="
            (event: any) => {
              if (
                event.target.value &&
                event.target.value != '' &&
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
                  setTimeout(3000, () => (event.target.loading = false))
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
          class="parameter flex-row justify-content-between align-items-center cursor-pointer"
          :draggable="true"
          :ondrag="dragStart"
          v-for="item in parameters"
          :key="item"
        >
          <Dropdown
            :options="[
              { name: 'Table', type: 'Table' },
              { name: 'In line', type: 'In line' },
              { name: 'Image', type: 'Image' }
            ]"
            option-label="name"
            class="border-0"
          />
          {{ item }}
        </div>
      </div>
      <BetterCheckBox label="Auto pageing" />
    </div>

    <!-- / -->

    <div
      class="border-round border-1 flex-column"
      :style="`height: ${pageHeight}px; width: ${pageWidth}px;`"
    >
      <Splitter layout="vertical" class="border-0">
        <SplitterPanel :size="7" class="p-3 pb-0 flex-wrap justify-content-around">
          <div>Header</div>
        </SplitterPanel>
        <SplitterPanel :size="88" class="px-3 flex-grow-1 flex-column">1 </SplitterPanel>
        <SplitterPanel :size="5" class="p-3 pt-0 flex-wrap justify-content-between">
          <div>Footer</div>
          <div>1</div>
        </SplitterPanel>
      </Splitter>

      <!-- <Editor :class="{ 'border-1': show }" />
      <Editor
        :class="{ 'border-1': show }"
        :style="`height: ${pageHeight}px; width: ${pageWidth}px;`"
      /> -->
    </div>

    <!-- / -->
  </div>
</template>

<script setup lang="ts">
const parameters = ref([])
// const show = ref<boolean>(true)
// const useFooter = ref<boolean>(true)
// const autoPaginate = ref<boolean>(true)
const pageWidth = ref(2480 / 4) // 2480 x 3508
const pageHeight = ref(3508 / 4) // 2480 x 3508
// const parameters = defineModel('parameters', {
//   type: Array<{ name: string; type: string }>,
//   required: true,
//   default: []
// })

async function dragStart(ev: any) {
  ev.dataTransfer.setData('text', ev.target.id)
}
// async function dragOver(ev: any) {
//   ev.preventDefault()
// }
// async function drop(ev: any) {
//   ev.preventDefault()
// }
</script>
