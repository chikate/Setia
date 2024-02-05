import { defineStore } from 'pinia'
import { makeApiRequest } from '@/helpers'
import type { Definition } from '@/interfaces'

export interface User extends Definition {
  id?: number
  email: string
  username: string
  password: string
  name: string
  statusCode: number | null
  rights: string[] | null
}

const defaultEditables: User = {
  email: '',
  username: '',
  password: '',
  name: '',
  statusCode: null,
  rights: null,
  active: true
}

export const useUserStore = defineStore('Users', {
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
      return (this.allLoadedItems = ((await makeApiRequest(`${this.$id}/GetAll`, 'get')) ??
        []) as User[])
    },
    async add() {
      await makeApiRequest(`${this.$id}/Add`, 'post', this.selectedItem).then(() => {
        this.getAll()
      })
    },
    async update() {
      await makeApiRequest(`${this.$id}/Update`, 'put', this.selectedItem).then(() => {
        this.getAll()
      })
    },
    async delete() {
      this.selectedItem ? (this.selectedItem.deleted = true) : null
      await makeApiRequest(`${this.$id}/Update`, 'put', this.selectedItem).then(() => {
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
