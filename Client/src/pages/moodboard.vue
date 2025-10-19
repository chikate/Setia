<template>
  <div
    class="w-full h-full overflow-hidden bg-neutral-900 relative"
    @wheel.prevent="onWheel"
    @mousedown="startPan"
    @mouseup="stopPan"
    @mousemove="onPan"
    @dragover.prevent
    @drop.prevent="onDrop"
  >
    <div
      class="absolute top-0 left-0"
      :style="{
        transform: `translate(${pan.x}px, ${pan.y}px) scale(${zoom})`,
        transformOrigin: '0 0',
      }"
    >
      <div
        v-for="(img, i) in images"
        :key="i"
        class="absolute cursor-move"
        :style="{
          top: img.y + 'px',
          left: img.x + 'px',
          transform: `scale(${img.scale})`,
        }"
        @mousedown.stop="startDrag(i, $event)"
      >
        <img :src="img.src" class="max-w-full max-h-full select-none" />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive } from "vue";

defineOptions({
  name: "Moodboard",
  icon: "🪞",
});

type ImgNode = {
  src: string;
  x: number;
  y: number;
  scale: number;
};

const images = ref<ImgNode[]>([]);
const pan = reactive({ x: 0, y: 0 });
const zoom = ref(1);

let isPanning = false;
let panStart = { x: 0, y: 0, px: 0, py: 0 };

function startPan(e: MouseEvent) {
  if ((e.target as HTMLElement).tagName == "IMG") return; // don't pan when dragging images
  isPanning = true;
  panStart = { x: e.clientX, y: e.clientY, px: pan.x, py: pan.y };
}
function stopPan() {
  isPanning = false;
}
function onPan(e: MouseEvent) {
  if (isPanning) {
    pan.x = panStart.px + (e.clientX - panStart.x);
    pan.y = panStart.py + (e.clientY - panStart.y);
  }
}

function onWheel(e: WheelEvent) {
  zoom.value *= e.deltaY > 0 ? 0.9 : 1.1;
}

// Dragging images
let draggingIndex: number | null = null;
let dragOffset = { x: 0, y: 0 };

function startDrag(i: number, e: MouseEvent) {
  draggingIndex = i;
  dragOffset = { x: e.offsetX, y: e.offsetY };
  window.addEventListener("mousemove", onDrag);
  window.addEventListener("mouseup", stopDrag);
}
function onDrag(e: MouseEvent) {
  if (draggingIndex != null) {
    const img = images.value[draggingIndex];
    img.x = e.clientX / zoom.value - dragOffset.x - pan.x / zoom.value;
    img.y = e.clientY / zoom.value - dragOffset.y - pan.y / zoom.value;
  }
}
function stopDrag() {
  draggingIndex = null;
  window.removeEventListener("mousemove", onDrag);
  window.removeEventListener("mouseup", stopDrag);
}

// Drag & drop images from file or browser
function onDrop(e: DragEvent) {
  const files = e.dataTransfer?.files;
  if (files) {
    for (const file of files) {
      if (file.type.startsWith("image/")) {
        const url = URL.createObjectURL(file);
        images.value.push({ src: url, x: 0, y: 0, scale: 1 });
      }
    }
  }
  const url = e.dataTransfer?.getData("text/uri-list");
  if (url) {
    images.value.push({ src: url, x: 0, y: 0, scale: 1 });
  }
}
</script>

<style scoped>
html,
body,
#app {
  height: 100%;
  margin: 0;
}
</style>
