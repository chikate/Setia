<script setup lang="ts">
import { useToast } from 'primevue/usetoast'
import { TOAST_BASE_HP } from '@/constants'
import { IAuthenticationDTO } from '@/interfaces'

const toast = useToast()

const registerClicked = ref<boolean>(false)

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
const inputWidth = defineModel('inputWidth', {
  type: Number,
  required: false,
  default: 110
})

async function registerClickedHandler() {
  registerClicked.value = true
  if (!inputEmail.value) {
    toast.add({
      severity: 'info',
      summary: 'Info',
      detail: 'Please enter a valid Email',
      life: TOAST_BASE_HP,
      group: 'main'
    })
    return
  }
  if (inputUsername.value.length < 6) {
    toast.add({
      severity: 'info',
      summary: 'Info',
      detail: 'Please enter a valid Username',
      life: TOAST_BASE_HP,
      group: 'main'
    })
    return
  }
  if (inputPassword.value.length < 6) {
    toast.add({
      severity: 'info',
      summary: 'Info',
      detail: 'Enter a longer Password',
      life: TOAST_BASE_HP,
      group: 'main'
    })
    return
  }
  if (inputPassword.value !== inputRepeatPassword.value) {
    toast.add({
      severity: 'info',
      summary: 'Info',
      detail: 'Please make sure the Repeat Password is the same as Password',
      life: TOAST_BASE_HP,
      group: 'main'
    })
    return
  }
  await useAuthStore()
    .register(inputEmail.value, inputUsername.value, inputPassword.value)
    .then((succesful: Boolean) => {
      succesful
        ? useAuthStore()
            .login({
              username: inputUsername.value,
              password: inputPassword.value
            } as IAuthenticationDTO)
            .then(() =>
              toast.add({
                severity: 'success',
                summary: 'Success',
                detail: 'Account created!',
                life: TOAST_BASE_HP,
                group: 'main'
              })
            )
        : toast.add({
            severity: 'error',
            summary: 'ERROR',
            detail: 'Something is wrong',
            life: TOAST_BASE_HP,
            group: 'main'
          })
    })
}
</script>

<template>
  <div class="flex flex-column gap-3 w-3">
    <h2>Register</h2>

    <InputGroup>
      <InlineMessage severity="info" v-if="!inputEmail" class="absolute -translate-x-100">
        {{ 'Email is required' }}
      </InlineMessage>
      <InputGroupAddon
        v-if="inputEmail"
        :style="`width: ${inputWidth}px; min-width: ${inputWidth}px`"
        class="m-0 p-2 px-3 justify-content-start"
      >
        Email
      </InputGroupAddon>
      <InputText
        v-model="inputEmail"
        id="email"
        placeholder="Email"
        :class="inputEmail ? '' : registerClicked ? 'p-invalid' : ''"
      />
    </InputGroup>

    <InputGroup>
      <InlineMessage severity="info" v-if="!inputUsername" class="absolute -translate-x-100">
        {{ 'Username is required' }}
      </InlineMessage>
      <InputGroupAddon
        v-if="inputUsername"
        :style="`width: ${inputWidth}px; min-width: ${inputWidth}px`"
        class="m-0 p-2 px-3 justify-content-start"
      >
        Username
      </InputGroupAddon>
      <InputText
        v-model="inputUsername"
        id="username"
        placeholder="Username"
        :class="inputUsername ? '' : registerClicked ? 'p-invalid' : ''"
      />
    </InputGroup>

    <InputGroup>
      <InlineMessage severity="info" v-if="!inputPassword" class="absolute -translate-x-100">
        {{ 'Password is required' }}
      </InlineMessage>
      <InputGroupAddon
        v-if="inputPassword"
        :style="`width: ${inputWidth}px; min-width: ${inputWidth}px`"
        class="m-0 p-2 px-3 justify-content-start"
      >
        Password
      </InputGroupAddon>
      <Password
        v-model="inputPassword"
        toggleMask
        id="password"
        placeholder="Password"
        :class="inputPassword ? '' : registerClicked ? 'p-invalid' : ''"
      >
        <template #footer>
          <Divider class="my-3 p-0" />
          <p class="m-0 mb-2 p-0">Suggestions</p>
          <ul class="px-4 m-0">
            <li>At least one lowercase</li>
            <li>At least one uppercase</li>
            <li>At least one numeric</li>
            <li>Minimum 8 characters</li>
          </ul>
        </template>
      </Password>
    </InputGroup>

    <InputGroup>
      <InlineMessage severity="info" v-if="!inputRepeatPassword" class="absolute -translate-x-100">
        {{ 'Repeat Password is required' }}
      </InlineMessage>
      <InputGroupAddon
        v-if="inputRepeatPassword"
        :style="`width: ${inputWidth}px; min-width: ${inputWidth}px`"
        class="m-0 p-2 px-3 justify-content-start"
      >
        Repeat password
      </InputGroupAddon>
      <Password
        v-model="inputRepeatPassword"
        toggleMask
        :feedback="false"
        id="repeatPassword"
        placeholder="Repeat password"
        :class="inputRepeatPassword ? '' : registerClicked ? 'p-invalid' : ''"
      />
    </InputGroup>

    <Button
      class="button-gradient-effect mt-1 align-self-start"
      label="Register"
      @click="registerClickedHandler"
    />
  </div>
</template>
