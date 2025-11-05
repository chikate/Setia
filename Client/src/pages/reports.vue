<template>
  <div class="container">
    <Accordion
      style="width: 20rem"
      multiple
      :activeIndex="[0, 1]"
      collapseIcon="pi pi-minus text-gray-200"
      class="border-round overflow-auto"
    >
      <AccordionTab header="Editor">
        <div class="flex flex-column justify-content-between">
          <div class="flex">
            <Button
              class="font-bold"
              label="B"
              @click="toggleBold"
              :class="{ active: isActive('bold') }"
            />
            <Button
              label="I"
              @click="toggleItalic"
              :class="{ active: isActive('italic') }"
            />
            <Button
              label="U"
              @click="toggleUnderline"
              :class="{ active: isActive('underline') }"
            />
          </div>
          <div class="flex">
            <Button
              icon="pi pi-align-left"
              @click="setAlign('left')"
              :class="{ active: isActive({ textAlign: 'left' }) }"
            />
            <Button
              icon="pi pi-align-justify"
              @click="setAlign('justify')"
              :class="{ active: isActive({ textAlign: 'justify' }) }"
            />
            <Button
              icon="pi pi-align-center"
              @click="setAlign('center')"
              :class="{ active: isActive({ textAlign: 'center' }) }"
            />
            <Button
              icon="pi pi-align-right"
              @click="setAlign('right')"
              :class="{ active: isActive({ textAlign: 'right' }) }"
            />
          </div>
          <Dropdown
            :options="['H1', 'H2', 'H3']"
            @change="setHeading($event.target.value)"
          />
          <Dropdown
            :options="[1, 1.5, 2]"
            @change="setLineHeight($event.target.value)"
          />
        </div>
      </AccordionTab>
      <AccordionTab header="Parameters">
        <ParametersComponent :parameters @paramDblClick="handleParamDblClick" />
      </AccordionTab>
      <AccordionTab header="Exports">
        <div class="flex-wrap">
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
        <div class="flex flex-column">
          <div class="flex flex-row">
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
        <div class="flex flex-row align-items-center">
          <InputNumber v-model="paperAspectRatio" />
          <Button
            icon="pi pi-clipboard"
            :style="`transform: rotate(${isLandscape ? '-90deg' : '0'})`"
            @click="isLandscape = !isLandscape"
          />
        </div>
      </AccordionTab>
    </Accordion>
    <EditorContent class="editor custom-shadow-1" :editor />
    <div class="rendered custom-shadow-1" v-html="editor.getHTML()" />
  </div>
</template>

<script setup lang="ts">
import { Editor, EditorContent } from "@tiptap/vue-3";
import StarterKit from "@tiptap/starter-kit";
import Underline from "@tiptap/extension-underline";
import TextAlign from "@tiptap/extension-text-align";
import { Extension } from "@tiptap/core";
import * as htmlToImage from "html-to-image";

defineOptions({
  name: "Reports",
  icon: "📜",
});

// State
const parameters = ref([{ name: "test", type: "value" }]);
const paperAspectRatio = ref(1.414);
const pages = ref(["<p>asd</p><p>dsa</p>", "<p>asd1</p><p>asd1</p>"]);
const pageNumber = ref(1);
const isLandscape = ref(false);
const autoPagination = ref(false);
const padding = ref("");
const loadingExport = ref(false);
const exportFileName = ref("export");

// Event handlers
function handleParamDblClick(event: any) {
  pages.value[
    pageNumber.value - 1
  ] += `<p><span style="color: rgb(0, 102, 204)">@${event.target.innerHTML}</span></p>`;
}

// Exporter
async function exporter(extension: string) {
  loadingExport.value = true;
  const paperToDownload = document.getElementById("paper") as HTMLElement;
  if (!paperToDownload) return;

  paperToDownload.style.borderRadius = "0";

  if (extension == "png") {
    const url = await htmlToImage.toPng(paperToDownload);
    downloadInBrowser(url, `${exportFileName.value}.png`);
  } else if (extension == "doc") {
    const blob = new Blob(
      [
        "\ufeffdata:application/vnd.ms-word;charset=utf-8," +
          encodeURIComponent(
            `<html
              xmlns:o="urn:schemas-microsoft-com:office:office"
              xmlns:w="urn:schemas-microsoft-com:office:word"
              xmlns="http://www.w3.org/TR/REC-html40">
              <head><meta charset="utf-8" /></head>
              <body>${paperToDownload.innerHTML}</body>
            </html>`
          ),
      ],
      { type: "application/msword" }
    );
    downloadInBrowser(URL.createObjectURL(blob), `${exportFileName.value}.doc`);
  }

  loadingExport.value = false;
}

// Line height extension
const LineHeight = Extension.create({
  name: "lineHeight",
  addGlobalAttributes() {
    return [
      {
        types: ["paragraph", "heading"],
        attributes: {
          lineHeight: {
            default: "0.1",
            renderHTML: (attributes) => {
              if (!attributes.lineHeight) return {};
              return { style: `line-height: ${attributes.lineHeight}` };
            },
            parseHTML: (element) => element.style.lineHeight || "1",
          },
        },
      },
    ];
  },
  addCommands() {
    return {
      setLineHeight:
        (value: string) =>
        ({ commands }: any) =>
          commands.updateAttributes("paragraph", { lineHeight: value }),
    };
  },
});

// Editor setup
const editor = new Editor({
  extensions: [
    StarterKit,
    Underline,
    LineHeight,
    TextAlign.configure({
      types: ["heading", "paragraph"],
    }),
  ],
  content: pages.value[0],
});

// Toolbar methods
const toggleBold = () => editor.chain().focus().toggleBold().run();
const toggleItalic = () => editor.chain().focus().toggleItalic().run();
const toggleUnderline = () => editor.chain().focus().toggleUnderline().run();
const isActive = (format: any) => editor.isActive(format);
const setAlign = (alignment: string) =>
  editor.chain().focus().setTextAlign(alignment).run();
const setHeading = (level: 1 | 2 | 3 | 4 | 5 | 6) => {
  if (!level) editor.chain().focus().setParagraph().run();
  else editor.chain().focus().toggleHeading({ level }).run();
};
const setLineHeight = (value: string) =>
  editor.chain().focus().setLineHeight(value).run();
</script>

<style scoped>
.container {
  display: flex;
  height: 100%;
  justify-content: space-between;
  gap: 0.5rem;
}

.editor,
.rendered {
  background-color: white;
  /* white-space: pre-wrap; */
  color: black;
  border-radius: 5px;
  aspect-ratio: 1/1.414;
  padding: 1rem;
  cursor: text;
}
</style>
