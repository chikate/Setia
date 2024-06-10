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
    // APIs
    async login(username: string, password: string): Promise<Boolean> {
      return await makeApiRequest(`${this.$id}/Login`, 'post', { username, password }).then(
        async (loginResponse: Response) => {
          if (loginResponse.status == 200) {
            const loginResult = await loginResponse.json()
            // this.token = loginResult.token
            localStorage.setItem('token', loginResult.token)
            // this.userData = loginResult.user
            localStorage.setItem('user', JSON.stringify(loginResult.user))
            window.location.reload()
          }
          return loginResponse.status == 200
        }
      )
    },
    async register(email: string, username: string, password: string): Promise<boolean> {
      return await makeApiRequest(`${this.$id}/Register`, 'post', {
        email,
        username,
        password
      }).then((registerResponse: Response) => {
        return registerResponse.status == 200
      })
    },
    checkUserRight(tag: string): boolean {
      console.log(this.userData?.tags?.indexOf(tag))
      return Boolean((this.userData?.tags?.indexOf(tag) ?? -1) > -1)
    },
    checkUserRights(tag?: string | string[]): boolean {
      // return Boolean((this.userData?.tags?.indexOf(tag) ?? -1) > 0)
      return false
    },
    async logOut() {
      // this.token = undefined
      // this.userData = undefined
      localStorage.setItem('token', '')
      localStorage.setItem('user', '')
      return window.location.replace('/')
    }
  },
  getters: {
    userData: () =>
      localStorage.getItem('user')
        ? (JSON.parse(String(localStorage.getItem('user'))) as User)
        : undefined,
    token: () => (localStorage.getItem('token') ? localStorage.getItem('token') : undefined)
  },
  persist: true
})
