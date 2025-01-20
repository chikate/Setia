<template>
  <div class="w-full h-full" @mouseup="stopDragging" @mousemove="onDrag">
    <svg class="absolute w-full h-full">
      <line
        v-if="items.length > 1"
        :x1="items[0].position.x - boxSize / 2"
        :y1="items[0].position.y + boxSize / 2"
        :x2="items[1].position.x - boxSize / 2"
        :y2="items[1].position.y + boxSize / 2"
        stroke="black"
        stroke-width="2"
      />
    </svg>
    <div
      v-for="item in items"
      :key="item.name"
      :style="{
        left: item.position.x + 'px',
        top: item.position.y + 'px',
        position: 'absolute'
      }"
      class="box"
      @mousedown="startDragging(item, $event)"
    />
  </div>
</template>

<script setup>
const isDragging = ref(false)
const draggedItem = ref(null)
const items = ref([
  { name: 'test1', position: { x: 100, y: 100 } },
  { name: 'test2', position: { x: 200, y: 200 } }
])
const dragStart = ref({ x: 0, y: 0 })
const boxSize = 50 // size of the box to center the line

const startDragging = (item, event) => {
  isDragging.value = true
  draggedItem.value = item
  dragStart.value = {
    x: event.clientX - item.position.x,
    y: event.clientY - item.position.y
  }
}

const stopDragging = () => {
  isDragging.value = false
  draggedItem.value = null
}

const onDrag = (event) => {
  if (isDragging.value && draggedItem.value) {
    draggedItem.value.position.x = event.clientX - dragStart.value.x
    draggedItem.value.position.y = event.clientY - dragStart.value.y
  }
}
</script>

<style>
.box {
  width: 50px;
  height: 50px;
  background-color: lightblue;
  border: 1px solid blue;
  cursor: grab;
}
</style>
