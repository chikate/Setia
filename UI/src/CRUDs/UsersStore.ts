import { defineStore } from 'pinia'
import type { UsersStore, User } from '@/interfaces'
import { makeRequest, checkRequest } from '@/helpers'

const defaultEditables: User = {
  email: '',
  username: '',
  name: '',
  statusCode: 0,
  authorityCode: 0
}

export const useUsersStore = defineStore('Users', {
  state: (): UsersStore => {
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
