import { defineStore } from 'pinia'
import type { PontajStore, Pontaj } from '@/interfaces'
import { makeRequest, checkRequest } from '@/helpers'
import { useAuthenticationStore } from '@/stores/AuthenticationStore'

export const usePontajStore = defineStore('PontajStore', {
  state: (): PontajStore => {
    return {
      allLoadedItems: [],
      selectedItem: {
        // Editables
        beginTime: new Date().toISOString()
      }
    }
  },
  actions: {
    async resetSelection() {
      this.selectedItem = {
        id: 0,
        id_User: useAuthenticationStore().user?.id,
        active: true,
        deleted: false,
        beginTime: new Date().toISOString(),
        creationDate: new Date().toISOString()
      }
    },
    async getAll(): Promise<Pontaj[]> {
      return await makeRequest('Pontaj/GetAll', 'get').then(async (response: Response) => {
        return await checkRequest(response).then((data) => {
          return (this.allLoadedItems = data as Pontaj[])
        })
      })
    },
    async add() {
      return await makeRequest('Pontaj/Add', 'post', this.selectedItem).then(
        async (response: Response) => {
          return await checkRequest(response).then(() => {
            this.getAll()
          })
        }
      )
    },
    async update() {
      return await makeRequest('Pontaj/Update', 'put', this.selectedItem).then(
        async (response: Response) => {
          return await checkRequest(response).then(() => {
            this.getAll()
          })
        }
      )
    },
    async delete() {
      this.selectedItem.deleted = true
      return await makeRequest('Pontaj/Update', 'put', this.selectedItem).then(
        async (response: Response) => {
          return await checkRequest(response).then(() => {
            this.getAll()
          })
        }
      )
    }
  }
})
