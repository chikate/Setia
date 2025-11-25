<template>
  <div
    class="report-editor-container w-full flex flex-row h-screen overflow-hidden bg-gray-50"
  >
    <div
      class="left-sidebar w-20rem bg-white p-2 border-right-1 border-gray-200 flex flex-column transition-all transition-duration-300"
      :class="{ 'w-0 hidden': !showLeftSidebar }"
    >
      <ParametersComponent
        v-model:parameters="parameters"
        @paramDblClick="handleParamDblClick"
      />
      <h3 class="text-sm font-semibold text-gray-600 mb-2">Snippets</h3>
      <Button
        v-for="snippet in snippets"
        :key="snippet.label"
        :label="snippet.label"
        :icon="snippet.icon"
        size="small"
        outlined
        class="w-full text-left justify-content-start mb-2"
        @click="snippet.action"
      />
    </div>

    <div class="flex flex-column w-full bg-gray-100 relative">
      <div
        class="editor-toolbar flex align-items-center gap-1 p-2 bg-white border-bottom-1 border-gray-200 shadow-1 overflow-x-auto"
      >
        <Button
          icon="pi pi-file-pdf"
          severity="danger"
          text
          @click="exporter('pdf')"
          :loading="loadingExport"
        />
        <Button
          icon="pi pi-file-word"
          severity="info"
          text
          @click="exporter('doc')"
          :loading="loadingExport"
        />
        <Button
          icon="pi pi-save"
          text
          @click="saveReport"
          :loading="loadingSave"
        />

        <Button
          icon="pi pi-undo"
          text
          rounded
          @click="editor?.chain().focus().undo().run()"
          :disabled="!editor?.can().undo()"
          v-tooltip.bottom="'Undo'"
        />
        <Button
          icon="pi pi-refresh"
          text
          rounded
          @click="editor?.chain().focus().redo().run()"
          :disabled="!editor?.can().redo()"
          v-tooltip.bottom="'Redo'"
        />

        <div class="w-1px h-2rem bg-gray-300 mx-2"></div>

        <Button
          v-for="btn in formatButtons"
          :key="btn.tooltip"
          :icon="btn.icon"
          text
          rounded
          @click="btn.action"
          :class="{ 'bg-blue-50 text-blue-600': isActive(btn.active) }"
          v-tooltip.bottom="btn.tooltip"
        />

        <div class="w-1px h-2rem bg-gray-300 mx-2"></div>

        <div
          class="relative flex align-items-center justify-content-center w-2rem h-2rem border-circle hover:bg-gray-100 cursor-pointer overflow-hidden"
          v-tooltip.bottom="'Text Color'"
        >
          <input
            type="color"
            class="absolute opacity-0 w-full h-full cursor-pointer z-1"
            @input="(e: any) => editor?.chain().focus().setColor(e.target.value).run()"
            :value="editor?.getAttributes('textStyle').color || '#333333'"
          />
          <i
            class="pi pi-palette"
            :style="{
              color: editor?.getAttributes('textStyle').color || '#333333',
            }"
          ></i>
        </div>

        <div class="w-1px h-2rem bg-gray-300 mx-2"></div>

        <Button
          v-for="btn in alignButtons"
          :key="btn.tooltip"
          :icon="btn.icon"
          text
          rounded
          @click="setAlign(btn.align)"
          :class="{
            'bg-blue-50 text-blue-600': isActive({ textAlign: btn.align }),
          }"
          v-tooltip.bottom="btn.tooltip"
        />

        <div class="w-1px h-2rem bg-gray-300 mx-2"></div>

        <Dropdown
          :options="headingOptions"
          v-model="selectedHeading"
          optionLabel="label"
          optionValue="value"
          placeholder="Normal"
          class="w-8rem"
          @change="setHeading($event.value)"
        />

        <div class="w-1px h-2rem bg-gray-300 mx-2"></div>

        <Button
          v-for="btn in listButtons"
          :key="btn.tooltip"
          :icon="btn.icon"
          text
          rounded
          @click="btn.action"
          :class="{ 'bg-blue-50 text-blue-600': isActive(btn.active) }"
          v-tooltip.bottom="btn.tooltip"
        />

        <div class="w-1px h-2rem bg-gray-300 mx-2"></div>

        <Button
          icon="pi pi-table"
          text
          rounded
          @click="insertTable"
          v-tooltip.bottom="'Insert Table'"
        />
        <Button
          icon="pi pi-image"
          text
          rounded
          @click="addImage"
          v-tooltip.bottom="'Insert Image'"
        />

        <div class="w-1px h-2rem bg-gray-300 mx-2"></div>

        <Button
          icon="pi pi-minus"
          text
          rounded
          @click="setHorizontalRule"
          v-tooltip.bottom="'Horizontal Rule'"
        />

        <div class="flex-grow-1"></div>

        <Button
          icon="pi pi-cog"
          text
          rounded
          @click="showRightSidebar = !showRightSidebar"
          :class="{ 'bg-blue-50 text-blue-600': showRightSidebar }"
          v-tooltip.bottom="'Properties'"
        />
      </div>

      <div
        class="editor-canvas flex-grow-1 overflow-auto p-5 flex justify-content-center align-items-start"
        @click.self="editor?.commands.focus()"
      >
        <div
          id="paper"
          class="paper bg-white shadow-3 transition-all transition-duration-200"
          :style="{
            width: paperWidth,
            minHeight: paperHeight,
            padding: padding,
            transform: `scale(${zoomLevel})`,
            transformOrigin: 'top center',
          }"
        >
          <EditorContent :editor="editor" class="h-full" />
        </div>
      </div>

      <div
        class="absolute bottom-0 right-0 m-4 bg-white shadow-2 border-round p-2 flex align-items-center gap-2 z-1"
      >
        <Button icon="pi pi-minus" text rounded size="small" @click="zoomOut" />
        <span class="text-sm w-3rem text-center"
          >{{ Math.round(zoomLevel * 100) }}%</span
        >
        <Button icon="pi pi-plus" text rounded size="small" @click="zoomIn" />
      </div>
    </div>

    <div
      class="right-sidebar w-18rem p-2 bg-white border-left-1 border-gray-200 flex flex-column transition-all transition-duration-300"
      :class="{ 'w-0 hidden': !showRightSidebar }"
    >
      <div class="flex flex-column gap-2 mb-4">
        <label class="text-sm font-medium text-gray-600">Orientation</label>
        <SelectButton
          v-model="orientation"
          :options="['Portrait', 'Landscape']"
          :allowEmpty="false"
        />
      </div>
      <div class="flex flex-column gap-2 mb-4">
        <label class="text-sm font-medium text-gray-600">Paper Size</label>
        <Dropdown
          v-model="paperSize"
          :options="paperSizes"
          optionLabel="name"
          class="w-full"
        />
      </div>
      <div class="flex flex-column gap-2 mb-4">
        <label class="text-sm font-medium text-gray-600">Margins</label>
        <InputText v-model="padding" placeholder="e.g. 2rem" />
      </div>
      <div class="flex flex-column gap-2 mb-4">
        <label class="text-sm font-medium text-gray-600">Line Height</label>
        <Dropdown
          v-model="lineHeight"
          :options="['1.0', '1.15', '1.5', '2.0']"
          @change="setLineHeight($event.value)"
          class="w-full"
        />
      </div>
      <Divider />
      <div class="flex align-items-center gap-2 mt-2">
        <Checkbox v-model="autoPagination" binary inputId="pagination" />
        <label for="pagination" class="text-sm text-gray-700"
          >Auto Pagination</label
        >
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from "vue";
import { Editor, EditorContent } from "@tiptap/vue-3";
import StarterKit from "@tiptap/starter-kit";
import Underline from "@tiptap/extension-underline";
import TextAlign from "@tiptap/extension-text-align";
import Placeholder from "@tiptap/extension-placeholder";
import { Extension } from "@tiptap/core";
import { Color } from "@tiptap/extension-color";
import { TextStyle } from "@tiptap/extension-text-style";
import { Table } from "@tiptap/extension-table";
import { TableCell } from "@tiptap/extension-table-cell";
import { TableHeader } from "@tiptap/extension-table-header";
import { TableRow } from "@tiptap/extension-table-row";
import { Image } from "@tiptap/extension-image";
import * as htmlToImage from "html-to-image";
import { useToast } from "primevue/usetoast";

