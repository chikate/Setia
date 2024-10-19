<script setup lang="ts">
import { useVuelidate } from '@vuelidate/core'
import { required, email } from '@vuelidate/validators'

const inputEmail = defineModel('inputEmail', { type: String, required: false, default: '' })
const recoveryState = ref<number>(0)
const v$ = useVuelidate({ inputEmail: { required, email } }, { inputEmail })

async function requestRecovery() {
  recoveryState.value = 1
  if (await v$.value.$validate()) {
    recoveryState.value = 2
    if (await authStore().recoverAccount(inputEmail.value)) {
      inputEmail.value = ''
      recoveryState.value = 3
    }
    setTimeout(() => (recoveryState.value = 0), 3000)
  }
}
</script>

<template>
  <div class="flex flex-column gap-3">
    <h2 class="m-0 p-0">Recover password</h2>
    <InputGroup>
      <InputGroupAddon v-if="inputEmail" :class="settingsStore().INPUT_CLASS">
        Email
      </InputGroupAddon>
      <InputText placeholder="Email" v-model="inputEmail" @keydown.enter="requestRecovery" />
    </InputGroup>

    <Button label="Send detail on email" class="button-gradient-effect" @click="requestRecovery" />
  </div>
</template>
