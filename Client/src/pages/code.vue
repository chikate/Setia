<template>
  <div class="flex flex-row">
    <div id="variables" class="flex flex-column">
      <AccordionTab header="Parameters">
        <SimpleParametersComponent
          :parameters
          @click="selectedParam = $event"
        />
      </AccordionTab>
    </div>
    <div class="flex flex-column border-left-1 bg-gray-700">
      <div class="flex flex-row p-2 overflow-auto">
        <div class="flex flex-column">
          <span
            style="min-width: 20px"
            class="text-right"
            v-for="n in lineCount"
            :key="n"
          >
            {{ n }}
          </span>
        </div>
        <div
          ref="contentEditor"
          class=""
          contenteditable
          @input="onInput"
          @keydown="handleKeydown"
        />
      </div>
      <Terminal prompt=">" aria-label="PrimeVue Terminal Service" />
    </div>
  </div>
</template>

<script setup lang="ts">
defineOptions({
  name: "Code",
  icon: "🧬",
});

const code = ref(`function hello() {
  console.log("Hello, in Vue!");
}`);

const lineCount = ref(1);
const parameters = ref([]);
const selectedParam = ref();
const contentEditor = ref<HTMLElement>();

// Autocomplete Data
const suggestions = [
  "apple",
  "banana",
  "carrot",
  "date",
  "eggplant",
  "fig",
  "grape",
];
const filteredSuggestions = ref<string[]>([]);
const activeSuggestion = ref(0);
const showSuggestions = ref(false);
const caretPosition = ref({ top: 0, left: 0 });

// Function to handle text input and update line count
async function onInput() {
  const content = contentEditor.value?.innerText;

  lineCount.value = content?.split(/\n\n|\n/).length ?? 1;

  const triggerMatch = content?.match(/@(\w*)$/); // Matches "@word" at the end
  if (triggerMatch) {
    const query = triggerMatch[1];
    filteredSuggestions.value = suggestions.filter((suggestion) =>
      suggestion.toLowerCase().startsWith(query.toLowerCase())
    );
    showSuggestions.value = filteredSuggestions.value.length > 0;
    activeSuggestion.value = 0;
    updateCaretPosition();
  } else {
    showSuggestions.value = false;
  }
}

// Function to get the caret position for suggestion dropdown
const updateCaretPosition = () => {
  const range = window.getSelection()?.getRangeAt(0);
  const rect = range?.getBoundingClientRect();
  if (rect) {
    caretPosition.value = {
      top: rect.bottom + window.scrollY,
      left: rect.left + window.scrollX,
    };
  }
};

// Function to handle arrow keys and enter for selecting suggestions
const handleKeydown = (event: KeyboardEvent) => {
  if (showSuggestions.value) {
    if (event.key == "ArrowDown") {
      activeSuggestion.value =
        (activeSuggestion.value + 1) % filteredSuggestions.value.length;
      event.preventDefault();
    } else if (event.key == "ArrowUp") {
      activeSuggestion.value =
        (activeSuggestion.value - 1 + filteredSuggestions.value.length) %
        filteredSuggestions.value.length;
      event.preventDefault();
    } else if (event.key == "Enter") {
      event.preventDefault();
      selectSuggestion(filteredSuggestions.value[activeSuggestion.value]);
    } else if (event.key == "Escape") {
      showSuggestions.value = false;
    }
  }
};

// Function to insert selected suggestion
const selectSuggestion = (suggestion: string) => {
  // const content = contentEditor.value?.innerText
  // const newContent = content?.replace(/ @\w*$/, suggestion)
  // contentEditor.value.innerText = newContent
  showSuggestions.value = false;

  // Set caret position to end of new text
  nextTick(() => {
    const range = document.createRange();
    // range.selectNodeContents(contentEditor.value)
    range.collapse(false);
    const selection = window.getSelection();
    selection?.removeAllRanges();
    selection?.addRange(range);
  });
};
</script>