defineOptions({ name: "Reports", icon: "📜" });
const toast = useToast();

const showLeftSidebar = ref(true);
const showRightSidebar = ref(true);
const loadingExport = ref(false);
const loadingSave = ref(false);
const zoomLevel = ref(1);
const exportFileName = ref("report_export");

const orientation = ref<"Portrait" | "Landscape">("Portrait");
const paperSize = ref({ name: "A4", width: "210mm", height: "297mm" });
const padding = ref("2.54cm");
const lineHeight = ref("1.15");
const autoPagination = ref(false);

const paperSizes = [
  { name: "A4", width: "210mm", height: "297mm" },
  { name: "Letter", width: "215.9mm", height: "279.4mm" },
  { name: "Legal", width: "215.9mm", height: "355.6mm" },
];

const parameters = ref([
  { name: "ClientName", type: "value" },
  { name: "Date", type: "value" },
]);
const content = ref(
  `<h1>Report Title</h1><p>Start typing your report here...</p>`
);

const headingOptions = [
  { label: "Normal", value: 0 },
  { label: "Heading 1", value: 1 },
  { label: "Heading 2", value: 2 },
  { label: "Heading 3", value: 3 },
];
const selectedHeading = ref(0);

const paperWidth = computed(() =>
  orientation.value === "Portrait"
    ? paperSize.value.width
    : paperSize.value.height
);
const paperHeight = computed(() =>
  orientation.value === "Portrait"
    ? paperSize.value.height
    : paperSize.value.width
);

