<template>
  <div class="flex flex-column gap-3" style="width: 18rem">
    <lable class="font-bold">Login</lable>

    <div class="flex flex-column gap-2">
      <InputGroup>
        <InputGroupAddon v-if="inputUsername" style="min-width: 6rem" class="p-0 m-0">
          Username
        </InputGroupAddon>
        <InputText placeholder="Username" v-model="inputUsername" @keydown.enter="submitLogin" />
      </InputGroup>

      <InputGroup>
        <InputGroupAddon v-if="inputPassword" style="min-width: 6rem" class="p-0 m-0">
          Password
        </InputGroupAddon>
        <Password
          placeholder="Password"
          v-model="inputPassword"
          :feedback="false"
          @keydown.enter="submitLogin"
        />
      </InputGroup>
    </div>

    <div class="flex-row gap-2 align-items-center">
      <Checkbox v-model="staySignedIn" binary />
      <label>Stay signed in</label>
    </div>

    <div class="text-xs font-semibold text-white-alpha-600 text-center">
      By continuing you accept the
      <RouterLink to="/terms-of-service" class="font-medium">Terms of Service</RouterLink>,
      <RouterLink to="/privacy-policy" class="font-medium"
        >Privacy Policy and Cookie Policy</RouterLink
      >.
    </div>

    <Button label="Login" @click="submitLogin" :disabled="showLoginSpinner">
      <ProgressSpinner v-if="showLoginSpinner" style="width: 20px; height: 20px" strokeWidth="8" />
    </Button>

    <div class="flex-row justify-content-around">
      <RouterLink to="/recovery">Can't sign in?</RouterLink>
      <RouterLink to="/register">Create Account</RouterLink>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useToast } from 'primevue/usetoast'
import { TOAST_BASE_HP } from '@/constants'
const toast = useToast()
const showLoginSpinner = ref<boolean>(false)

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
const staySignedIn = defineModel('staySignedIn', {
  type: Boolean,
  required: false,
  default: false
})

async function submitLogin() {
  if (inputUsername.value.length < 6 || inputPassword.value.length < 6) return
  showLoginSpinner.value = true
  await useAuthStore()
    .login(inputUsername.value, inputPassword.value)
    .then((successful: Boolean) => {
      successful
        ? toast.add({
            severity: 'success',
            summary: 'Succesful login',
            detail: '',
            life: TOAST_BASE_HP,
            group: 'main'
          })
        : toast.add({
            severity: 'error',
            summary: 'Invalid account',
            detail: '',
            life: TOAST_BASE_HP,
            group: 'main'
          })
    })
  showLoginSpinner.value = false
}
</script>
