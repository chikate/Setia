import { defineStore } from 'pinia'
import { makeRequest, checkRequest } from '@/helpers'
import type { Definition } from '@/interfaces'

export interface User extends Definition {
  id?: number
  email: string
  username: string
  password?: string
  name: string
  statusCode: number | null
  authorityCode: number | null
}

const defaultEditables: User = {
  email: '',
  username: '',
  name: '',
  statusCode: 0,
  authorityCode: 0,
  active: true
}

export const useUserStore = defineStore('User', {
  state: (): {
    allLoadedItems: User[]
    selectedItem: User
  } => {
    return {
      allLoadedItems: [],
      selectedItem: defaultEditables
    }
  },
  actions: {
    async getAll(): Promise<User[]> {
      return await makeRequest(`${this.$id}/GetAll`, 'get').then(async (response: Response) => {
        return await checkRequest(response).then((data) => {
          return (this.allLoadedItems = data as User[])
        })
      })
    },
    async add() {
      return await makeRequest(`${this.$id}/Add`, 'post', this.selectedItem).then(() => {
        this.getAll()
      })
    },
    async update() {
      return await makeRequest(`${this.$id}/Update`, 'put', this.selectedItem).then(() => {
        this.getAll()
      })
    },
    async delete() {
      this.selectedItem ? (this.selectedItem.deleted = true) : null
      return await makeRequest(`${this.$id}/Update`, 'put', this.selectedItem).then(() => {
        this.getAll()
      })
    },
    getDefaults() {
      return defaultEditables
    },
    async setSelectionToDefaults() {
      this.selectedItem = this.getDefaults()
    }
  }
})
