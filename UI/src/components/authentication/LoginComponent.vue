<template>
  <div class="flex flex-column gap-2" style="width: 18rem">
    <lable class="font-bold mb-1">Login</lable>

    <span class="p-float-label">
      <InputText v-model="inputUsername" class="w-full" />
      <label>Username</label>
    </span>

    <span class="p-float-label">
      <Password
        v-model="inputPassword"
        class="w-full"
        :feedback="false"
        :toggleMask="inputPassword.length > 0"
      />
      <label>Password</label>
    </span>

    <span class="flex flex-row gap-2 align-items-center">
      <Checkbox v-model="staySignedIn" :binary="true" />
      <label>Stay signed in</label>
    </span>

    <Button
      label="Login"
      @click="
        useAuthenticationStore()
          .tryLogin(inputUsername, inputPassword)
          .then((value) => {
            return value
          })
      "
    />
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

const inputUsername = ref<string>('')
const inputPassword = ref<string>('')
const staySignedIn = ref<boolean>(false)
</script>

<style>

</style>
