import { defineStore } from 'pinia'
import type { AuthenticationStore, User } from '@/interfaces'
import { makeRequest, checkRequest } from '@/helpers'
import { useToast } from 'primevue/usetoast'

export const useAuthenticationStore = defineStore('AuthenticationStore', {
  state: (): AuthenticationStore => {
    return {
      user: {
        id: 6,
        email: '',
        username: '',
        password: '',
        name: '',
        statusCode: 0,
        authorityCode: 0,
        active: false,
        deleted: false,
        creationDate: new Date().toISOString(),
        id_CreatedBy: 0,
        createdBy: {} as User,
        lastUpdate: new Date().toISOString(),
        id_LastUpdateBy: 0,
        lastUpdateBy: {} as User
      }
    }
  },
  actions: {
    async tryLogin(username: string, password: string) {
      if (username.length < 3) {
        return useToast().add({
          severity: 'error',
          summary: 'Register Message',
          detail: 'Invalid Username',
          life: 3000
        })
      }
      if (password.length < 3) {
        return useToast().add({
          severity: 'error',
          summary: 'Register Message',
          detail: 'Invalid Password',
          life: 3000
        })
      }
      return await makeRequest(
        `Authentication/Login?username=${username}&password=${password}`,
        'post'
      ).then(async (response: Response) => {
        return await checkRequest(response).then((data) => {
          return (this.user = data as User)
        })
      })
    },
    async tryRegister(email: string, username: string, password: string) {
      return await makeRequest(
        `Authentication/Register?email=${email}&username=${username}&password=${password}`,
        'post'
      ).then(async (response: Response) => {
        return await checkRequest(response).then(() => {})
      })
    }
  }
})
