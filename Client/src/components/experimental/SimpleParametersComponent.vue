<script setup lang="ts">
import type { ToastMessageOptions } from "primevue/toast";
import { useToast } from "primevue/usetoast";
const toast = useToast();

defineEmits(["paramDblClick"]);

export interface IParameter {
  guid: string;
  name: string;
}
export interface IParameter {
  guid: string;
  value: string;
}

const selectedParam = ref<IParameter>({} as IParameter);
const parameters = defineModel("parameters", {
  type: Array<IParameter>,
  required: true,
  default: [],
});

function addParameter(name: string) {
  const toastMessage = {
    severity: "info",
    summary: "Adding parameter",
  } as ToastMessageOptions;

  if (!parameters.value?.find((elem: IParameter) => elem.name == name)) {
    parameters.value?.push({ guid: generateGuid(), name });
    toastMessage.severity = "success";
    toastMessage.detail = "Success";
  } else {
    toastMessage.detail = "Parameter already exists!";
  }

  toast.add(toastMessage);
}

function deleteParameter(index: number) {
  parameters.value?.splice(index, 1); // Remove the parameter at the specified index
  toast.add({
    severity: "info",
    summary: "Deleted",
    detail: "Parameter removed successfully!",
  });
}

const dragStart = (ev: any) =>
  ev.dataTransfer.setData("text", "@" + ev.target.innerHTML);
</script>

<template>
  <div id="parameters_panel" class="flex flex-column gap-2">
    <InputText
      placeholder="Add parameter"
      @keydown.enter="addParameter(($event.target as HTMLInputElement).value)"
    />
    <div class="flex flex-column">
      <div
        class="flex flex-row justify-content-between align-params-center cursor-pointer"
        draggable
        :ondragstart="dragStart"
        v-for="(param, param_index) in parameters"
        :key="param_index"
        @dblclick="$emit('paramDblClick', param)"
        @click="selectedParam = param"
      >
        <InputText
          class="param"
          v-model="param.name"
          readonly
          @dblclick="$event.target.readOnly = false"
          @blur="$event.target.readOnly = true"
          style="min-width: 100px; width: 100px; max-width: 100px"
        />
        <Textarea
          class="param_value"
          v-model="param.value"
          readonly
          @dblclick="$event.target.readOnly = false"
          @blur="$event.target.readOnly = true"
          :rows="1"
        />
      </div>
    </div>
  </div>
</template>

<style scoped>
.param:read-only {
  border: none;
  box-shadow: none;
}
.param_value:read-only {
  flex-grow: 1;
  min-height: 100%;
}
</style>
