<template>
  <div class="flex-row gap-2 align-items-center">
    <Accordion
      style="width: 300px"
      multiple
      :activeIndex="[0, 1, 2, 3, 4]"
      collapseIcon="pi pi-minus text-gray-200"
      class="custom-shadow-1"
    >
      <AccordionTab header="Exports">
        <div class="flex-wrap gap-2">
          <Button
            :loading="loadingExport"
            icon="pi pi-file-pdf"
            class="bg-red-500"
            @click="exporter('pdf')"
          />
          <Button
            :loading="loadingExport"
            icon="pi pi-file-word"
            class="bg-blue-600"
            @click="exporter('doc')"
          />
          <Button
            :loading="loadingExport"
            icon="pi pi-image"
            class="bg-purple-500"
            @click="exporter('png')"
          />
          <Button
            :loading="loadingExport"
            icon="pi pi-file-excel"
            class="bg-green-600"
            @click="exporter('xls')"
          />
        </div>
      </AccordionTab>
      <AccordionTab header="Tools">
        <div class="flex-wrap gap-2">
          <Button icon="pi pi-at" :draggable="true" />
        </div>
      </AccordionTab>
      <AccordionTab header="Properties">
        <div class="flex-row gap-2">
          <Checkbox v-model="autoPagination" binary />
          <label>Pagination</label>
        </div>
      </AccordionTab>
      <AccordionTab header="Dimensions">
        <div class="flex-row gap-2 align-items-center">
          <InputNumber v-model="paperAspectRatio" />
          <Button
            icon="pi pi-clipboard"
            :style="`transform: rotate(${isLandscape ? '-90deg' : '0'})`"
            @click="isLandscape = !isLandscape"
          />
        </div>
      </AccordionTab>
      <AccordionTab header="Parameters">
        <ParametersComponent :parameters @paramDblClick="handleParamDblClick" />
      </AccordionTab>
    </Accordion>

    <div class="flex-column gap-2 align-items-center justify-content-center">
      <EditorComponent v-model="pages[pageNumber - 1]" />

      <div class="flex-row align-items-center gap-2">
        <InputNumber
          :min="1"
          :max="pages?.length"
          :useGrouping="false"
          showButtons
          buttonLayout="horizontal"
          fluid
          @input="
            (event: InputNumberInputEvent) => {
              if (
                event.value == null ||
                event.value == undefined ||
                Number(event.value) < 1 ||
                isNaN(Number(event.value)) ||
                !String(event.value).trim()[0]
              )
                event.value = 1
            }
          "
          class="small"
          v-model="pageNumber"
        >
          <template #incrementbuttonicon>
            <span class="pi pi-chevron-right" />
          </template>
          <template #decrementbuttonicon>
            <span class="pi pi-chevron-left" />
          </template>
        </InputNumber>
        out of {{ pages?.length }} pages
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import * as htmlToImage from 'html-to-image'
import { jsPDF } from 'jspdf'
import { download } from '@/helpers'

import EditorComponent from '@/components/better/EditorComponent.vue'

import { IParameter } from '@/components/experimental/ParametersComponent.vue'
import { InputNumberInputEvent } from 'primevue/inputnumber'

const parameters = ref<IParameter[]>([{ name: 'test', type: 'value' }])
const paperAspectRatio = ref<number>(1.414)
const pages = ref<string[]>(['<p style="height:100px">asd</p><p>dsa</p>', '<p>asd</p><p>asd</p>'])
const pageNumber = ref<number>(1)
const isLandscape = ref(false)
const autoPagination = ref(false)
const loadingExport = ref(false)
const exportFileName = ref<string>('export')

function handleParamDblClick(event: any) {
  pages.value[pageNumber.value - 1] +=
    `<p><span style="color: rgb(0, 102, 204)">@${event.target.innerHTML}</span></p>`
}

async function exporter(extenstion: string) {
  loadingExport.value = true
  const paperToDownload = document.getElementById('paper') as HTMLElement
  paperToDownload.style.borderRadius = '0'

  if (extenstion.includes('png'))
    await htmlToImage
      .toPng(paperToDownload)
      .then((url: string) => download(url, exportFileName.value + '.png'))

  if (extenstion.includes('pdf'))
    await new jsPDF({
      orientation: isLandscape.value ? 'l' : 'p',
      unit: 'mm',
      format: [297, 210]
    })
      .html(paperToDownload)
      .save(exportFileName.value + '.pdf')

  if (extenstion.includes('doc'))
    download(
      URL.createObjectURL(
        new Blob(
          [
            '\ufeffdata:application/vnd.ms-word;charset=utf-8,' +
              encodeURIComponent(
                `<html
                  xmlns:o="urn:schemas-microsoft-com:office:office"
                  xmlns:w="urn:schemas-microsoft-com:office:word"
                  xmlns="http://www.w3.org/TR/REC-html40"
                >
                  <head><meta charset="utf-8" /></head>
                  <body>${paperToDownload.innerHTML}</body>
                </html>`
              )
          ],
          { type: 'application/msword' }
        )
      ),
      exportFileName.value + '.doc'
    )

  setTimeout(() => (loadingExport.value = false), 300)
}
</script>

<style scoped>
:deep(.p-inputnumber.p-inputnumber-buttons-horizontal .p-inputnumber-button) {
  border: 0 none !important;
}
</style>
