import { defineStore } from 'pinia'
import type { AuthenticationStore, User } from '@/interfaces'
import { makeRequest, checkRequest } from '@/helpers'

export const useAuthenticationStore = defineStore('AuthenticationStore', {
  state: (): AuthenticationStore => {
    return {}
  },
  actions: {
    async tryLogin(username: string, password: string) {
      const response = await makeRequest(
        `User/Login?username=${username}&password=${password}`,
        'post'
      )
      return (this.user = (await checkRequest(response)) as User)

      // headers: {
      //   'Content-Type': 'application/x-www-form-urlencoded'
      // }
    },
    async tryRegister(email: string, username: string, password: string) {
      const response = await makeRequest(
        `User/Register?email=${email}&username=${username}&password=${password}`,
        'post'
      )
      return (this.user = (await checkRequest(response)) as User)
    }
  }
})
