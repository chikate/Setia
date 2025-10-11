<template>
  <div class="flex flex-row align-items-center justify-content-around">
    <Accordion
      style="width: 300px"
      multiple
      :activeIndex="[0, 1]"
      collapseIcon="pi pi-minus text-gray-200"
      class="custom-shadow-1 border-1 border-round border-gray-200 h-full overflow-auto"
    >
      <AccordionTab header="Editor">
        <div class="flex flex-row justify-content-between gap-2">
          <div class="flex flex-column gap-2 align-items-center">
            <Button icon="pi pi-bars" @click="console.log(textEditor)" />
            <label>Left</label>
          </div>
          <div class="flex flex-column gap-2 align-items-center">
            <Button icon="pi pi-bars" @click="console.log(textEditor)" />
            <label>Justify</label>
          </div>
          <div class="flex flex-column gap-2 align-items-center">
            <Button icon="pi pi-bars" @click="console.log(textEditor)" />
            <label>Center</label>
          </div>
          <div class="flex flex-column gap-2 align-items-center">
            <Button icon="pi pi-bars" @click="console.log(textEditor)" />
            <label>Right</label>
          </div>
        </div>
      </AccordionTab>
      <AccordionTab header="Parameters">
        <ParametersComponent :parameters @paramDblClick="handleParamDblClick" />
      </AccordionTab>
      <AccordionTab header="Exports">
        <div class="flex-wrap gap-2">
          <Button
            v-tooltip.top="'PDF'"
            :loading="loadingExport"
            icon="pi pi-file-pdf"
            class="bg-red-500"
            @click="exporter('pdf')"
          />
          <Button
            v-tooltip.top="'DOC'"
            :loading="loadingExport"
            icon="pi pi-file-word"
            class="bg-blue-600"
            @click="exporter('doc')"
          />
          <Button
            v-tooltip.top="'PNG'"
            :loading="loadingExport"
            icon="pi pi-image"
            class="bg-purple-500"
            @click="exporter('png')"
          />
          <Button
            v-tooltip.top="'CSV'"
            :loading="loadingExport"
            icon="pi pi-file-excel"
            class="bg-green-600"
            @click="exporter('xls')"
          />
          <Button
            v-tooltip.top="'Save to Cloud'"
            :loading="loadingExport"
            icon="pi pi-cloud-upload"
            class="bg-blue-300"
            @click="exporter('xls')"
          />
        </div>
      </AccordionTab>
      <AccordionTab header="Properties">
        <div class="flex flex-column gap-2">
          <div class="flex flex-row gap-2">
            <Checkbox v-model="autoPagination" binary />
            <label>Pagination</label>
          </div>
          <InputText
            v-model="padding"
            placeholder="Padding - left, top, bottom, right / x, y"
          />
        </div>
      </AccordionTab>
      <AccordionTab header="Dimensions">
        <div class="flex flex-row gap-2 align-items-center">
          <InputNumber v-model="paperAspectRatio" />
          <Button
            icon="pi pi-clipboard"
            :style="`transform: rotate(${isLandscape ? '-90deg' : '0'})`"
            @click="isLandscape = !isLandscape"
          />
        </div>
      </AccordionTab>
    </Accordion>
    <div class="flex flex-column gap-2 align-items-center">
      <label>Editor</label>
      <BetterEditor :pages contenteditable ref="textEditor" />
    </div>
    <div class="flex flex-column gap-2 align-items-center">
      <label>View</label>
      <BetterEditor :pages ref="renderer" />
    </div>
  </div>
</template>

<script setup lang="ts">
import * as htmlToImage from "html-to-image";
import type { IParameter } from "@/components/experimental/ParametersComponent.vue";

defineOptions({
  name: "Raports",
  icon: "📜",
});

const parameters = ref<IParameter[]>([{ name: "test", type: "value" }]);
const paperAspectRatio = ref<number>(1.414);
const pages = ref<HTMLElement[]>([
  {
    innerHTML: "<p>asd</p><p>dsa</p>",
  } as HTMLElement,
  {
    innerHTML: "<p>asd1</p><p>asd1</p>",
  } as HTMLElement,
]);
const pageNumber = ref<number>(1);
const isLandscape = ref(false);
const autoPagination = ref(false);
const padding = ref("");
const textEditor = ref();
const loadingExport = ref(false);
const exportFileName = ref<string>("export");

function handleParamDblClick(event: any) {
  pages.value[
    pageNumber.value - 1
  ] += `<p><span style="color: rgb(0, 102, 204)">@${event.target.innerHTML}</span></p>`;
}

async function exporter(extenstion: string) {
  loadingExport.value = true;
  const paperToDownload = document.getElementById("paper") as HTMLElement;
  paperToDownload.style.borderRadius = "0";

  if (extenstion.includes("png"))
    await htmlToImage
      .toPng(paperToDownload)
      .then((url: string) =>
        downloadInBrowser(url, exportFileName.value + ".png")
      );

  // if (extenstion.includes('pdf'))
  //   await new jsPDF({
  //     orientation: isLandscape.value ? 'l' : 'p',
  //     unit: 'mm',
  //     format: [297, 210]
  //   })
  //     .html(paperToDownload)
  //     .save(exportFileName.value + '.pdf')

  if (extenstion.includes("doc"))
    downloadInBrowser(
      URL.createObjectURL(
        new Blob(
          [
            "\ufeffdata:application/vnd.ms-word;charset=utf-8," +
              encodeURIComponent(
                `<html
                  xmlns:o="urn:schemas-microsoft-com:office:office"
                  xmlns:w="urn:schemas-microsoft-com:office:word"
                  xmlns="http://www.w3.org/TR/REC-html40"
                >
                  <head><meta charset="utf-8" /></head>
                  <body>${paperToDownload.innerHTML}</body>
                </html>`
              ),
          ],
          { type: "application/msword" }
        )
      ),
      exportFileName.value + ".doc"
    );

  setTimeout(() => {
    loadingExport.value = false;
  }, 300);
}
</script>

<style scoped>
:deep().p-inputnumber.p-inputnumber-buttons-horizontal .p-inputnumber-button {
  border: 0 none !important;
}
</style>
