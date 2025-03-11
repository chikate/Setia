<script setup lang="ts">
import * as htmlToImage from "html-to-image";

const tierListDimension = 128;
const tierData = defineModel("tierData", {
  type: Array<{
    tier: string;
    color: string;
    content?: string[];
  }>,
  default: [
    {
      tier: "S",
      color: "#ff7f7e",
      content: [
        "https://letsenhance.io/static/8f5e523ee6b2479e26ecc91b9c25261e/1015f/MainAfter.jpg",
      ],
    },
    { tier: "A", color: "#ffbf7f" },
    { tier: "B", color: "#ffdf80" },
    { tier: "C", color: "#feff7f" },
    { tier: "D", color: "#beff7f" },
    { tier: "F", color: "#7eff80" },
    {
      content: [
        "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRmDwycVzCsWaZx5AaCTeqz6e8qLbt8UaQz7g&s",
        "https://images.pexels.com/photos/2662116/pexels-photo-2662116.jpeg?cs=srgb&dl=pexels-jaime-reimer-1376930-2662116.jpg&fm=jpg",
        "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT6WcdjgQXfUoz7afVYi53uNYsamoIvoWZpDg&s",
        //'https://upload.wikimedia.org/wikipedia/commons/c/c9/Napa_Valley.jpg',
        "https://images.unsplash.com/photo-1446308386271-523272773b92?fm=jpg&q=60&w=3000&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
        "https://media.post.rvohealth.io/wp-content/uploads/2020/08/banana-pink-background-thumb-1-732x549.jpg",
      ],
    },
  ],
});

const tableData = computed(() => {
  if (!tierData.value) return [];
  return tierData.value.flatMap((t) => {
    if (!t.content) return [];
    return t.content.map((img) => ({
      tier: t.tier,
      color: t.color,
      name: "",
      content: img,
    }));
  });
});

const saving = ref(false);
const mode = ref(0);

async function saveAsImage() {
  saving.value = true;
  await htmlToImage
    .toPng(document.getElementById("tierList") as HTMLElement)
    .then(downloadInBrowser);
  saving.value = false;
}

const draggedElement = ref<HTMLElement | null>(null);

function handleDrag(event: DragEvent, type: "start" | "end") {
  if (event.target instanceof HTMLElement) {
    if (type === "start") {
      draggedElement.value = event.target;
      event.target.classList.toggle("opacity-05", true);
    } else {
      event.target.classList.toggle("opacity-05", false);
    }
  }
}

function drop(ev: DragEvent) {
  ev.preventDefault();

  if (ev.target instanceof HTMLElement) {
    const img = document.createElement("img");

    // Assuming you're transferring an image URL (or similar text data)
    const imgSrc = ev.dataTransfer?.getData("text/plain");
    if (imgSrc) {
      img.src = imgSrc; // Set the source of the image
      img.height = tierListDimension; // Set the dimension of the image
      ev.target.appendChild(img); // Append image to the target element
    }

    // Remove the dragged element from the DOM
    if (draggedElement.value) {
      draggedElement.value.remove();
      draggedElement.value = null; // Clear the reference after removing the element
    }
  }
}
</script>

<template>
  <div class="flex-column gap-2">
    <div class="flex-row gap-2 align-items-center">
      <Button
        label="Save / See the result in console log"
        class="button-gradient-effect"
        :loading="saving"
        @click="saveAsImage"
      />
      <SelectButton
        :options="[
          { label: 'Tier List', value: 0 },
          { label: 'Leaderboard', value: 1 },
        ]"
        option-label="label"
        option-value="value"
        v-model="mode"
      />
    </div>

    <div v-if="mode == 0" id="tierList" class="flex-column border-round">
      <div
        v-for="(tierRow, tierRowIndex) in tierData"
        :id="'tierRow-' + tierRowIndex"
        :style="{
          minHeight: tierListDimension + 'px',
          maxHeight: tierListDimension + 'px',
        }"
        class="flex-row"
        :key="tierRowIndex"
      >
        <Textarea
          v-if="tierRow.tier != undefined"
          v-model="tierRow.tier"
          auto-resize
          class="tier text-xl flex-column text-center"
          :style="{
            backgroundColor: tierRow.color,
            maxWidth: tierListDimension + 'px',
            maxHeight: tierListDimension + 'px',
          }"
        />
        <div
          :id="'droptarget-' + tierRowIndex"
          class="droptarget flex-wrap w-full"
          :class="{ 'bg-primary': tierRow.tier != undefined }"
          @dragover.prevent
          @drop="drop"
          style="display: flex; flex-wrap: wrap; justify-content: space-between"
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

    <DataTable v-else-if="mode == 1" :value="tableData" striped-rows>
      <Column header="Rank" style="width: 10px" class="text-center">
        <template #body="{ index }">
          {{ index + 1 }}
        </template>
      </Column>
      <Column header="Content">
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
      <Column header="Tier" style="width: 10px" class="text-center font-bold">
        <template #body="{ data }">
          <div :style="{ color: data.color }">
            {{ data.tier ?? "🤷‍♂️" }}
          </div>
        </template>
      </Column>
      <Column class="w-full" />
    </DataTable>
  </div>
</template>

<style scoped>
:deep().p-inputtext {
  border-style: none;
  border-radius: 0;
  box-shadow: none;
  vertical-align: middle;
}
:deep().p-datatable .p-datatable-tbody > tr > td {
  padding: 1px;
}
</style>
