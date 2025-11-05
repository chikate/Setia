<template>
  <Form class="flex flex-column gap-2">
    <InputGroup v-for="(val, key) in Object.keys(input)" :key>
      <InlineMessage
        v-if="v$[key]?.$error"
        severity="error"
        class="absolute -translate-x-100 -ml-5"
      >
        {{ v$[key].$errors[0].$message }}
      </InlineMessage>

      <InputGroupAddon
        v-if="input[val]"
        class="m-0 p-2 px-3 justify-content-start"
        style="min-width: 130px; width: 130px; max-width: 130px"
      >
        {{ capitalizeString(val) }}
      </InputGroupAddon>

      <component
        :is="val?.includes('password') ? Password : InputText"
        :inputProps="{
          autocomplete: val?.includes('password')
            ? ['current-password', 'new-password']
            : val,
        }"
        v-model="input[val]"
        :placeholder="capitalizeString(val)"
        :class="{ 'p-invalid': state > 0 && v$[key]?.$invalid }"
        :feedback="false"
        @keydown.enter="submit"
      />
    </InputGroup>

    <Button
      label="Submit"
      class="button-gradient-effect font-bold"
      :loading="state > 0"
      @click="submit"
    />
  </Form>
</template>

<script setup lang="ts">
import { InputText, Password } from "primevue";
import { useVuelidate } from "@vuelidate/core";
import { required, email, minLength, sameAs } from "@vuelidate/validators";
import { app } from "@/main";

const emit = defineEmits(["successful"]);
const props = defineProps(["submitFunction"]);
const input = defineModel<any>("input");

const v$ = useVuelidate(
  Object.fromEntries(
    Object.keys(input.value).map((key) => [
      key,
      {
        email: { required, email },
        username: { required, minLength: minLength(6) },
        password: { required, minLength: minLength(6) },
        repeatPassword: {
          required,
          sameAsPassword: sameAs(() => input.value.password),
        },
      }[key],
    ])
  ),
  input
);

const state = ref(0);
async function submit() {
  if (state.value > 0) return;
  state.value = 1;
  try {
    state.value = 2;
    if (await v$.value.$validate()) {
      state.value = 3;
      const response = await props.submitFunction();
      state.value = 4;
      emit("successful", response);
      state.value = 5;
      app.config.globalProperties.$toast.add({
        summary: "Server",
        detail: "Request successful ✅",
        severity: "success",
      });
      state.value = 6;
    }
    state.value = 7;
  } catch (ex) {
    app.config.globalProperties.$toast.add({
      summary: "Server",
      detail: ex,
      severity: "error",
    });
    state.value = 0;
  }
  state.value = 0;
}
</script>

<style scoped>
* {
  gap: 0;
}
</style>
