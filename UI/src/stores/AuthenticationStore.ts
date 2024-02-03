import { defineStore } from 'pinia'
import { makeRequest } from '@/helpers'

export const useAuthenticationStore = defineStore('Authentication', {
  state: (): {
    token: string | null
  } => {
    return {
      token: null
    }
  },
  actions: {
    async tryLogin(username: string, password: string) {
      if (username.length < 3) {
        return // toast
      }
      if (password.length < 3) {
        return // toast
      }
      this.token = await makeRequest(
        `Login/Login?username=${username}&password=${password}`,
        'post'
      )
    },
    async tryRegister(email: string, username: string, password: string) {
      await makeRequest(
        `Login/Register?email=${email}&username=${username}&password=${password}`,
        'post'
      )
    }
  }
})
