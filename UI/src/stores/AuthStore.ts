import { defineStore } from 'pinia'
import { makeApiRequest } from '@/helpers'
import type { User } from '@/interfaces'

export const useAuthStore = defineStore('Auth', {
  state: (): {
    userData?: User
  } => {
    return {}
  },
  actions: {
    async tryLogin(username: string, password: string): Promise<boolean> {
      return await makeApiRequest(`Auth/Login`, 'post', { username, password }).then(
        (loginResult) => {
          console.log(loginResult)
          if (loginResult.token.length > 50) {
            localStorage.setItem('token', loginResult.token)
          }
          this.userData = loginResult.user
          console.log(this.userData)
          return Boolean(loginResult)
        }
      )
    },
    async LogOut() {
      localStorage.setItem('token', '')
      return window.location.reload()
    },
    getToken(): string | null {
      return localStorage.getItem('token')
    },
    async tryRegister(email: string, username: string, password: string): Promise<boolean> {
      return await makeApiRequest(`Auth/Register`, 'post', { email, username, password }).then(
        (successful: boolean) => {
          if (successful) {
            window.location.assign('/')
          }
          return successful
        }
      )
    }
  },
  persist: true
})
