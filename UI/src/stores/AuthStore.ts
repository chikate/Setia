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
          localStorage.setItem('token', this.token ?? '')
          this.userData = loginResult.user
          localStorage.setItem('user', JSON.stringify(this.userData))
          return Boolean(loginResult)
        }
      )
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
    },
    getToken() {
      return localStorage.getItem('token')
    },
    async logOut() {
      this.token = undefined
      this.userData = undefined
      localStorage.setItem('token', '')
      localStorage.setItem('user', '')
      return window.location.reload()
    },
    async checkUserTags(tags?: string[]): Promise<string[]> {
      return tags
    }
  },
  persist: true
})
