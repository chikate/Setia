import { defineStore } from 'pinia'
import { makeApiRequest } from '@/helpers'
import type { User } from '@/interfaces'

export const useAuthStore = defineStore('Auth', {
  state: (): {
    token?: string
    userData?: User
  } => {
    return {}
  },
  actions: {
    async tryLogin(username: string, password: string): Promise<boolean> {
      return await makeApiRequest(`${this.$id}/Login`, 'post', { username, password }).then(
        (loginResult) => {
          this.token = loginResult.token
          this.userData = loginResult.user
          return Boolean(loginResult)
        }
      )
    },
    async logOut() {
      this.token = undefined
      this.userData = undefined
      return window.location.reload()
    },
    hasRight(right: string): boolean {
      return true
    },
    async tryRegister(email: string, username: string, password: string): Promise<boolean> {
      return await makeApiRequest(`${this.$id}/Register`, 'post', {
        email,
        username,
        password
      }).then((successful: boolean) => {
        if (successful) {
          window.location.assign('/')
        }
        return successful
      })
    }
  },
  persist: true
})
