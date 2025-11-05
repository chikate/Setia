<template>
  <div class="mb-4 absolute z-5">
    <input type="file" @change="onFileChange" accept="image/*" />
  </div>

  <VueFlow
    v-model:nodes="nodes"
    v-model:edges="edges"
    :nodeTypes="nodeTypes"
    fit-view-on-init
    style="min-width: 50rem"
  >
    <Background />
    <MiniMap @click="fitView()" />
  </VueFlow>
</template>

<script setup lang="ts">
import { VueFlow, useVueFlow, Position, Handle } from "@vue-flow/core";
import { Background } from "@vue-flow/background";
import { MiniMap } from "@vue-flow/minimap";

defineOptions({
  name: "Referencer",
  icon: "🧩",
});

const { fitView } = useVueFlow();

const IfNode = {
  props: ["data"],
  setup(props) {
    return () =>
      h(
        "div",
        {
          class: "bg-white text-gray-500 border-round px-3 py-2",
        },
        [
          h(Handle, {
            id: "input",
            type: "target",
            position: Position.Left,
            style: { top: "30%" },
          }),
          h(Handle, {
            id: "input",
            type: "target",
            position: Position.Left,
            style: { top: "70%" },
          }),

          // Node label
          h("label", { class: "font-medium" }, props.data.label || "IF"),

          // Two outputs (Yes / No)
          h(Handle, {
            id: "yes",
            type: "source",
            position: Position.Right,
            style: { top: "30%" },
          }),
          h(Handle, {
            id: "no",
            type: "source",
            position: Position.Right,
            style: { top: "70%" },
          }),
        ]
      );
  },
};

const ImageNode = {
  props: ["data"],
  setup(props) {
    return () =>
      h(
        "div",
        { class: "bg-white border rounded p-2 flex flex-col items-center" },
        [
          props.data.src &&
            h("img", {
              src: props.data.src,
              class: "max-w-[120px] max-h-[80px] object-contain",
            }),
          h(Handle, {
            id: "input",
            type: "target",
            position: Position.Left,
          }),
          h(Handle, {
            id: "output",
            type: "source",
            position: Position.Right,
          }),
        ]
      );
  },
};

const nodeTypes = { ifNode: IfNode, imageNode: ImageNode };

function onFileChange(event: Event) {
  const target = event.target as HTMLInputElement;
  if (target.files?.length) {
    const file = target.files[0];
    const reader = new FileReader();
    reader.onload = () => {
      const newNode = {
        id: `${nodes.value.length}`,
        type: "imageNode",
        position: { x: 300, y: 100 },
        data: { label: file.name, src: reader.result },
      };
      nodes.value.push(newNode);
    };
    reader.readAsDataURL(file);
  }
}

const nodes = ref<any>([
  {
    id: "0",
    position: { x: 0, y: -50 },
    data: { label: "START" },
    type: "input",
    sourcePosition: Position.Right,
  },
  {
    id: "1",
    position: { x: 0, y: 50 },
    data: { label: "Condition" },
    type: "input",
    sourcePosition: Position.Right,
  },
  {
    id: "2",
    position: { x: 250, y: 0 },
    type: "ifNode",
    data: { label: "Check Condition" },
  },
  {
    id: "3",
    position: { x: 500, y: -50 },
    data: { label: "Yes Branch" },
    type: "output",
    targetPosition: Position.Left,
  },
  {
    id: "4",
    position: { x: 500, y: 50 },
    data: { label: "No Branch" },
    type: "output",
    targetPosition: Position.Left,
  },
]);

const edges = ref<any>([
  {
    id: "e0-1",
    source: "0",
    target: "2",
  },
  { id: "e1-3", source: "1", target: "2" },
  { id: "e1-2", source: "2", target: "3", sourceHandle: "yes" },
  { id: "e1-3", source: "2", target: "4", sourceHandle: "no" },
]);
</script>
