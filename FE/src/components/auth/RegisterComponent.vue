<script setup lang="ts">
import { useVuelidate } from '@vuelidate/core'
import { required, email, minLength, sameAs } from '@vuelidate/validators'

const inputEmail = defineModel('inputEmail', {
  type: String,
  required: false,
  default: ''
})
const inputUsername = defineModel('inputUsername', {
  type: String,
  required: false,
  default: ''
})
const inputPassword = defineModel('inputPassword', {
  type: String,
  required: false,
  default: ''
})
const inputRepeatPassword = defineModel('inputRepeatPassword', {
  type: String,
  required: false,
  default: ''
})
const registerState = ref<number>(0)
const v$ = useVuelidate(
  {
    inputEmail: { required, email },
    inputUsername: { required, minLength: minLength(6) },
    inputPassword: { required, minLength: minLength(6) },
    inputRepeatPassword: { sameAs: sameAs(inputPassword) }
  },
  { inputEmail, inputUsername, inputPassword, inputRepeatPassword }
)

async function submitRegistration() {
  registerState.value = 1
  if (await v$.value.$validate()) {
    registerState.value = 2
    if (await authStore().register(inputEmail.value, inputUsername.value, inputPassword.value)) {
      inputEmail.value = ''
      inputUsername.value = ''
      inputPassword.value = ''
      inputRepeatPassword.value = ''

      registerState.value = 3
    }
    setTimeout(() => (registerState.value = 0), 3000)
  }
}
</script>

<template>
  <div v-if="registerState < 3" class="flex flex-column gap-3 w-4 border-round align-self-start">
    <h2 class="m-0 p-0">Register</h2>

    <InputGroup>
      <InlineMessage
        severity="info"
        v-if="v$.inputEmail.$error"
        class="absolute -translate-x-100 -ml-5"
      >
        {{ v$.inputEmail.$errors[0].$message }}
      </InlineMessage>
      <InputGroupAddon v-if="inputEmail" :class="settingsStore().INPUT_CLASS">
        Email
      </InputGroupAddon>
      <InputText
        v-model="inputEmail"
        id="email"
        placeholder="Email"
        :class="inputEmail ? '' : registerState > 0 ? 'p-invalid' : ''"
      />
    </InputGroup>

    <InputGroup>
      <InlineMessage
        severity="info"
        v-if="v$.inputUsername.$error"
        class="absolute -translate-x-100 -ml-5"
      >
        {{ v$.inputUsername.$errors[0].$message }}
      </InlineMessage>
      <InputGroupAddon v-if="inputUsername" :class="settingsStore().INPUT_CLASS">
        Username
      </InputGroupAddon>
      <InputText
        v-model="inputUsername"
        id="username"
        placeholder="Username"
        :class="inputUsername ? '' : registerState > 0 ? 'p-invalid' : ''"
      />
    </InputGroup>

    <InputGroup>
      <InlineMessage
        severity="info"
        v-if="v$.inputPassword.$error"
        class="absolute -translate-x-100 -ml-5"
      >
        {{ v$.inputPassword.$errors[0].$message }}
      </InlineMessage>
      <InputGroupAddon v-if="inputPassword" :class="settingsStore().INPUT_CLASS">
        Password
      </InputGroupAddon>
      <Password
        v-model="inputPassword"
        toggleMask
        id="password"
        placeholder="Password"
        :class="inputPassword ? '' : registerState > 0 ? 'p-invalid' : ''"
      >
        <template #footer>
          <Divider />
          <div class="flex-wrap gap-2">
            <Chip class="text-white bg-red-300">one lowercase</Chip>
            <Chip class="text-white bg-red-300">one uppercase</Chip>
            <Chip class="text-white bg-red-300">one numeric</Chip>
            <Chip class="text-white bg-red-300">8 characters</Chip>
          </div>
        </template>
      </Password>
    </InputGroup>

    <InputGroup>
      <InlineMessage
        severity="info"
        v-if="v$.inputRepeatPassword.$error"
        class="absolute -translate-x-100 -ml-5"
      >
        {{ v$.inputPassword.$errors[0].$message }}
      </InlineMessage>
      <InputGroupAddon v-if="inputRepeatPassword" :class="settingsStore().INPUT_CLASS">
        Repeat password
      </InputGroupAddon>
      <Password
        v-model="inputRepeatPassword"
        toggleMask
        :feedback="false"
        id="repeatPassword"
        placeholder="Repeat password"
        :class="inputRepeatPassword ? '' : registerState > 0 ? 'p-invalid' : ''"
      />
    </InputGroup>

    <Button
      label="Register"
      class="button-gradient-effect"
      :loading="registerState > 1"
      @click="submitRegistration"
    />
  </div>
  <div v-else class="align-items-center h-full">Welcome</div>
</template>

<style scoped>
:deep(.p-button:hover .p-button-label) {
  font-size: 1.5rem;
}
</style>