const LineHeight = Extension.create({
  name: "lineHeight",
  addGlobalAttributes() {
    return [
      {
        types: ["paragraph", "heading"],
        attributes: {
          lineHeight: {
            default: "1.15",
            renderHTML: (attributes) =>
              attributes.lineHeight
                ? { style: `line-height: ${attributes.lineHeight}` }
                : {},
            parseHTML: (element) => element.style.lineHeight || "1.15",
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

const editor = new Editor({
  extensions: [
    StarterKit,
    Underline,
    LineHeight,
    TextStyle,
    Color,
    TableRow,
    TableHeader,
    TableCell,
    Image,
    TextAlign.configure({ types: ["heading", "paragraph"] }),
    Placeholder.configure({ placeholder: "Write something..." }),
    Table.configure({ resizable: true }),
  ],
  content: content.value,
  onUpdate: ({ editor }) => updateHeading(editor),
  onSelectionUpdate: ({ editor }) => updateHeading(editor),
});

const updateHeading = (editor: any) => {
  if (editor.isActive("heading", { level: 1 })) selectedHeading.value = 1;
  else if (editor.isActive("heading", { level: 2 })) selectedHeading.value = 2;
  else if (editor.isActive("heading", { level: 3 })) selectedHeading.value = 3;
  else selectedHeading.value = 0;
};

const formatButtons = [
  {
    icon: "pi pi-bold",
    action: () => editor.chain().focus().toggleBold().run(),
    active: "bold",
    tooltip: "Bold",
  },
  {
    icon: "pi pi-italic",
    action: () => editor.chain().focus().toggleItalic().run(),
    active: "italic",
    tooltip: "Italic",
  },
  {
    icon: "pi pi-underline",
    action: () => editor.chain().focus().toggleUnderline().run(),
    active: "underline",
    tooltip: "Underline",
  },
  {
    icon: "pi pi-strikethrough",
    action: () => editor.chain().focus().toggleStrike().run(),
    active: "strike",
    tooltip: "Strikethrough",
  },
];

const alignButtons = [
  { icon: "pi pi-align-left", align: "left", tooltip: "Align Left" },
  { icon: "pi pi-align-center", align: "center", tooltip: "Align Center" },
  { icon: "pi pi-align-right", align: "right", tooltip: "Align Right" },
  { icon: "pi pi-align-justify", align: "justify", tooltip: "Justify" },
];

const listButtons = [
  {
    icon: "pi pi-list",
    action: () => editor.chain().focus().toggleBulletList().run(),
    active: "bulletList",
    tooltip: "Bullet List",
  },
  {
    icon: "pi pi-sort-numeric-down",
    action: () => editor.chain().focus().toggleOrderedList().run(),
    active: "orderedList",
    tooltip: "Ordered List",
  },
];

const snippets = [
  {
    label: "Insert Date",
    icon: "pi pi-calendar",
    action: () =>
      editor
        .chain()
        .focus()
        .insertContent(new Date().toLocaleDateString())
        .run(),
  },
  {
    label: "Insert Signature Line",
    icon: "pi pi-pencil",
    action: () =>
      editor
        .chain()
        .focus()
        .insertContent("<p>_________________________<br>Signature</p>")
        .run(),
  },
];

const isActive = (format: any) => editor.isActive(format);
const setAlign = (alignment: string) =>
  editor.chain().focus().setTextAlign(alignment).run();
const setHeading = (level: number) =>
  level === 0
    ? editor.chain().focus().setParagraph().run()
    : editor
        .chain()
        .focus()
        .toggleHeading({ level: level as any })
        .run();
const setLineHeight = (value: string) =>
  editor.chain().focus().setLineHeight(value).run();
const setHorizontalRule = () =>
  editor.chain().focus().setHorizontalRule().run();
const insertTable = () =>
  editor
    .chain()
    .focus()
    .insertTable({ rows: 3, cols: 3, withHeaderRow: true })
    .run();
const addImage = () => {
  const url = window.prompt("URL");
  if (url) editor.chain().focus().setImage({ src: url }).run();
};
const zoomIn = () => {
  if (zoomLevel.value < 2) zoomLevel.value += 0.1;
};
const zoomOut = () => {
  if (zoomLevel.value > 0.5) zoomLevel.value -= 0.1;
};

function handleParamDblClick(event: any) {
  editor
    .chain()
    .focus()
    .insertContent(
      `<span style="color: #0066cc; font-weight: bold;">@${event.target.innerText}</span>`
    )
    .run();
}

const download = (url: string, filename: string) => {
  const a = document.createElement("a");
  Object.assign(a, { href: url, download: filename });
  a.click();
  URL.revokeObjectURL(url);
};

const exportMap: Record<string, (el: HTMLElement) => Promise<void> | void> = {
  png: async (el) => {
    const url = await htmlToImage.toPng(el, {
      quality: 0.95,
      backgroundColor: "#ffffff",
    });
    download(url, `${exportFileName.value}.png`);
  },
  pdf: () => window.print(),
  doc: (el) => {
    const content = `<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:w="urn:schemas-microsoft-com:office:word" xmlns="http://www.w3.org/TR/REC-html40"><head><meta charset="utf-8" /></head><body>${el.innerHTML}</body></html>`;
    const url = URL.createObjectURL(
      new Blob(["\ufeff", content], { type: "application/msword" })
    );
    download(url, `${exportFileName.value}.doc`);
  },
};

async function exporter(type: string) {
  loadingExport.value = true;
  const el = document.getElementById("paper");
  if (!el) return (loadingExport.value = false);

  const { transform, margin } = el.style;
  Object.assign(el.style, { transform: "none", margin: "0" });

  try {
    if (exportMap[type]) await exportMap[type](el);
  } catch (e) {
    console.error(e);
    toast.add({
      severity: "error",
      summary: "Export Failed",
      detail: "Could not export document.",
    });
  } finally {
    Object.assign(el.style, { transform, margin });
    loadingExport.value = false;
  }
}

const saveReport = () => {
  loadingSave.value = true;
  setTimeout(() => {
    loadingSave.value = false;
    toast.add({
      severity: "success",
      summary: "Saved",
      detail: "Report saved successfully.",
    });
  }, 1000);
};
</script>

<style>
.ProseMirror {
  outline: none;
  min-height: 100%;
  color: #333333;
}
.ProseMirror p {
  margin: 0.5em 0;
}
.ProseMirror ul,
.ProseMirror ol {
  padding-left: 1.5rem;
}
.ProseMirror blockquote {
  border-left: 3px solid #ddd;
  padding-left: 1rem;
  color: #666;
  font-style: italic;
}
.ProseMirror h1,
.ProseMirror h2,
.ProseMirror h3 {
  line-height: 1.2;
  margin-top: 1em;
  margin-bottom: 0.5em;
}

@media print {
  .editor-toolbar,
  .left-sidebar,
  .right-sidebar,
  .zoom-controls {
    display: none !important;
  }
  .report-editor-container {
    height: auto !important;
    overflow: visible !important;
    background: white !important;
  }
  .editor-canvas {
    padding: 0 !important;
    overflow: visible !important;
  }
  #paper {
    box-shadow: none !important;
    transform: none !important;
    margin: 0 !important;
    width: 100% !important;
    max-width: none !important;
  }
}

.ProseMirror table {
  border-collapse: collapse;
  table-layout: fixed;
  width: 100%;
  margin: 0;
  overflow: hidden;
}
.ProseMirror td,
.ProseMirror th {
  min-width: 1em;
  border: 1px solid #ced4da;
  padding: 3px 5px;
  vertical-align: top;
  box-sizing: border-box;
  position: relative;
}
.ProseMirror th {
  font-weight: bold;
  text-align: left;
  background-color: #f8f9fa;
}
.ProseMirror .selectedCell:after {
  z-index: 2;
  position: absolute;
  content: "";
  left: 0;
  right: 0;
  top: 0;
  bottom: 0;
  background: rgba(200, 200, 255, 0.4);
  pointer-events: none;
}
</style>
