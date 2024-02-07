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
        (token: string) => {
          if (token.length > 50) {
            localStorage.setItem('token', token)
            window.location.reload()
            return Boolean(token)
          }
          return false
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
    async tryRegister(email: string, username: string, password: string) {
      return await makeApiRequest(`Auth/Register`, 'post', { email, login: { username, password } })
    }
  }
})
