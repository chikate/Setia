<script setup lang="ts">
import type { ToastMessageOptions } from "primevue/toast";
import { useToast } from "primevue/usetoast";
const toast = useToast();

export interface IParameter {
  name?: string;
  value?: any;
  type?: string;
}

defineEmits(["paramDblClick"]);

const parameters = defineModel("parameters", {
  type: Array<IParameter>,
  required: true,
  default: [],
});

async function dragStart(ev: any) {
  ev.dataTransfer.setData("text", "@" + ev.target.innerHTML);
}

function handleAddParameter(event: KeyboardEvent) {
  const target = event.target as HTMLInputElement;
  const inputValue = target.value;
  const toastMessage: ToastMessageOptions = {
    severity: "info",
    summary: "Adding parameter",
  };

  if (
    !target.value ||
    target.value == "" ||
    target.value == undefined ||
    target.value == null
  )
    toastMessage.detail = "Invalid parameter!";
  else {
    if (
      !parameters.value?.find((elem: IParameter) => elem.name == inputValue)
    ) {
      parameters.value?.push({ name: inputValue, type: "value" });
      toastMessage.severity = "success";
      toastMessage.detail = "Success";
    } else {
      toastMessage.detail = "Parameter already exists!";
    }
  }

  toast.add(toastMessage);
  target.value = "";
}

function handleDeleteParameter(index: number) {
  parameters.value?.splice(index, 1); // Remove the parameter at the specified index
  toast.add({
    severity: "info",
    summary: "Deleted",
    detail: "Parameter removed successfully!",
  });
}
</script>

<template>
  <div class="flex flex-column">
    <InputText
      placeholder="Add parameter"
      @keydown.enter="handleAddParameter"
    />
    <div
      class="parameter flex flex-row gap-2 border-bottom-1 border-gray-200 justify-content-between align-items-center cursor-pointer"
      draggable
      v-for="(item, item_index) in parameters"
      :key="item_index"
    >
      <!-- <Dropdown
        class="border-0 shadow-none"
        input-class="p-0"
        v-model="item.type"
        :options="[
          { name: 'Table', icon: 'pi pi-table', type: 'list' },
          { name: 'Value', icon: 'pi pi-hashtag', type: 'value' },
          { name: 'Image', icon: 'pi pi-image', type: 'file' },
        ]"
        option-label="name"
        option-value="type"
      >
        <template #option="{ option }">
          <div class="flex gap-3">
            <i
              :class="option.icon"
              class="pi text-center"
              style="min-width: 20px"
            />
            {{ option.name }}
          </div>
        </template>
      </Dropdown> -->
      <InputText v-model="item.value" />
      <label
        @dblclick="$emit('paramDblClick', $event)"
        class="w-full text-right cursor-pointer"
        :draggable="true"
        :ondragstart="dragStart"
      >
        {{ item.name }}
      </label>
      <Button
        icon="pi pi-ellipsis-v"
        text
        @click="handleDeleteParameter(item_index)"
      />
    </div>
  </div>
</template>

<style scoped>
:deep().p-dropdown-trigger {
  display: none !important;
}
</style>
