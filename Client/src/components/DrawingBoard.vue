<template>
  <div class="drawing-board">
    <div v-if="showToolbar" class="toolbar">
      <label>Color<input type="color" v-model="color" /></label>
      <label>
        Size<input type="range" min="1" max="60" v-model.number="size" />
        <span> {{ size }}px </span>
      </label>
      <Button @click="mode = 'draw'" :class="{ active: mode == 'draw' }">
        Draw
      </Button>
      <Button @click="mode = 'erase'" :class="{ active: mode == 'erase' }">
        Erase
      </Button>
      <Button @click="undo" :disabled="!undoStack.length">Undo</Button>
      <Button @click="clearCanvas">Clear</Button>
      <Button @click="savePNG">Save PNG</Button>
    </div>
    <canvas ref="canvas" />
  </div>
</template>

<script lang="ts" setup>
const props = defineProps({
  showToolbar: { type: Boolean, default: false },
});

const canvas = ref();
const ctx = ref();
const drawing = ref(false);
const last = ref({ x: 0, y: 0 });
const color = ref("#ffcc00");
const size = ref(6);
const mode = ref("draw");
const undoStack = ref([]);
const maxUndo = 20;

function pushUndo() {
  undoStack.value.push(canvas.value.toDataURL());
  if (undoStack.value.length > maxUndo) undoStack.value.shift();
}

function undo() {
  if (!undoStack.value.length) return;
  const img = new Image();
  img.onload = () =>
    ctx.value.clearRect(0, 0, canvas.value?.width, canvas.value?.height) ||
    ctx.value.drawImage(img, 0, 0, canvas.value?.width, canvas.value?.height);
  img.src = undoStack.value.pop();
}
function clearCanvas() {
  pushUndo();
  ctx.value.clearRect(0, 0, canvas.value?.width, canvas.value?.height);
}
function savePNG() {
  const a = document.createElement("a");
  a.href = canvas.value.toDataURL();
  a.download = `drawing_${Date.now()}.png`;
  a.click();
}

function getPos(e) {
  const r = canvas.value.getBoundingClientRect(),
    dpr = window.devicePixelRatio || 1;
  const x = (e.touches ? e.touches[0].clientX : e.clientX) - r.left;
  const y = (e.touches ? e.touches[0].clientY : e.clientY) - r.top;
  return { x: x * dpr, y: y * dpr };
}
function drawLine(x1, y1, x2, y2) {
  ctx.value.save();
  ctx.value.lineJoin = ctx.value.lineCap = "round";
  ctx.value.lineWidth = size.value * (window.devicePixelRatio || 1);
  ctx.value.globalCompositeOperation =
    mode.value == "erase" ? "destination-out" : "source-over";
  ctx.value.strokeStyle = mode.value == "erase" ? "rgba(0,0,0,1)" : color.value;
  ctx.value.beginPath();
  ctx.value.moveTo(x1, y1);
  ctx.value.lineTo(x2, y2);
  ctx.value.stroke();
  ctx.value.restore();
}
function down(e) {
  e.preventDefault();
  drawing.value = true;
  last.value = getPos(e);
  pushUndo();
}
function move(e) {
  if (!drawing.value) return;
  const p = getPos(e);
  drawLine(last.value.x, last.value.y, p.x, p.y);
  last.value = p;
}
const up = () => (drawing.value = false);

function resizeCanvas() {
  if (!canvas.value) return;

  const dpr = window.devicePixelRatio || 1;
  const w = canvas.value.clientWidth || 1;
  const h = canvas.value.clientHeight || 1;

  const tmp = document.createElement("canvas");
  tmp.width = canvas.value.width;
  tmp.height = canvas.value.height;
  const tmpCtx = tmp.getContext("2d");

  if (tmpCtx && canvas.value instanceof HTMLCanvasElement)
    tmpCtx.drawImage(canvas.value, 0, 0);

  canvas.value.width = w * dpr;
  canvas.value.height = h * dpr;

  ctx.value = canvas.value.getContext("2d");
  ctx.value.scale(dpr, dpr);

  if (tmpCtx) ctx.value.drawImage(tmp, 0, 0, tmp.width, tmp.height, 0, 0, w, h);
}

onMounted(init);
async function init() {
  await nextTick();
  ctx.value = canvas.value.getContext("2d");
  resizeCanvas();
  canvas.value.addEventListener("mousedown", down);
  canvas.value.addEventListener("mousemove", move);
  canvas.value.addEventListener("touchstart", down, { passive: false });
  canvas.value.addEventListener("touchmove", move, { passive: false });
  window.addEventListener("mouseup", up);
  window.addEventListener("touchend", up);
  const observer = new ResizeObserver(resizeCanvas);
  observer.observe(canvas.value);
  const keyHandler = (e) => {
    if (e.key == "z" && (e.ctrlKey || e.metaKey)) {
      e.preventDefault();
      undo();
    }
    if (e.key == "e") mode.value = "erase";
    if (e.key == "d") mode.value = "draw";
  };
  window.addEventListener("keydown", keyHandler);
}
</script>

<style scoped>
.drawing-board {
  display: flex;
  flex-direction: column;
  height: 100%;
}
.toolbar {
  position: absolute;
  display: flex;
  flex-wrap: wrap;
  align-items: center;
  padding: 8px;
}
button {
  padding: 6px 10px;
  border-radius: 6px;
  border: 1px solid rgba(0, 0, 0, 0.1);
}
canvas {
  display: block;
  width: 100%;
  height: 100%;
  touch-action: none;
}
</style>
