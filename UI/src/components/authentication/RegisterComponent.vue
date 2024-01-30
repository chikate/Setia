<template>
  <div class="flex flex-column gap-3">
    <div class="mb-1">Register</div>

    <label for="email" class="p-sr-only">Email</label>
    <span class="p-float-label">
      <InlineMessage severity="error" v-if="!inputEmail" class="absolute -translate-x-100">
        {{ 'Email is required' }}
      </InlineMessage>
      <InputText
        v-model="inputEmail"
        id="email"
        placeholder="Email"
        :class="inputEmail ? '' : 'p-invalid'"
      />
      <label>Email</label>
    </span>

    <label for="username" class="p-sr-only">Username</label>
    <span class="p-float-label">
      <InlineMessage severity="error" v-if="!inputUsername" class="absolute -translate-x-100">
        {{ 'Username is required' }}
      </InlineMessage>
      <InputText
        v-model="inputUsername"
        id="username"
        placeholder="Username"
        :class="inputUsername ? '' : 'p-invalid'"
      />
      <label>Username</label>
    </span>

    <label for="password" class="p-sr-only">Password</label>
    <span class="p-float-label">
      <InlineMessage severity="error" v-if="!inputPassword" class="absolute -translate-x-100">
        {{ 'Password is required' }}
      </InlineMessage>
      <Password
        v-model="inputPassword"
        toggleMask
        id="password"
        placeholder="Password"
        :class="inputPassword ? '' : 'p-invalid'"
      >
        <template #footer>
          <Divider class="my-3 p-0" />
          <p class="m-0 mb-2 p-0">Suggestions</p>
          <ul class="px-4 m-0" style="line-height: 1.5">
            <li>At least one lowercase</li>
            <li>At least one uppercase</li>
            <li>At least one numeric</li>
            <li>Minimum 8 characters</li>
          </ul>
        </template>
      </Password>
      <label>Password</label>
    </span>

    <label for="repeatPassword" class="p-sr-only">Repeat Password</label>
    <span class="p-float-label">
      <InlineMessage severity="error" v-if="!inputRepeatPassword" class="absolute -translate-x-100">
        {{ 'Repeat Password is required' }}
      </InlineMessage>
      <Password
        v-model="inputRepeatPassword"
        toggleMask
        :feedback="false"
        id="repeatPassword"
        placeholder="Repeat password"
        :class="inputRepeatPassword ? '' : 'p-invalid'"
      />
      <label>Repeat Password</label>
    </span>

    <Button class="mt-1 align-self-start" label="Register" @click="registerClickedHandler" />
  </div>
</template>

<script setup lang="ts">
import { useAuthenticationStore } from '@/stores/AuthenticationStore'
import { ref } from 'vue'
import { useToast } from 'primevue/usetoast'

const inputEmail = ref<string>()
const inputUsername = ref<string>()
const inputPassword = ref<string>()
const inputRepeatPassword = ref<string>()

function registerClickedHandler() {
  if (!inputEmail.value) {
    return useToast().add({
      severity: 'error',
      summary: 'Register Message',
      detail: 'Invalid Email',
      life: 3000
    })
  }
  if (String(inputUsername.value).length < 3) {
    return useToast().add({
      severity: 'error',
      summary: 'Register Message',
      detail: 'Invalid Username',
      life: 3000
    })
  }
  if (String(inputPassword.value).length < 7) {
    return useToast().add({
      severity: 'error',
      summary: 'Register Message',
      detail: 'Invalid Password',
      life: 3000
    })
  }
  if (inputPassword.value !== inputRepeatPassword.value) {
    return useToast().add({
      severity: 'error',
      summary: 'Register Message',
      detail: 'Passwords does not match',
      life: 3000
    })
  }
  return useAuthenticationStore().tryRegister(
    inputEmail.value as string,
    inputUsername.value as string,
    inputPassword.value as string
  )
}
</script>
