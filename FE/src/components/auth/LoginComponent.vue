<script setup lang="ts">
import type { IAuthenticationDTO } from '@/global/interfaces'
import { INPUT_CLASS } from '@/global/constants'

const loginState = ref(localStorage.getItem('token') ? 3 : 0)

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

async function submitLogin() {
  loginState.value = 1
  await authService
    .login({
      username: inputUsername.value,
      password: inputPassword.value
    } as IAuthenticationDTO)
    .then((successful: Boolean) => {
      if (successful) {
        inputUsername.value = ''
        loginState.value = 3
      } else loginState.value = 0
      inputPassword.value = ''
    })
}
</script>

<template>
  <div v-if="loginState < 3" class="flex flex-column gap-3 border-round w-3 align-self-start">
    <h2 class="m-0 p-0">Login</h2>

    <InputGroup>
      <InputGroupAddon v-if="inputUsername" :class="INPUT_CLASS"> Username </InputGroupAddon>
      <InputText placeholder="Username" v-model="inputUsername" @keydown.enter="submitLogin" />
    </InputGroup>

    <InputGroup>
      <InputGroupAddon v-if="inputPassword" :class="INPUT_CLASS"> Password </InputGroupAddon>
      <Password
        placeholder="Password"
        v-model="inputPassword"
        @keydown.enter="submitLogin"
        :feedback="false"
      />
    </InputGroup>

    <div
      class="flex-wrap justify-content-center text-xs font-semibold text-white-alpha-600 text-center"
    >
      By continuing you accept the
      <RouterLink to="/terms-of-service" class="font-medium">Terms of Service</RouterLink>,
      <div>
        <RouterLink to="/privacy-policy" class="font-medium">
          Privacy Policy and Cookie Policy </RouterLink
        >.
      </div>
    </div>

    <Button
      label="Login"
      class="button-gradient-effect"
      :loading="loginState > 0"
      @click="submitLogin"
    />

    <div class="flex-row justify-content-around">
      <RouterLink to="/recovery">Can't sign in?</RouterLink>
      <RouterLink to="/register">Create Account</RouterLink>
    </div>
  </div>
  <div v-else class="flex-column justify-content-between">
    Profile

    <Button label="Log Out" class="button-gradient-effect" @click="authService.logOut()" />
  </div>
</template>

<style scoped>
:deep().p-button:hover .p-button-label {
  font-size: 1.5rem;
}
</style>
