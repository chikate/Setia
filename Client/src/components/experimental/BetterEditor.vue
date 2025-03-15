<template>
  <div class="flex-column gap-2 align-items-center justify-content-center">
    <div class="pages-container">
      <div class="page">
        <div
          id="editableContent"
          class="contenteditable border-1 custom-shadow-1 border-round border-gray-200"
          style="height: 90vh; aspect-ratio: 1/1.414; text-align: justify"
          :style="{ padding: pagesPadding }"
          :contenteditable
          :oninput="checkOverflow"
          v-html="pages[activePage - 1].innerHTML"
        />
      </div>
    </div>
    <div class="flex-row align-items-center gap-2">
      <InputNumber
        :min="1"
        :max="pages?.length"
        :useGrouping="false"
        showButtons
        buttonLayout="horizontal"
        fluid
        @input="validatePageNumber"
        class="small"
        v-model="activePage"
      >
        <template #incrementbuttonicon>
          <span class="pi pi-chevron-right" />
        </template>
        <template #decrementbuttonicon>
          <span class="pi pi-chevron-left" />
        </template>
      </InputNumber>
      <label class="w-full font-light">
        out of <b>{{ pages?.length }}</b> pages
      </label>
    </div>
  </div>
</template>

<script lang="ts" setup>
import type { InputNumberInputEvent } from "primevue/inputnumber";

const pages = defineModel("pages", {
  type: Array<HTMLElement>,
  default: {},
});

const activePage = defineModel("activePage", {
  type: Number,
  default: 1,
});

const contenteditable = defineModel("contenteditable", {
  type: Boolean,
  default: false,
});

const pagesPadding = defineModel("pagesPadding", {
  type: String,
  default: "1rem 1rem 1rem 1rem",
});

function checkOverflow() {
  const editableElement = document.getElementById("editableContent");
  const pageHTML = editableElement?.closest(".page");
  if (
    editableElement &&
    pageHTML &&
    editableElement.scrollHeight > pageHTML.clientHeight
  )
    createNewPage();
}

function createNewPage() {
  const pagesContainer = document.querySelector(".pages-container");
  const newPage = document.createElement("div");
  newPage.classList.add("page");

  const newEditable = document.createElement("div");
  newEditable.setAttribute("contenteditable", "true");
  newEditable.classList.add("contenteditable");

  newPage.appendChild(newEditable);
  pagesContainer?.appendChild(newPage);

  newEditable.focus();
}

const validatePageNumber = (event: InputNumberInputEvent) => {
  const value = Number(event.value);
  event.value =
    isNaN(value) || value < 1 || value > (pages.value?.length ?? 0 > 0)
      ? 1
      : value;
};
</script>
