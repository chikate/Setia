<template>
  <div class="flex flex-column" style="min-width: 50rem">
    <div class="flex flex-row align-items-center">
      <Button
        label="Save / See the result in console log"
        class="button-gradient-effect"
        :loading="saving"
        @click="saveAsImage"
      />
      <SelectButton :options="['Table']" v-model="viewMode" />
    </div>

    <DataTable
      v-if="viewMode == 'Table'"
      :value="tableData"
      striped-rows
      class="border-round overflow-hidden"
    >
      <Column header="Rank" style="width: 10px" class="text-center">
        <template #body="{ index }">
          {{ index + 1 }}
        </template>
      </Column>
      <Column header="Content" class="text-center">
        <template #body="{ data }">
          <img
            height="64"
            width="64"
            style="object-fit: cover"
            :src="data.content"
          />
          {{ data.name }}
        </template>
      </Column>
      <Column header="Tier" class="text-center font-bold">
        <template #body="{ data }">
          <div :style="{ color: data.color }">
            {{ data.tier ?? "-" }}
          </div>
        </template>
      </Column>
      <Column class="" />
    </DataTable>

    <div
      v-else
      id="tierList"
      class="flex flex-column border-round overflow-hidden"
    >
      <div
        v-for="(tierRow, tierRowIndex) in tierListData"
        :id="'tierRow-' + tierRowIndex"
        :style="{
          minHeight: tierListDimension + 'px',
          maxHeight: tierListDimension + 'px',
        }"
        class="flex flex-row"
        :key="tierRowIndex"
      >
        <Textarea
          v-if="tierRow.tier != undefined"
          v-model="tierRow.tier"
          auto-resize
          class="tier text-xl flex flex-column text-center"
          :style="{
            backgroundColor: tierRow.color,
            maxWidth: tierListDimension + 'px',
            maxHeight: tierListDimension + 'px',
            'border-radius': 0,
          }"
        />
        <div
          :id="'droptarget-' + tierRowIndex"
          class="droptarget flex-wrap"
          @dragover.prevent
          @drop="drop"
        >
          <img
            v-for="tierItem in tierRow.content"
            :src="tierItem"
            :key="tierItem"
            :height="tierListDimension"
            class="cursor-grab"
            @dragstart="handleDrag($event, 'start')"
            @dragend="handleDrag($event, 'end')"
            :style="{
              maxHeight: tierListDimension + 'px',
            }"
          />
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import * as htmlToImage from "html-to-image";

const tierListDimension = 128;
const tierListData = defineModel("tierListData", {
  type: Array<{
    tier: string;
    color: string;
    content?: string[];
  }>,
  default: [
    {
      tier: "S",
      color: "#ff7f7e",
    },
    { tier: "A", color: "#ffbf7f" },
    { tier: "B", color: "#ffdf80" },
    { tier: "C", color: "#feff7f" },
    { tier: "D", color: "#beff7f" },
    { tier: "F", color: "#7eff80" },
    {
      content: [
        "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRmDwycVzCsWaZx5AaCTeqz6e8qLbt8UaQz7g&s",
        "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT6WcdjgQXfUoz7afVYi53uNYsamoIvoWZpDg&s",
        "https://media.post.rvohealth.io/wp-content/uploads/2020/08/banana-pink-background-thumb-1-732x549.jpg",
      ],
    },
  ],
});

const tableData = computed(() =>
  tierListData.value?.flatMap(
    (t) =>
      t.content?.map((img) => ({
        tier: t.tier,
        color: t.color,
        name: "",
        content: img,
      })) || []
  )
);

const saving = ref(false);
const viewMode = ref();

async function saveAsImage() {
  saving.value = true;
  await htmlToImage
    .toPng(document.getElementById("tierList") as HTMLElement)
    .then(downloadInBrowser);
  saving.value = false;
}

const draggedElement = ref<HTMLElement | null>(null);

function handleDrag(event: DragEvent, type: "start" | "end") {
  if (event.target instanceof HTMLImageElement)
    if (type == "start") {
      draggedElement.value = event.target;
      event.target.classList.add("opacity-05");
      event.dataTransfer?.setData("text/plain", event.target.src);
    } else if (type == "end") event.target.classList.remove("opacity-05");
}

function drop(ev: DragEvent) {
  ev.preventDefault();

  if (!(ev.target instanceof HTMLElement)) return;
  if (!draggedElement.value) return;

  const imgSrc = ev.dataTransfer?.getData("text/plain");
  if (!imgSrc) return;

  const img = document.createElement("img");
  img.src = imgSrc;
  img.height = tierListDimension;
  img.draggable = true;
  img.classList.add("cursor-grab");
  img.addEventListener("dragstart", (e) => handleDrag(e, "start"));
  img.addEventListener("dragend", (e) => handleDrag(e, "end"));

  ev.target.appendChild(img);

  draggedElement.value.remove();
  draggedElement.value = null;
}
</script>
