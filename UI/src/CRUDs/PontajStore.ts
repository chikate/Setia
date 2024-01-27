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
        beginTime: new Date().toLocaleDateString()
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
        beginTime: new Date().toLocaleDateString() + ' ' + new Date().toLocaleTimeString(),
        creationDate: new Date().toLocaleDateString()
      }
    },
    async getAll(): Promise<Pontaj[]> {
      const response = await makeRequest('Pontaj/GetAll', 'get')
      return (this.allLoadedItems = (await checkRequest(response)) as Pontaj[])
    },
    async add(): Promise<boolean> {
      console.log(this.selectedItem)
      const response = (await checkRequest(
        await makeRequest('Pontaj/Add', 'post', this.selectedItem)
      )) as boolean
      this.getAll()
      return response
    },
    async delete(): Promise<boolean> {
      // this.selectedItem?.deleted ? (this.selectedItem.deleted = true) : {}
      const response = (await checkRequest(
        await makeRequest('Pontaj/Update', 'put', this.selectedItem)
      )) as boolean
      this.getAll()
      return response
    }
  }
})
