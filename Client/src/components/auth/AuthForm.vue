<template>
  <!-- v-if="state < 3" -->
  <div class="flex flex-column gap-2">
    <InputGroup v-for="(val, key) in Object.keys(input)" :key>
      <InlineMessage
        v-if="v$[key]?.$error"
        severity="error"
        class="absolute -translate-x-100 -ml-5"
      >
        {{ v$[key].$errors[0].$message }}
      </InlineMessage>

      <InputGroupAddon v-if="input[val]" :class="INPUT_CLASS">
        {{ capitalizeString(val) }}
      </InputGroupAddon>

      <component
        :is="val?.includes('password') ? Password : InputText"
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
  </div>
  <!-- <div v-else class="text-center">Success</div> -->
</template>

<script setup lang="ts">
import { InputText, Password } from "primevue";
import { useVuelidate } from "@vuelidate/core";
import { required, email, minLength, sameAs } from "@vuelidate/validators";

const emit = defineEmits(["successful"]);
const input = defineModel<any>("input");
const props = defineProps(["submitFunction"]);
const state = ref(0);

// Generate rules only for keys present in input
const rules = Object.fromEntries(
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
);

const v$ = useVuelidate(rules, input);

async function submit() {
  state.value = 1;
  if (await v$.value.$validate()) {
    state.value = 2;
    await props.submitFunction(input.value).then((success) => {
      success ? emit("successful") : (state.value = 0);
      state.value = 3;
    });
  }
}
</script>
