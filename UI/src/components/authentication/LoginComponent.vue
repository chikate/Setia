<template>
  <div class="flex flex-column gap-2">
    <lable class="font-bold mb-1">Login</lable>

    <span class="p-float-label w-full">
      <InputText v-model="inputUsername" class="w-full" />
      <label>Username</label>
    </span>

    <span class="p-float-label w-full">
      <Password v-model="inputPassword" :feedback="false" toggleMask class="w-full" />
      <label>Password</label>
    </span>

    <span class="flex flex-row gap-2 align-items-center">
      <Checkbox v-model="staySignedIn" :binary="true" />
      <label>Stay signed in</label>
    </span>

    <Button label="Login" @click="loginClickedHandler" />
    <div class="flex flex-row justify-content-around mt-2" @click="$emit('onClose')">
      <RouterLink to="/recovery">Can't sign in?</RouterLink>
      <RouterLink to="/register">Create Account</RouterLink>
    </div>
  </div>
</template>

<script setup lang="ts">
import InputText from 'primevue/inputtext'
import Password from 'primevue/password'
import Button from 'primevue/button'
import Checkbox from 'primevue/checkbox'
import { ref } from 'vue'
import { useAuthenticationStore } from '@/stores/AuthenticationStore'
import { useToast } from 'primevue/usetoast'

const toast = useToast()
const authenticationStore = useAuthenticationStore()

const inputUsername = ref<string>('')
const inputPassword = ref<string>('')
const staySignedIn = ref<boolean>(false)

async function loginClickedHandler() {
  if (String(inputUsername.value).length < 3) {
    return toast.add({
      severity: 'error',
      summary: 'Register Message',
      detail: 'Invalid Username',
      life: 3000
    })
  }
  if (String(inputPassword.value).length < 3) {
    return toast.add({
      severity: 'error',
      summary: 'Register Message',
      detail: 'Invalid Password',
      life: 3000
    })
  }
  await authenticationStore.tryLogin(inputUsername.value, inputPassword.value).then((value) => {
    return console.log(value)
  })
}
</script>
