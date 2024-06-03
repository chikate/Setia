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
    async login(username: string, password: string) {
      return await makeApiRequest(`${this.$id}/Login`, 'post', { username, password }).then(
        async (loginResponse: Response) => {
          const loginResult = await loginResponse.json()
          if (loginResult) {
            this.token = loginResult.token
            localStorage.setItem('token', this.token ?? '')

            this.userData = loginResult.user
            localStorage.setItem('user', JSON.stringify(this.userData))

            useUserTagsCRUDStore().get()
            return //window.location.reload()
          }
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
    async checkUserRights(tags?: string | string[]): Promise<boolean | string[]> {
      return Boolean(
        useUserTagsCRUDStore().allLoadedItems?.filter((value: string) => tags?.includes(value)) &&
          localStorage.getItem('token')
      )
    },

    // client functions
    getToken() {
      return localStorage.getItem('token')
    },
    async logOut() {
      this.token = undefined
      this.userData = undefined
      localStorage.setItem('token', '')
      localStorage.setItem('user', '')
      return window.location.reload()
    }
  },
  persist: true
})
