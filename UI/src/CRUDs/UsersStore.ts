import { defineStore } from 'pinia'
import type { UsersStore, User } from '@/interfaces'
import { makeRequest, checkRequest } from '@/helpers'

export const useUsersStore = defineStore('UsersStore', {
  state: (): UsersStore => {
    return {
      allLoadedItems: [],
      selectedItem: {
        // Editables
        email: '',
        name: ''
      }
    }
  },
  actions: {
    async resetSelection() {
      this.selectedItem = {
        id: 0,
        username: this.selectedItem.email,
        password: 'Password',
        active: true,
        deleted: false,
        creationDate: new Date().toISOString()
      }
    },
    async getAll(): Promise<User[]> {
      return await makeRequest('Users/GetAll', 'get').then(async (response: Response) => {
        return await checkRequest(response).then((data) => {
          return (this.allLoadedItems = data as User[])
        })
      })
    },
    async add() {
      console.log(JSON.stringify(this.selectedItem))
      return await makeRequest('Users/Add', 'post', this.selectedItem).then(
        async (response: Response) => {
          return await checkRequest(response).then(() => {
            this.getAll()
          })
        }
      )
    },
    async update() {
      return await makeRequest('Users/Update', 'put', this.selectedItem).then(
        async (response: Response) => {
          return await checkRequest(response).then(() => {
            this.getAll()
          })
        }
      )
    },
    async delete() {
      this.selectedItem.deleted = true
      return await makeRequest('Users/Update', 'put', this.selectedItem).then(
        async (response: Response) => {
          return await checkRequest(response).then(() => {
            this.getAll()
          })
        }
      )
    }
  }
})
